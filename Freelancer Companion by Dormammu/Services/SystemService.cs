using Freelancer_Companion_by_Dormammu.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Freelancer_Companion_by_Dormammu.Services
{
    public class SystemService
    {
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        private static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpFileName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int LoadString(IntPtr hInstance, int ID, StringBuilder lpBuffer, int nBufferMax);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FreeLibrary(IntPtr hModule);
        /// <summary>
        /// Все данные системы
        /// </summary>
        public Dictionary<string, UniverseSystem> UniverseSystemsData { get; set; }
        public Dictionary<string, UniverseBase> UniverseBasesData { get; set; }
        public List<string> SystemsID { get; set; }
        /// <summary>
        /// Список предметов в игре
        /// </summary>
        public List<Equipment> Equipments { get; set; }
        /// <summary>
        /// ID системы - Список зон добычи ископаемых
        /// </summary>
        public Dictionary<string, List<LootableZone>> SysAsteroids { get; set; }
        /// <summary>
        /// ID - Name системы
        /// </summary>
        public Dictionary<string, string> SystemNamesID { get; set; }
        /// <summary>
        /// Cписок контейнеров
        /// </summary>
        public List<string> SupriseID { get; set; }
        /// <summary>
        /// Список путей от систем до систем
        /// </summary>
        public List<string> HollRoads { get; set; } 
        public string IDCurrent { get; set; }
        private string BaseId { get; set; }
        private string SystemID { get; set; }
        /// <summary>
        /// Массив очищенных айдишников систем для ComboBox
        /// </summary>
        public string[] ArraySystemsCombobox { get; set; }
        /// <summary>
        /// Name - Id систем
        /// </summary>
        public Dictionary<string, string> SystemsNameId { get; set; }
        public bool IsRussian { get; set; }

        public SystemService(bool isRussian)
        {
            UniverseSystemsData = new Dictionary<string, UniverseSystem>();
            UniverseBasesData = new Dictionary<string, UniverseBase>();
            SystemNamesID = new Dictionary<string, string>();
            SystemsNameId = new Dictionary<string, string>();
            SysAsteroids = new Dictionary<string, List<LootableZone>>();
            Equipments = new List<Equipment>();
            SystemsID = new List<string>();
            SupriseID = new List<string>();
            HollRoads = new List<string>();
            IsRussian = isRussian;
        }

        /// <summary>
        /// Чтение элемента из DLL
        /// </summary>
        /// <param name="file">Адрес до файла</param>
        /// <param name="number">Номер элемента</param>
        /// <returns></returns>
        public string ExtractStringFromDLL(string file, int number)
        {
            var lib = LoadLibrary(file);
            var resultBuilder = new StringBuilder(2048);
            LoadString(lib, number, resultBuilder, resultBuilder.Capacity);
            FreeLibrary(lib);
            return resultBuilder.ToString();
        }

        public void GetInfo(ComboBox blockInfo, ComboBox road_1, ComboBox road_2, ComboBox equipments, LogService logService)
        {
            try
            {   File.Delete("log.txt"); //чищу логи
                GetAllSystems();
                GetAllEquipments();
                GetAllAsteroids();
                var dirInfoSystems = new DirectoryInfo("SYSTEMS");
                var dirInfoArray = dirInfoSystems.GetDirectories();
                //получаю список грузов
                using (var reader = new StreamReader("loadouts.ini"))
                {
                    var line = string.Empty;
                    bool load = false;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (!string.IsNullOrEmpty(line))
                        {
                            line = line.ToLower();
                            if (line.Contains("[loadout]"))
                                load = true;
                            if(load) GetLoadout(line, logService);
                        }
                    }
                }

                using (var reader = new StreamReader("universe.ini"))
                {
                    var line = string.Empty;
                    bool systemState = false, baseState = false;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (!string.IsNullOrEmpty(line))
                        {
                            if (line.Contains("[system]"))
                            {
                                systemState = true;
                                baseState = false;
                            }
                            if (line.Contains("[Base]"))
                            {
                                systemState = false;
                                baseState = true;
                            }
                            if (systemState)
                                GetSystemDataToFile(line, logService);
                            if (baseState)
                                GetBaseDataToFile(line, logService);
                        }
                    }
                }

                var resultDataSystems = new Dictionary<string, UniverseSystem>();
                int resultCountSystem = 0;
                var countCurrSys = 0;
                ArraySystemsCombobox = new string[UniverseSystemsData.Count];

                //формирую список идентификаторов систем
                foreach (var dirInfo in dirInfoArray)
                {
                    var dirName = dirInfo.ToString().ToLower();
                    if (UniverseSystemsData.ContainsKey(dirName))
                    {
                        resultCountSystem++;
                        resultDataSystems.Add(dirName, UniverseSystemsData[dirName]);

                        road_1.Items.Add((IsRussian) ? UniverseSystemsData[dirName].Name : UniverseSystemsData[dirName].Id);
                        road_2.Items.Add((IsRussian) ? UniverseSystemsData[dirName].Name : UniverseSystemsData[dirName].Id);

                        if (UniverseSystemsData[dirName].Name.Length == 0)
                            blockInfo.Items.Add(UniverseSystemsData[dirName].Id);
                        else blockInfo.Items.Add(UniverseSystemsData[dirName].Name + " | " + UniverseSystemsData[dirName].Id);
                        ArraySystemsCombobox[countCurrSys] = dirName;
                        countCurrSys++;
                        SystemsID.Add(UniverseSystemsData[dirName].Id);
                    }
                    else
                    {
                        resultCountSystem++;
                        logService.ErrorWarningEvent("[" + resultCountSystem + "]  " + dirName + " - не является системой...");
                    }
                }

                blockInfo.SelectedIndex = 0;
                road_1.SelectedIndex = 0;
                road_2.SelectedIndex = 1;
                ArraySystemsCombobox = ArraySystemsCombobox.Where(x => x != null).ToArray();
                logService.LogEvent("Список систем определён");

                //обработать спсиок всех связей между системами, все гиперпереходы
                foreach (var sys in ArraySystemsCombobox)
                {
                    foreach (var elem in UniverseSystemsData[sys].Objects.FindAll((el) => !el.ID.Contains('=') && el.ID.ToLower().Contains(sys.ToLower() + "_to")))
                    {
                        var name = elem.ID;
                        var destiny = name.Substring(name.IndexOf('_') + 4, name.Length - 4 - sys.Length);
                        if (destiny.IndexOf('_') != -1)
                            destiny = destiny.Substring(0, destiny.IndexOf('_'));
                        var res = sys.ToLower() + "=" + destiny.ToLower();
                        if(!res.Contains("police01"))
                            HollRoads.Add(res);
                    }
                    HollRoads.Add("aod01=hu04"); //система ангелов тьиы
                    HollRoads.Add("hu04=aod01"); //система ангелов тьиы
                    HollRoads.Add("dream_system01=hi03"); //система грёз
                    HollRoads.Add("hi03=dream_system01"); //система грёз
                }
                //перебирает список корректный систем
                for(int i = 0; i < ArraySystemsCombobox.Length; i++)
                {
                    if (!SystemsNameId.ContainsKey(UniverseSystemsData[ArraySystemsCombobox[i]].Name))
                        SystemsNameId.Add(UniverseSystemsData[ArraySystemsCombobox[i]].Name, ArraySystemsCombobox[i]);
                    else
                        logService.LogEvent(UniverseSystemsData[ArraySystemsCombobox[i]].Name);
                }

                //формирую combobox для поиска элемента
                foreach (var eq in Equipments)
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Text = (string.IsNullOrEmpty(eq.Name) ? eq.Id : eq.Name);
                    item.ID = eq.Id;
                    equipments.Items.Add(item);
                }

                logService.LogEvent("Список маршрутов определён");
            }
            catch (Exception exception)
            {
                logService.ErrorLogEvent(exception.Message);
            }
        }

        private void GetLoadout(string line, LogService logService)
        {
            if (line.Contains("nickname"))
            {
                var nick = (line.Substring(10, line.Length - 10)).Trim().ToLower();
                SupriseID.Add(nick);
            }
        }

        private void GetSystemDataToFile(string line, LogService logService)
        {
            if (line.Contains("nickname"))
            {
                SystemID = (line.Substring(10, line.Length - 10)).Trim().ToLower();
                UniverseSystemsData.Add(SystemID, new UniverseSystem()
                {
                    Id = SystemID
                });
            }
            if (line.Contains("strid_name"))
            {
                var dll_name = (line.Substring(12, line.Length - 12)).Trim();
                UniverseSystemsData[SystemID].DLL_Name = dll_name;
                var id = SystemID.ToLower();
                string name = string.Empty;
                if (SystemNamesID.ContainsKey(id)) name = SystemNamesID[id];
                else name = id;
                UniverseSystemsData[SystemID].Name = name;
            }
            if (line.Contains("visit"))
            {
                var visit = (line.Substring(7, line.Length - 7)).Trim();
                UniverseSystemsData[SystemID].Visit = int.Parse(visit);
            }
            if (line.Contains("ids_info"))
            {
                var dll_ids_name = (line.Substring(10, line.Length - 10)).Trim();
                UniverseSystemsData[SystemID].DLL_InfoCard = dll_ids_name;
            }
            if (line.Contains("file"))
            {
                var file = (line.Substring(6, line.Length - 6)).Trim();
                UniverseSystemsData[SystemID].INI = file;
                GetSystemInfo(SystemID, logService);
            }
            if (line.Contains("NavMapScale"))
            {
                var nav = (line.Substring(13, line.Length - 13)).Trim();
                IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
                UniverseSystemsData[SystemID].NavMapScale = double.Parse(nav, formatter);
            }
        }

        private void GetBaseDataToFile(string line, LogService logService)
        {
            if (line.Contains("nickname"))
            {
                BaseId = (line.Substring(10, line.Length - 10)).Trim().ToLower();
                UniverseBasesData.Add(BaseId, new UniverseBase()
                {
                    Id = BaseId
                });
            }
            if (line.Contains("strid_name"))
            {
                var dll_name = (line.Substring(12, line.Length - 12)).Trim();
                if (dll_name.Contains(";"))
                    dll_name = dll_name.Substring(0, dll_name.IndexOf(';'));
                UniverseBasesData[BaseId].DLL_Name = dll_name;
                var names = GetNameSystem(logService, int.Parse(dll_name));
                foreach (var name in names)
                {
                    UniverseBasesData[BaseId].Name += name + " | ";
                }
            }
        }

        /// <summary>
        /// Читает конфигурационный файл системы
        /// </summary>
        /// <param name="systemId">ID системы</param>

        private void GetSystemInfo(string systemId, LogService logService)
        {
            var sr = new StreamReader(UniverseSystemsData[systemId].INI);
            var data = sr.ReadLine();
            var Object = false;
            while (data != null)
            {
                if (data.Contains("Object"))
                {
                    Object = true;
                    if (UniverseSystemsData[systemId].Objects == null)
                        UniverseSystemsData[systemId].Objects = new List<ObjectSystem>();
                }
                if (data.Contains("Zone"))
                {
                    Object = false;
                }
                if (Object)
                {
                    if (data.Contains("nickname"))
                    {
                        var id_name = (data.Substring(10, data.Length - 10)).Trim();
                        UniverseSystemsData[systemId].Objects.Add(new ObjectSystem()
                        {
                            ID = id_name
                        });
                    }
                    if (data.Contains("pos ="))
                    {
                        var position = (data.Substring(5, data.Length - 5)).Trim();
                        if (position.Contains(";"))
                            position = (position.Substring(0, position.IndexOf(';')).Trim());
                        int[] pos = position.Split(',').Select(n =>
                        {
                            int val = 0;
                            n = n.Trim();
                            var state = int.TryParse(n, out val);
                            if (state == false)
                            {
                                double td = 0;
                                if (n.Contains('.'))
                                {
                                    IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
                                    td = double.Parse(n, formatter);
                                    int i = (int)Math.Round(td, MidpointRounding.AwayFromZero);
                                    val = i;
                                    return val;
                                }
                                else
                                {
                                    File.AppendAllText("log.txt", "Error Parse Position - " + position + " - " + UniverseSystemsData[systemId].INI + "\n");
                                    return 0;
                                }
                            }
                            else return val;
                        }).ToArray();
                        if(pos == null)
                        {
                            logService.ErrorLogEvent(systemId);
                        }
                        else UniverseSystemsData[systemId].Objects[UniverseSystemsData[systemId].Objects.Count - 1].Pos = pos;
                    }
                    if (data.Contains("base ="))
                    {
                        var baseID = (data.Substring(6, data.Length - 6)).Trim();
                        UniverseSystemsData[systemId].Objects[UniverseSystemsData[systemId].Objects.Count - 1].BaseID = baseID;
                    }
                    if (data.Contains("ids_name ="))
                    {
                        var idsName = (data.Substring(10, data.Length - 10)).Trim();
                        UniverseSystemsData[systemId].Objects[UniverseSystemsData[systemId].Objects.Count - 1].IdsName = idsName;
                    }
                    if (data.Contains("archetype ="))
                    {
                        var archetype = (data.Substring(13, data.Length - 13)).Trim();
                        UniverseSystemsData[systemId].Objects[UniverseSystemsData[systemId].Objects.Count - 1].Archetype = archetype;
                    }
                    if (data.Contains("loadout ="))
                    {
                        var loadout = (data.Substring(11, data.Length - 11)).Trim();
                        UniverseSystemsData[systemId].Objects[UniverseSystemsData[systemId].Objects.Count - 1].Loadout = loadout;
                    }
                    if (data.Contains("goto =") && !data.Contains(';'))
                    {
                        var loadout = (data.Substring(6, data.Length - 6)).Trim().ToLower();
                        var idS = loadout.Substring(0, loadout.IndexOf(','));
                        var nameS = SystemNamesID[idS];
                        UniverseSystemsData[systemId].Objects[UniverseSystemsData[systemId].Objects.Count - 1].Goto = nameS;
                        UniverseSystemsData[systemId].Objects[UniverseSystemsData[systemId].Objects.Count - 1].GotoID = idS;
                    }
                }
                data = sr.ReadLine();
            }
        }

        public List<string> GetNameSystem(LogService logService, int id)
        {
            string[] dlls = new string[] { "NameResources.dll", "SBM.dll", "SBM2.dll", "SBM3.dll" };
            var names = new List<string>();
            foreach(string dll in dlls)
            {
                string name = ExtractStringFromDLL(dll, id);
                if (!string.IsNullOrEmpty(name))
                {
                    if(!names.Contains(name))
                    {
                        names.Add(name);
                    }
                }
            }
            if(names.Count == 0)
            {
                names.Add("НЕТ НАЗВАНИЙ");
                logService.ErrorLogEvent("id: " + id + " - не содержит названия");
            }
            return names;
        }

        private void GetAllSystems()
        {
            //получаю список систем
            using (var reader = new StreamReader("systems.ini"))
            {
                var line = string.Empty;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        var idS = line.Substring(0, line.IndexOf('='));
                        var nameS = line.Substring(line.IndexOf('=') + 1);
                        SystemNamesID.Add(idS, nameS);
                    }
                }
            }
        }

        private void GetAllEquipments()
        {
            //получаю список систем
            using (var reader = new StreamReader("equipments.ini"))
            {
                var line = string.Empty;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        var tmp1 = line.Substring(line.IndexOf(',') + 1, line.Length - (line.IndexOf(',') + 1));
                        var tmp2 = tmp1.Substring(tmp1.IndexOf(',') + 1, tmp1.Length - (tmp1.IndexOf(',') + 1));
                        var tmp3 = tmp2.Substring(tmp2.IndexOf(',') + 1, tmp2.Length - (tmp2.IndexOf(',') + 1));
                        var tmp4 = tmp3.Substring(tmp3.IndexOf(',') + 1, tmp3.Length - (tmp3.IndexOf(',') + 1));
                        var Id = tmp4.Substring(0, tmp4.IndexOf(',')).Trim();
                        var tmp5 = tmp4.Substring(tmp4.IndexOf(',') + 1, tmp4.Length - (tmp4.IndexOf(',') + 1));
                        var Name = tmp5.Substring(0, tmp5.IndexOf(',')).Trim();
                        Equipments.Add(new Equipment() { Id = Id, Name = Name });
                    }
                }
            }
        }

        private void GetAllAsteroids()
        {
            var dirInfoSystems = new DirectoryInfo("ASTEROIDS");
            var files = dirInfoSystems.GetFiles();

            foreach(var file in files)
            {
                if (!file.Name.Contains('_')) continue;
                var idSys = file.Name.Substring(0, file.Name.IndexOf('_'));
                //получаю список груза
                using (var reader = new StreamReader(Path.Combine("ASTEROIDS", file.Name)))
                {
                    var line = string.Empty;
                    bool loot = false;
                    var nameZone = string.Empty;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (!string.IsNullOrEmpty(line))
                        {
                            if (line.Contains("[LootableZone]")) loot = true;
                            if (line.Contains("[TexturePanels]")) loot = false;
                            if (loot)
                            {
                                //id зоны
                                if(line.Contains("zone ="))
                                {
                                    nameZone = line.Substring(line.IndexOf('=') + 1, line.Length - (line.IndexOf('=') + 1)).Trim();
                                }
                                //Груз астероида
                                if(line.Contains("asteroid_loot_commodity ="))
                                {
                                    var lootes = line.Substring(line.IndexOf('=') + 1, line.Length - (line.IndexOf('=') + 1)).Trim();
                                    if (SysAsteroids.ContainsKey(idSys))
                                    {
                                        SysAsteroids[idSys].Add(new LootableZone()
                                        {
                                            LootId = lootes.ToLower(),
                                            ZoneName = nameZone.ToLower()
                                        });
                                    }
                                    else
                                    {

                                        SysAsteroids.Add(idSys, new List<LootableZone>() { new LootableZone()
                                        {
                                            LootId = lootes.ToLower(),
                                            ZoneName = nameZone.ToLower()
                                        }});
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

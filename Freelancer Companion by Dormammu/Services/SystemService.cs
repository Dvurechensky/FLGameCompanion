using Freelancer_Companion_by_Dormammu.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

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

        public Dictionary<string, UniverseSystem> UniverseSystemsData { get; set; }
        public Dictionary<string, UniverseBase> UniverseBasesData { get; set; }
        public List<string> SystemsID { get; set; }
        public string IDCurrent { get; set; }

        public SystemService()
        {
            UniverseSystemsData = new Dictionary<string, UniverseSystem>();
            UniverseBasesData = new Dictionary<string, UniverseBase>();
            SystemsID = new List<string>();
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

        public void GetInfo(ComboBox blockInfo, LogService logService)
        {
            try
            {
                File.Delete("log.txt");
                var dirInfoSystems = new DirectoryInfo("SYSTEMS");
                var dirInfoArray = dirInfoSystems.GetDirectories();

                //получаю список всех баз и систем
                var universePath = "universe.ini";

                using (StreamReader reader = new StreamReader(universePath))
                {
                    string line;
                    int countBase = 0;
                    int countSystem = 0;
                    bool systemState = false;
                    bool baseState = false;
                    string baseId = string.Empty;
                    string systemId = string.Empty;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (!string.IsNullOrEmpty(line))
                        {
                            //определяю заголовок это или нет
                            if (line.Contains("[system]"))
                            {
                                countSystem++;
                                systemState = true;
                                baseState = false;
                            }
                            //определяю заголовок это или нет
                            if (line.Contains("[Base]"))
                            {
                                countBase++;
                                systemState = false;
                                baseState = true;
                            }
                            if (systemState)
                            {
                                if (line.Contains("nickname"))
                                {
                                    systemId = (line.Substring(10, line.Length - 10)).Trim().ToLower();
                                    UniverseSystemsData.Add(systemId, new UniverseSystem()
                                    {
                                        Id = systemId
                                    });
                                }
                                if (line.Contains("strid_name"))
                                {
                                    var dll_name = (line.Substring(12, line.Length - 12)).Trim();
                                    UniverseSystemsData[systemId].DLL_Name = dll_name;
                                    var names = GetNameSystem(logService, int.Parse(dll_name));
                                    foreach (var name in names)
                                    {
                                        UniverseSystemsData[systemId].Name += name + " | ";
                                    }
                                }
                                if (line.Contains("visit"))
                                {
                                    var visit = (line.Substring(7, line.Length - 7)).Trim();
                                    UniverseSystemsData[systemId].Visit = int.Parse(visit);
                                }
                                if (line.Contains("ids_info"))
                                {
                                    var dll_ids_name = (line.Substring(10, line.Length - 10)).Trim();
                                    UniverseSystemsData[systemId].DLL_InfoCard = dll_ids_name;
                                }
                                if (line.Contains("file"))
                                {
                                    var file = (line.Substring(6, line.Length - 6)).Trim();
                                    UniverseSystemsData[systemId].INI = file;
                                    StreamReader sr = new StreamReader(UniverseSystemsData[systemId].INI);
                                    var data = sr.ReadLine();
                                    var Object = false;
                                    var countObject = 0;
                                    while (data != null)
                                    {
                                        if (data.Contains("Object"))
                                        {
                                            Object = true;
                                            countObject++;
                                            if(UniverseSystemsData[systemId].Objects == null)
                                                UniverseSystemsData[systemId].Objects = new List<ObjectSystem>();
                                        }

                                        if(Object)
                                        {
                                            if(data.Contains("nickname"))
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
                                                if(position.Contains(";"))
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
                                                            File.AppendAllText("log.txt", "Error Parse Position - " + position + " - " + file + "\n");
                                                            return 0;
                                                        }
                                                    }
                                                    else return val;
                                                }).ToArray();
                                                UniverseSystemsData[systemId].Objects[countObject - 1].Pos = pos;
                                            }
                                            if(data.Contains("base ="))
                                            {
                                                var baseID = (data.Substring(6, data.Length - 6)).Trim();
                                                UniverseSystemsData[systemId].Objects[countObject - 1].BaseID = baseID;
                                            }
                                            if (data.Contains("ids_name ="))
                                            {
                                                var idsName = (data.Substring(10, data.Length - 10)).Trim();
                                                UniverseSystemsData[systemId].Objects[countObject - 1].IdsName = idsName;
                                            }
                                        }

                                        data = sr.ReadLine();
                                    }
                                }
                            }
                            if (baseState)
                            {
                                if (line.Contains("nickname"))
                                {
                                    baseId = (line.Substring(10, line.Length - 10)).Trim().ToLower();
                                    UniverseBasesData.Add(baseId, new UniverseBase()
                                    {
                                        Id = baseId
                                    });
                                }
                                if (line.Contains("strid_name"))
                                {
                                    var dll_name = (line.Substring(12, line.Length - 12)).Trim();
                                    if (dll_name.Contains(";"))
                                        dll_name = dll_name.Substring(0, dll_name.IndexOf(';'));
                                    UniverseBasesData[baseId].DLL_Name = dll_name;
                                    var names = GetNameSystem(logService, int.Parse(dll_name));
                                    foreach (var name in names)
                                    {
                                        UniverseBasesData[baseId].Name += name + " | ";
                                    }
                                }
                            }
                        }
                    }
                }

                logService.LogEvent("Список баз определён");

                var resultDataSystems = new Dictionary<string, UniverseSystem>();
                int resultCountSystem = 0;

                //формирую список идентификаторов систем
                foreach (var dirInfo in dirInfoArray)
                {
                    var dirName = dirInfo.ToString().ToLower();
                    if (UniverseSystemsData.ContainsKey(dirName))
                    {
                        resultCountSystem++;
                        resultDataSystems.Add(dirName, UniverseSystemsData[dirName]);
                        if (UniverseSystemsData[dirName].Name.Length == 0)
                            blockInfo.Items.Add(UniverseSystemsData[dirName].Id);
                        else
                            blockInfo.Items.Add(UniverseSystemsData[dirName].Name);
                        SystemsID.Add(UniverseSystemsData[dirName].Id);
                    }
                    else
                    {
                        resultCountSystem++;
                        logService.ErrorLogEvent("[" + resultCountSystem + "]  " + dirName + " - не является системой...");
                    }
                }
                blockInfo.SelectedIndex = 0;
                logService.LogEvent("Список систем определён");
            }
            catch (Exception exception)
            {
                logService.ErrorLogEvent(exception.Message);
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
    }
}

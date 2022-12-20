using Freelancer_Companion_by_Dormammu.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
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

        private Dictionary<string, UniverseSystem> UniverseSystemsData { get; set; }

        public SystemService()
        {
            UniverseSystemsData = new Dictionary<string, UniverseSystem>();
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
                    if(!string.IsNullOrEmpty(line))
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

                        if(systemState)
                        {
                            if(line.Contains("nickname"))
                            {
                                systemId = (line.Substring(10, line.Length - 10)).Trim();
                                UniverseSystemsData.Add(systemId, new UniverseSystem()
                                {
                                    Id = systemId
                                });
                            }
                            if (line.Contains("strid_name"))
                            {
                                var dll_name = (line.Substring(12, line.Length - 12)).Trim();
                                UniverseSystemsData[systemId].DLL_Name = dll_name;
                                string name = GetNameSystem(int.Parse(dll_name));
                                UniverseSystemsData[systemId].Name = name;
                                logService.LogEvent(countSystem + " - " + UniverseSystemsData[systemId].DLL_Name + " / " + UniverseSystemsData[systemId].Name);
                            }
                        }
                    }
                }
            }


            ////формирую список идентификаторов систем
            //foreach (var dirInfo in dirInfoArray)
            //{
            //    //получаю данные системы



            //    UniverseSystemsData.Add(
            //        new UniverseSystem()
            //        {
            //            Id = dirInfo.ToString()
            //        }
            //    );
            //    blockInfo.Items.Add(dirInfo.ToString());
            //}
        }

        public string GetNameSystem(int id)
        {
            string[] dlls = new string[] { "NameResources.dll", "SBM.dll", "SBM2.dll", "SBM3.dll" };
            foreach(string dll in dlls)
            {
                string name = ExtractStringFromDLL(dll, id);
                if (!string.IsNullOrEmpty(name)) return name;
            }
            return string.Empty;
        }
    }
}

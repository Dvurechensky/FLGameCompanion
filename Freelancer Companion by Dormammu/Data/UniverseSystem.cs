namespace Freelancer_Companion_by_Dormammu.Data
{
    public class UniverseSystem
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Имя 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Адрес до INI
        /// </summary>
        public string INI { get; set; }
        /// <summary>
        /// ОТключено (Позиция системы на карте в MultiUniverse)
        /// </summary>
        public int[] Pos { get; set; }
        /// <summary>
        /// Посетил ли
        /// </summary>
        public int Visit { get; set; }
        /// <summary>
        /// Ссылка на имя в DLL
        /// </summary>
        public string DLL_Name { get; set; }
        /// <summary>
        /// Ссылка на инфоркарту DLL
        /// </summary>
        public string DLL_InfoCard { get; set; }
        /// <summary>
        /// НЕ работает(позиция на карте теперь в MultiUniverse)
        /// </summary>
        public string NavMapScale { get; set; }
        /// <summary>
        /// НПС говорят название объекта
        /// </summary>
        public string MsgIdPrefix { get; set; }
    }
}

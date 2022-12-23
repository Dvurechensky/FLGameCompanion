using System;
using System.Collections.Generic;

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
        public double NavMapScale { get; set; }
        /// <summary>
        /// Итоговый радиус системы
        /// </summary>
        public int Radius
        {
            get
            {
                double val;
                if (NavMapScale != 0)
                {
                    val = 131041.0 / NavMapScale;
                }
                else val = 131041.0;
                return (int)Math.Round(val, MidpointRounding.AwayFromZero);
            }
        }
        /// <summary>
        /// НПС говорят название объекта
        /// </summary>
        public string MsgIdPrefix { get; set; }
        /// <summary>
        /// Список объектов
        /// </summary>
        public List<ObjectSystem> Objects { get; set; }
    }

    public class ObjectSystem
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// Местоположение
        /// </summary>
        public int[] Pos { get; set; }
        /// <summary>
        /// Позиция на карте в App
        /// </summary>
        public int[] MapPos { get; set; }
        /// <summary>
        /// База ID
        /// </summary>
        public string BaseID { get; set; }
        /// <summary>
        /// Имя базы
        /// </summary>
        public string NameBase { get; set; }
        /// <summary>
        /// Инфокарта объекта
        /// </summary>
        public string IdsName { get; set; }
        /// <summary>
        /// Принадлежность объекта
        /// </summary>
        public string Archetype { get; set; }
    }
}

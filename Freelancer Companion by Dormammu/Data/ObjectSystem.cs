﻿namespace Freelancer_Companion_by_Dormammu.Data
{
    /// <summary>
    /// Класс объектов системы
    /// </summary>
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
        /// <summary>
        /// Сюрприз ли это
        /// </summary>
        public string Loadout { get; set; }
        /// <summary>
        /// Куда ведёт портал
        /// </summary>
        public string Goto { get; set; }
        /// <summary>
        /// Куда ведёт портал ID
        /// </summary>
        public string GotoID { get; set; }
    }
}
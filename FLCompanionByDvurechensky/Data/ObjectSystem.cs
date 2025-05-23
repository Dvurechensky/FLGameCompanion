﻿/*
 * Author: Nikolay Dvurechensky
 * Site: https://www.dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 мая 2025 06:38:00
 * Version: 1.0.27
 */

namespace FLCompanionByDvurechensky.Data
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

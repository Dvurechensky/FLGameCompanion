using System.Collections.Generic;

namespace Freelancer_Companion_by_Dormammu.Data
{
    /// <summary>
    /// Сюрпризик
    /// </summary>
    public class Loadout
    {
        public Loadout()
        {
            Cargo = new List<Cargo>();
        }
        /// <summary>
        /// Имя сюрприза
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Тип сюрприза
        /// </summary>
        public string Archetype { get; set; }
        /// <summary>
        /// Груз сюрприза
        /// </summary>
        public List<Cargo> Cargo { get; set; }
    }

    public class Cargo
    {
        /// <summary>
        /// Имя груза
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Количество груза
        /// </summary>
        public int Count { get; set; }
    }
}

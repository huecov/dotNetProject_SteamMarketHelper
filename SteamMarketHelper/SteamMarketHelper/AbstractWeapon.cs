using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamMarketHelper
{
    public abstract class AbstractWeapon : IWeapon
    {
        public double MedianaPrice { get; set; }
        public double MinimunPrice { get; set; }
        public int Volume { get; set; }

        public abstract string weaponType();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamMarketHelper
{
    public interface IWeapon
    {
        double MedianaPrice { get; set; }
        double MinimunPrice { get; set; }
        int Volume { get; set; }

        string weaponType();
    }
}

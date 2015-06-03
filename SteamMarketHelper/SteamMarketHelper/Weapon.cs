using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamMarketHelper
{
    public class Weapon : AbstractWeapon
    {
        public Weapon() { }
        public Weapon(double mediana, double min, int vol)
        {
            MedianaPrice = mediana;
            MinimunPrice = min;
            Volume = vol;
        }

        public string WeaponCategory { get; set; }
        public string WeaponModel { get; set; }
        public string WeaponSkin { get; set; }
        public string WeaponExterior { get; set; }
        public string Currency { get; set; }
        public string LinkToSteamServer { get; set; }

        public override string ToString()
        {
            return "Waluta: " + Currency + "; Mediania: " + MedianaPrice + "; Minimalna: "+ MinimunPrice + "; Ilosc: " + Volume + ";";
        }

        public override string weaponType()
        {
            return "Kategoria: " + WeaponCategory + "; Model: " + WeaponModel + "; Wzor: " + WeaponSkin + "; Stan: " + WeaponExterior + ";";
        }
    }
}

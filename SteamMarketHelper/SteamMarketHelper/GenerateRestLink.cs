using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamMarketHelper
{
    public class GenerateRestLink
    {
        public static readonly string country = "PL";

        public GenerateRestLink() { }

        public string generateAddress(int currency, string model, string skin, string exterior)
        {
            model = model.Replace(" ", "%20");
            skin = skin.Replace(" ", "%20");

            string link = "";
            link += "?country=" + country;
            link += "&";
            link += "currency=" + currency.ToString();
            link += "&appid=730";
            link += "&";
            link += "market_hash_name=" + model;
            link += "%20|%20";
            link += skin;
            link += "%20%28";
            link += exterior;
            link += "%29";

            return link;
        }
    }
}

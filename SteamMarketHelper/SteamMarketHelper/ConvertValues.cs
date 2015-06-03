using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamMarketHelper
{
    public class ConvertValues
    {
        public ConvertValues() { }

        public double convertCosts(string currency, string textPrice)
        {
            double result = 0;
            string[] s;

            textPrice = textPrice.Replace("-", "0");
            textPrice = textPrice.Replace(".", ",");
            textPrice = textPrice.Replace("USD", "");
            switch (currency)
            {
                case "USD":
                    s = textPrice.Split(';');
                    result = Double.Parse(s[1]);
                    break;
                case "GBP":
                    s = textPrice.Split(';');
                    result = Double.Parse(s[1]);
                    break;
                case "EUR":
                    s = textPrice.Split('&');
                    result = Double.Parse(s[0]);
                    break;
            }
            return result;
        }

        public int convertAmount(string amount)
        {
            int result = 0;

            amount = amount.Replace(",", "");
            result = Int32.Parse(amount);

            return result;
        }
    }
}

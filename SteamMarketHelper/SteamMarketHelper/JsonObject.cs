﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamMarketHelper
{
    public class JsonObject
    {
        public bool success { get; set; }
        public string lowest_price{ get; set; }
        public string volume { get; set; }
        public string median_price { get; set; }
    }
}

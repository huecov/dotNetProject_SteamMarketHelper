using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamMarketHelper
{
    public enum Currency
    {
        USD,
        GBP,
        EUR,
    }

    public enum Exterior
    {
        Factory_New,
        Minimal_Wear,
        Field0Tested,
        Well0Worn,
        Battle0Scarred
    }

    public enum Quality
    {
        Consumer_Grade,
        Industrial_Grade,
        Mil_Spec_Grade,
        Restricted_Grade,
        Classified_Grade,
        Covert
    }
}

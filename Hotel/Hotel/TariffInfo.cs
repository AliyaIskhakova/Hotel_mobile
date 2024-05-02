using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel
{
    public class TariffInfo
    {
        public int TariffID { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Food { get; set; }
        public string Gym { get; set; }
        public string Transfer { get; set; }
        public string Wifi { get; set; }
        public int Cost { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtYarisi
{
    internal class istatistik
    {
        public static class Istatistik
        {
            public static int ToplamBahisSayisi { get; set; } = 0;
            public static int KazanilanBahisSayisi { get; set; } = 0;
            public static decimal ToplamKazanilanPara { get; set; } = 0;
            public static decimal ToplamKaybedilenPara { get; set; } = 0;

            public static int KaybedilenBahisSayisi => ToplamBahisSayisi - KazanilanBahisSayisi;
        }

    }
}

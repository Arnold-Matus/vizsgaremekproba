using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace suliradio
{
    public class CsengetesiRend
    {
        public int SzunetSorszam { get; set; }
        public TimeSpan SzunetKezdet { get; set; }
        public TimeSpan SzunetVege { get; set; }
        public CsengetesiRend(int szunetSorszam, TimeSpan szunetKezdet, TimeSpan szunetVege)
        {
            SzunetSorszam = szunetSorszam;
            SzunetKezdet = szunetKezdet;
            SzunetVege = szunetVege;
        }
    }
}

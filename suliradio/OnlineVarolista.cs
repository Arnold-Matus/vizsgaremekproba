using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace suliradio
{
    public class OnlineVarolista
    {
        public int Id { get; set; }
        public int ZeneId { get; set; }
        public TimeSpan Mettol { get; set; }
        public TimeSpan Meddig { get; set; }
        public OnlineVarolista(int id, int zeneId, TimeSpan mettol, TimeSpan meddig)
        {
            Id = id;
            ZeneId = zeneId;
            Mettol = mettol;
            Meddig = meddig;
        }
        public OnlineVarolista(List<(int id, int zeneid, TimeSpan mettol, TimeSpan meddig)> onlineVarolista)
        {
            foreach (var item in onlineVarolista)
            {
                new OnlineVarolista(item.id, item.zeneid, item.mettol, item.meddig);
            }
        }
    }
}

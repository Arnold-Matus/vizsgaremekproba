using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TagLib;
using System.Security.Cryptography.X509Certificates;

namespace suliradio
{
    public class Zenek
    {
        public int ZeneID { get; set; } 
        public string EleresiUt { get; set; } //ha https-sel kezdődik, letölti, ha nem, akkor megpróbálja megnyitni a fájlrendszerben
        //a webes bigyo is leelenőrzi, hogy már lejátszható e, be lehet-e kérni
        public string Eloado { get; set; } 
        public string Cim { get; set; }
        public string FajlNev { get; set; }
        public TimeSpan Hossz { get; set; }
        public enum Forras{ Mappa, YouTube, Spotify, Bemondas }
        public Forras ZeneForrasa { get; set; }
        public TimeSpan Mikortol { get; set; }
        public TimeSpan Meddig { get; set; }
        //id vagy index list
        //public Zenek(int zeneID=2, string eleresiUt="c:", string eloado="aaqwasy", string cim, string fajlNev, TimeSpan hossz, Forras zeneForrasa) { ZeneID = zeneID; EleresiUt = eleresiUt; Eloado = eloado; Cim = cim; FajlNev = fajlNev; Hossz = hossz; ZeneForrasa = zeneForrasa; }
        public Zenek(int zeneID, string eleresiUt, string eloado, string cim, string fajlNev, TimeSpan hossz, Forras zeneForrasa) {
            ZeneID = zeneID;
            EleresiUt = eleresiUt; 
            Eloado = eloado;
            Cim = cim;
            FajlNev = fajlNev;
            Hossz = hossz;
            ZeneForrasa = zeneForrasa; 
        }
        public Zenek(string eleresiUt, string eloado, string cim, string fajlNev, TimeSpan hossz, Forras zeneForrasa)
        {
            ZeneID = 0;
            EleresiUt = eleresiUt;
            Eloado = eloado;
            Cim = cim;
            FajlNev = fajlNev;
            Hossz = hossz;
            ZeneForrasa = zeneForrasa;
        }
        public Zenek(string eleresiUt, string eloado, string cim, TimeSpan hossz, Forras zeneForrasa, TimeSpan mikortol, TimeSpan meddig)
        {
            ZeneID = 0;
            EleresiUt = eleresiUt;
            Eloado = eloado;
            Cim = cim;
            FajlNev = Path.GetFileName(eleresiUt);
            Hossz = hossz;
            ZeneForrasa = zeneForrasa;
            Mikortol = mikortol;
            Meddig = meddig;
        }
        public Zenek(int zeneid, string eleresiUt, string eloado, string cim, TimeSpan hossz, Forras zeneForrasa, TimeSpan mikortol, TimeSpan meddig)
        {
            ZeneID = zeneid;
            EleresiUt = eleresiUt;
            Eloado = eloado;
            Cim = cim;
            FajlNev = Path.GetFileName(eleresiUt);
            Hossz = hossz;
            ZeneForrasa = zeneForrasa;
            Mikortol = mikortol;
            Meddig = meddig;
        }

        public Zenek() { }

        public static List<Zenek> Fajlbeolvas(string eleresiut) {
            List<Zenek> Zenelista = new List<Zenek>();
            string[] fajlok = Directory.GetFiles(eleresiut, "*.*", SearchOption.AllDirectories)
                .Where(file => file.EndsWith(".mp3") || file.EndsWith(".wav") || file.EndsWith(".flac") || file.EndsWith(".ogg"))
                .ToArray();

            int id = 1;

            foreach (var fajl in fajlok)
            {
                try
                {
                    var tagFile = TagLib.File.Create(fajl);
                    string eloado = tagFile.Tag.FirstPerformer ?? tagFile.Tag.JoinedPerformers ?? "Ismeretlen előadó";
                    string cim = tagFile.Tag.Title ?? Path.GetFileNameWithoutExtension(fajl);
                    TimeSpan hossz = tagFile.Properties.Duration;
                    Zenek zene = new Zenek(id, fajl, eloado, cim, Path.GetFileName(fajl), hossz, Forras.Mappa);
                    Zenelista.Add(zene);
                    id++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hiba a fájl beolvasásakor: {fajl}. Hiba: {ex.Message}");
                }
            }

            return Zenelista;
        }
        /*public static Zenek EgyFajlbeolvas(string fajlEleresiUt)
        {
            Zenek zene = new Zenek();
            try
            {
                var tagFile = TagLib.File.Create(fajlEleresiUt);
                string eloado = tagFile.Tag.FirstPerformer ?? tagFile.Tag.JoinedPerformers ?? "Ismeretlen előadó";
                string cim = tagFile.Tag.Title ?? Path.GetFileNameWithoutExtension(fajlEleresiUt);
                TimeSpan hossz = tagFile.Properties.Duration;
                zene = new Zenek(1, fajlEleresiUt, eloado, cim, Path.GetFileName(fajlEleresiUt), hossz, Forras.Mappa);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a fájl beolvasásakor. Hiba: {ex.Message}");
            }

            return zene;
        }*/

    }
}

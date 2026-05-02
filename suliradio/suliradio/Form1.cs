using System.Diagnostics.Eventing.Reader;
using System.Reflection.Metadata.Ecma335;
using System.Timers;
using System.Xml.Linq;
using static suliradio.Zenek;

namespace suliradio
{
    public partial class Form1 : Form
    {
        private Lejatszo Lejatszo;

        public Form1()
        {
            Instance = this;
            InitializeComponent();
            Lejatszo = new Lejatszo();
        }
        public static Form1 Instance { get; private set; }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Instance = this;
        }

        public List<Zenek> Zenelista { get; set; } = new List<Zenek>();
        public List<Zenek> Varolista { get; set; } = new List<Zenek>();
        public List<CsengetesiRend> CsengetesiRend { get; set; } = new List<CsengetesiRend>();//Szóval, a CsengetesiRend osztály valóban nem csengetési rendeknek a tulajdonságait tartalmazza, hanem egy-egy szünet adatait :/ Csak furán neveztem el
        public bool CsengetesiRendAktiv { get; set; } = false;
        private System.Timers.Timer Timer;
        private bool CsengetesiMotorRunning = false;
        private bool LejatszasIdoRunning = false;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void zenemappaHelyénekMegadásaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Mappa kiválasztása
            ///regi nem biztonsagos modszer inkabb commonopenfiledialog
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Válaszd ki a zenemappát";
                dialog.UseDescriptionForTitle = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = dialog.SelectedPath;
                    //beolvasás a megadott mappából a listában lévõ meglévõ elemek megtartásával
                    //a megegyezõ elõadõjú és címû zenék ne kerüljenek be kétszer a listába
                    //régi rossz: Zenelista = Zenek.Fajlbeolvas(selectedPath);
                    List<Zenek> ujZenek = Zenek.Fajlbeolvas(selectedPath);
                    foreach (var zene in ujZenek)
                    {
                        if (!Zenelista.Any(z => z.Eloado == zene.Eloado && z.Cim == zene.Cim))
                        {
                            Zenelista.Add(zene);
                        }
                    }
                    MessageBox.Show($"Kiválasztott mappa:\n{selectedPath}");
                    ZenelistaFrissit(Zenelista);

                }
            }
        }

        public void BeallitasMento()
        {
            //beallitasok mentese egy fájlba, hogy legközelebb is meglegyenek
            //pl csengetesi rend=>csengetes.csv, zenelista=>zenelista.csv
            StreamWriter writer = new StreamWriter("csengetes.csv");
            foreach (var szunet in CsengetesiRend)
            {
                writer.WriteLine($"{szunet.SzunetSorszam};{szunet.SzunetKezdet};{szunet.SzunetVege}");
            }
            writer.Close();
            StreamWriter writer2 = new StreamWriter("zenelista.csv");
            foreach (var zene in Zenelista)
            {
                writer2.WriteLine($"{zene.ZeneID};{zene.EleresiUt};{zene.Eloado};{zene.Cim};{zene.FajlNev};{zene.Hossz};{zene.ZeneForrasa}");
            }
            writer2.Close();
            StreamWriter writer3 = new StreamWriter("varolista.csv");
            foreach (var zene in Varolista)
            {
                writer3.WriteLine($"{zene.ZeneID};{zene.EleresiUt};{zene.Eloado};{zene.Cim};{zene.FajlNev};{zene.Hossz};{zene.ZeneForrasa}");
            }
            writer3.Close();
        }

        public void BeallitasBetolto()
        {
            try
            {
                CsengetesiRend.Clear();
                Zenelista.Clear();
                Varolista.Clear();
                using (StreamReader reader = new StreamReader("csengetes.csv"))
                {
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(';');
                        if (parts.Length >= 3 &&
                            int.TryParse(parts[0], out int sorszam) &&
                            TimeSpan.TryParse(parts[1], out TimeSpan kezdet) &&
                            TimeSpan.TryParse(parts[2], out TimeSpan vege))
                        {
                            CsengetesiRend.Add(new CsengetesiRend(sorszam, kezdet, vege));
                        }
                    }
                }

                using (StreamReader reader2 = new StreamReader("zenelista.csv"))
                {
                    string? line2;
                    while ((line2 = reader2.ReadLine()) != null)
                    {
                        string[] parts = line2.Split(';');
                        if (parts.Length >= 7 &&
                            int.TryParse(parts[0], out int zeneID) &&
                            TimeSpan.TryParse(parts[5], out TimeSpan hossz) &&
                            Enum.TryParse<Zenek.Forras>(parts[6], out Zenek.Forras zeneForrasa))
                        {
                            string eleresiUt = parts[1];
                            string eloado = parts[2];
                            string cim = parts[3];
                            string fajlNev = parts[4];
                            Zenelista.Add(new Zenek(zeneID, eleresiUt, eloado, cim, fajlNev, hossz, zeneForrasa));
                        }
                    }
                }
                using (StreamReader reader3 = new StreamReader("varolista.csv"))
                {
                    string? line3;
                    while ((line3 = reader3.ReadLine()) != null)
                    {
                        string[] parts = line3.Split(';');
                        if (parts.Length >= 7 &&
                            int.TryParse(parts[0], out int zeneID) &&
                            TimeSpan.TryParse(parts[5], out TimeSpan hossz) &&
                            Enum.TryParse<Zenek.Forras>(parts[6], out Zenek.Forras zeneForrasa))
                        {
                            string eleresiUt = parts[1];
                            string eloado = parts[2];
                            string cim = parts[3];
                            string fajlNev = parts[4];
                            Varolista.Add(new Zenek(zeneID, eleresiUt, eloado, cim, fajlNev, hossz, zeneForrasa));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba a beállítások betöltésekor: {ex.Message}");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public async Task CsengetesiMotor(List<CsengetesiRend> CsegnetesiRend)
        {
            while (true)
            {
                if (CsengetesiRendAktiv == true)
                {
                    TimeSpan aktualisIdo = DateTime.Now.TimeOfDay;
                    foreach (var szunet in CsengetesiRend)
                    {
                        if (aktualisIdo >= szunet.SzunetKezdet && aktualisIdo <= szunet.SzunetVege)
                        {
                            if (!Lejatszo.IsPlaying)
                            {
                                var lastZene = Varolista[0];
                                var eerer = Zenelista.Find(x => x.Cim == lastZene.Cim);
                                if (eerer != null)
                                {
                                    string eleresiut = eerer.EleresiUt;
                                    Lejatszo.Play(eleresiut);
                                    AktualisZeneFrissit(eerer);
                                    Varolista.RemoveAt(0);
                                    VaroListaFrissit(Varolista);
                                }
                            }
                        }
                        else
                        {
                            Lejatszo.FadeOutAndStop();
                            AktualisZeneFrissit();
                        }
                    }
                }
                await Task.Delay(1000);
            }
        }
        public void ZenelistaFrissit(List<Zenek> Zenelista)
        {
            lv_zenelista.Items.Clear();
            lv_zenelista.Columns.Clear();
            lv_zenelista.View = View.Details;
            lv_zenelista.Columns.Add("Elõadó", 150);
            lv_zenelista.Columns.Add("Cím", 150);
            lv_zenelista.Columns.Add("Hossz", 100);
            lv_zenelista.FullRowSelect = true;

            foreach (var zene in Zenelista)
            {
                ListViewItem item = new ListViewItem(zene.Eloado);
                item.SubItems.Add(zene.Cim);
                item.SubItems.Add(zene.Hossz.ToString(@"mm\:ss"));
                lv_zenelista.Items.Add(item);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv_zenelista.SelectedItems.Count != 0)
            {
                tb_kijeloltcim.Text = lv_zenelista.SelectedItems[0].SubItems[1].Text;
                tb_kijelolteloado.Text = lv_zenelista.SelectedItems[0].SubItems[0].Text;
            }
        }

        private void zenemappaBezárásaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Biztosan törlöd a zenelistát? A fájlok megmaradnak, de az összes betöltött zene utvonalát és a várólistát elfelejti a szoftver.", "Zenemappa bezárása", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                lv_zenelista.Items.Clear();
                lv_varolista.Items.Clear();
                Zenelista.Clear();
                Varolista.Clear();
                ZenelistaFrissit(Zenelista);
                VaroListaFrissit(Varolista);
                MessageBox.Show("Zenemappa bezárva, zenelista törölve");
            }
        }

        private void bt_hozzaad_Click(object sender, EventArgs e)
        {
            if (lv_zenelista.SelectedItems.Count == 0)
            {
                MessageBox.Show("Nincs kijelölt zene!"); return;
            }
            else
            {
                var eerer = Zenelista.Find(x => x.Cim == lv_zenelista.SelectedItems[0].SubItems[1].Text);
                if (eerer is null) { MessageBox.Show("Valami hiba történt, nem találom a zenét a listában"); return; }
                else { Varolista.Add(eerer); VaroListaFrissit(Varolista); } //várólista: legyen hozzáadás idõpontja is, hogy késõbb lehessen rendezni
                                                                            //legyen a listbox lecserélve 
            }


        }
        public void VaroListaFrissit(List<Zenek> Varolista)
        {
            lv_varolista.Items.Clear();
            lv_varolista.Columns.Clear();
            lv_varolista.View = View.Details;
            lv_varolista.Columns.Add("Elõadó", 150);
            lv_varolista.Columns.Add("Cím", 150);
            lv_varolista.Columns.Add("Hossz", 100);
            lv_varolista.FullRowSelect = true;
            foreach (var zene in Varolista)
            {
                ListViewItem item = new ListViewItem(zene.Eloado);
                item.SubItems.Add(zene.Cim);
                item.SubItems.Add(zene.Hossz.ToString(@"mm\:ss"));
                lv_varolista.Items.Add(item);
            }
        }

        public void AktualisZeneFrissit(Zenek aktualisZene)
        {
            tb_cim.Text = aktualisZene.Cim;
            tb_eloado.Text = aktualisZene.Eloado;
        }
        public void AktualisZeneFrissit()
        {
            tb_cim.Text = "Cím";
            tb_eloado.Text = "Elõadó";
        }

        public void LetoltottZeneHozzaad(string substr)
        {
            List<Zenek> ujZenek = Zenek.Fajlbeolvas("zenek");
            var tagFile = TagLib.File.Create(substr);
            string eloado = tagFile.Tag.FirstPerformer ?? tagFile.Tag.JoinedPerformers ?? "Ismeretlen elõadó";
            string cim = tagFile.Tag.Title ?? Path.GetFileNameWithoutExtension(substr);
            TimeSpan hossz = tagFile.Properties.Duration;
            Zenek zene = new Zenek(substr, eloado, cim, Path.GetFileName(substr), hossz, Forras.Mappa);
            Zenelista.Add(zene);
            if (!Zenelista.Any(z => z.Eloado == zene.Eloado && z.Cim == zene.Cim))
            {
                Zenelista.Add(zene);
            }
            if (!Varolista.Any(z => z.Eloado == zene.Eloado && z.Cim == zene.Cim))
            {
                Varolista.Add(zene);
            }
            ZenelistaFrissit(Zenelista);
            VaroListaFrissit(Varolista);
        }

        private void bt_eltavolit_Click(object sender, EventArgs e)
        {
            if (lv_varolista.SelectedItems.Count == 0) { MessageBox.Show("Nincs kijelölt zene!"); return; }
            else
            {
                //index alapján eltávolítjuk a várólistából az lv_varolisa-n kijelölt elemet
                Varolista.RemoveAt(lv_varolista.SelectedIndices[0]);
                VaroListaFrissit(Varolista);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Lejatszo.IsPlaying == false && lv_varolista.SelectedItems.Count > 0)
            {
                var eerer = Zenelista.Find(x => x.Cim == lv_varolista.SelectedItems[0].SubItems[1].Text); //Hibakezelés szükséges
                string eleresiut = eerer.EleresiUt;
                Lejatszo.Play(eleresiut);
                AktualisZeneFrissit(eerer);
            }
            else
            {
                Lejatszo.FadeOutAndStop();
                AktualisZeneFrissit();
            }
            if (LejatszasIdoRunning == false) {
                LejatszasIdoRunning = true;
                _ = LejatszasIdo();
            }
        }

        private void csengetésiRendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            beallitasok1 beallitasok = new beallitasok1(this);
            beallitasok.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (CsengetesiRendAktiv == true)
            {
                CsengetesiRendAktiv = false;
                button3.Text = "Automatikus lejátszás: KI";
                button3.BackColor = Color.DarkGray;
                CsengetesiMotorRunning = false;
                _ = Lejatszo.FadeOutAndStop();
            }
            else
            {
                if (!CsengetesiMotorRunning)
                {
                    CsengetesiRendAktiv = true;
                    button3.Text = "Automatikus lejátszás: BE";
                    button3.BackColor = Color.DarkGreen;
                    CsengetesiMotorRunning = true;
                    _ = CsengetesiMotor(CsengetesiRend);
                }
            }
            if (LejatszasIdoRunning == false)
            {
                LejatszasIdoRunning = true;
                _ = LejatszasIdo();
            }
        }

        private void letöltöttZenékMegnyitásaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Zenek> ujZenek = Zenek.Fajlbeolvas("zenek");
            foreach (var zene in ujZenek)
            {
                if (!Zenelista.Any(z => z.Eloado == zene.Eloado && z.Cim == zene.Cim))
                {
                    Zenelista.Add(zene);
                }
            }
            MessageBox.Show($"Kiválasztott mappa: Letöltött zenék mappája");
            ZenelistaFrissit(Zenelista);
        }

        private void zeneletöltõToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //letolto form megnyitása:
            Letolto letolto = new Letolto();
            letolto.ShowDialog();
        }

        private void beállításokBetöltéseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Megerõsítés messagebox:
            DialogResult result = MessageBox.Show("Biztosan betöltöd a beállításokat? Ezzel felülírod a jelenlegi beállításokat!", "Beállítások betöltése", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            BeallitasBetolto();
            ZenelistaFrissit(Zenelista);
            VaroListaFrissit(Varolista);
        }

        private void beállításokMentéseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeallitasMento();
        }

        private void kilépésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fájlToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void zenelistaFrissítéseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZenelistaFrissit(Zenelista);
            VaroListaFrissit(Varolista);
        }

        private void lb_idosav_Click(object sender, EventArgs e)
        {
            lb_idosav.Text = Lejatszo.CurrentTime().ToString();
        }
        public async Task LejatszasIdo() {
            while (LejatszasIdoRunning == true) {
                if (Lejatszo.IsPlaying == true)
                {
                    lb_idosav.Text = Lejatszo.CurrentTime().ToString();
                }
                await Task.Delay(200);
            }

        }
    }
}

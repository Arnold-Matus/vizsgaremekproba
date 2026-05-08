using System.Diagnostics.Eventing.Reader;
using System.Reflection.Metadata.Ecma335;
using System.Timers;
using System.Xml.Linq;
using static suliradio.Zenek;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace suliradio
{
    public partial class Form1 : Form
    {
        private Lejatszo Lejatszo;
        private ApiKeresek ApiKeresek;
        private string _currentToken = "";

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
        public List<(int id, int zeneid, TimeSpan mettol, TimeSpan meddig)> OnlineVarolista { get; set; } = new List<(int id, int zeneid, TimeSpan mettol, TimeSpan meddig)>();
        public List<CsengetesiRend> CsengetesiRend { get; set; } = new List<CsengetesiRend>();
        public bool CsengetesiRendAktiv { get; set; } = false;
        private System.Timers.Timer Timer;
        private bool CsengetesiMotorRunning = false;
        private bool LejatszasIdoRunning = false;
        public TimeSpan CsengoCsuszas = TimeSpan.Zero;
        public bool AdatbazisHasznalat { get; set; } = false;

        private void label1_Click(object sender, EventArgs e) { }

        private void groupBox1_Enter(object sender, EventArgs e) { }

        private void groupBox2_Enter(object sender, EventArgs e) { }

        private async void zenemappaHelyénekMegadásaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Mappa kiválasztása
            ///regi nem biztonsagos modszer inkabb commonopenfiledialog
            if (AdatbazisHasznalat == true)
            {
                if (ApiKeresek == null)
                {
                    MessageBox.Show("Nincs bejelentkezett felhasználó. Kérlek jelentkezz be online mód használatához.", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (FolderBrowserDialog dialog = new FolderBrowserDialog())
                {
                    dialog.Description = "Válaszd ki a feltöltendõ zenemappát";
                    dialog.UseDescriptionForTitle = true;

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        string selectedPath = dialog.SelectedPath;
                        List<Zenek> ujZenek = Zenek.Fajlbeolvas(selectedPath);

                        int successCount = 0;
                        int failCount = 0;
                        var failMessages = new List<string>();

                        foreach (var zene in ujZenek)
                        {
                            try
                            {
                                // Prepare payload according to backend expectations
                                var payload = new Dictionary<string, object>
                                {
                                    ["zeneurl"] = zene.EleresiUt ?? string.Empty,
                                    ["eloado"] = zene.Eloado ?? string.Empty,
                                    ["cim"] = zene.Cim ?? string.Empty,
                                    ["hossz"] = (int)zene.Hossz.TotalSeconds,
                                    ["lejatszhatoe"] = 1,
                                    ["tema"] = string.Empty,
                                    ["keresurl"] = zene.EleresiUt ?? string.Empty
                                };
                                //if (zene.Eloado != null) MessageBox.Show($"{zene.Eloado}");

                                bool siker = await ApiKeresek.ZeneFeltoltes(payload);
                                if (siker)
                                {
                                    successCount++;
                                    // Add to local list if not present
                                    if (!Zenelista.Any(z => z.Eloado == zene.Eloado && z.Cim == zene.Cim))
                                    {
                                        Zenelista.Add(zene);
                                    }
                                }
                                else
                                {
                                    failCount++;
                                    failMessages.Add($"{zene.Cim} - {zene.Eloado}");
                                }
                            }
                            catch (Exception ex)
                            {
                                failCount++;
                                failMessages.Add($"{zene.Cim} - {zene.Eloado}: {ex.Message}");
                            }
                        }

                        ZenelistaFrissit(Zenelista);

                        string msg = $"Feltöltés befejezõdött. Sikeres: {successCount}, Sikertelen: {failCount}.";
                        if (failCount > 0)
                        {
                            msg += "\nSikertelen tételek:\n" + string.Join("\n", failMessages.Take(10));
                        }
                        MessageBox.Show(msg, "Feltöltés eredménye", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                try
                {
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
                catch (Exception ex)
                {
                    MessageBox.Show($"Hiba a mappa kiválasztásakor: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void BeallitasMento()
        {
            try
            {
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

                MessageBox.Show("Beállítások mentve!", "Siker", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba a beállítások mentésekor: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                MessageBox.Show($"Hiba a beállítások betöltésekor: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void OnlineBeallitasBetolto()
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
                MessageBox.Show($"Hiba a beállítások betöltésekor: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                BeallitasBetolto();
                ZenelistaFrissit(Zenelista);
                VaroListaFrissit(Varolista);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba a beállítások betöltésekor: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async Task CsengetesiMotor(List<CsengetesiRend> CsegnetesiRend)
        {
            while (true)
            {
                try
                {
                    if (CsengetesiRendAktiv == true)
                    {
                        if (AdatbazisHasznalat == true)
                        {
                            string lejatszandoEleresiUt = await ApiKeresek.LejatszandoZene();
                            if (!Lejatszo.IsPlaying && !string.IsNullOrEmpty(lejatszandoEleresiUt))
                            {
                                Lejatszo.Play(lejatszandoEleresiUt);
                                var eerer = Zenelista.Find(x => x.EleresiUt == lejatszandoEleresiUt);
                                AktualisZeneFrissit(eerer);
                                //várólista frissítése??
                            }
                        }
                        else
                        {
                            TimeSpan aktualisIdo = DateTime.Now.TimeOfDay;
                            foreach (var szunet in CsegnetesiRend)
                            {
                                if (aktualisIdo >= (szunet.SzunetKezdet + CsengoCsuszas) && aktualisIdo <= (szunet.SzunetVege + CsengoCsuszas))
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
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hiba a csengetõ motorban: {ex.Message}");
                }
                await Task.Delay(1000);
            }
        }

        public void ZenelistaFrissit(List<Zenek> Zenelista)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba a zenelista frissítésekor: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lv_zenelista.SelectedItems.Count != 0)
                {
                    tb_kijeloltcim.Text = lv_zenelista.SelectedItems[0].SubItems[1].Text;
                    tb_kijelolteloado.Text = lv_zenelista.SelectedItems[0].SubItems[0].Text;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a zene kijelölésekor: {ex.Message}");
            }
        }

        private void zenemappaBezárásaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba a zenemappa bezárásakor: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void bt_hozzaad_Click(object sender, EventArgs e)
        {
            if (AdatbazisHasznalat == true)
            {
                //késõbb
                var eerer = Zenelista.Find(x => x.Cim == lv_zenelista.SelectedItems[0].SubItems[1].Text);
                bool sikerese = await ApiKeresek.ZeneKeres(eerer.EleresiUt);
                MessageBox.Show($"A bekérés {(sikerese ? "sikeres" : "sikertelen")}!\n{eerer.EleresiUt}"); //ez csak teszt, késõbb majd a keresés eredménye alapján döntünk a hozzáadásról
            }
            else
            {
                try
                {
                    if (lv_zenelista.SelectedItems.Count == 0)
                    {
                        MessageBox.Show("Nincs kijelölt zene!");
                        return;
                    }

                    var eerer = Zenelista.Find(x => x.Cim == lv_zenelista.SelectedItems[0].SubItems[1].Text);
                    if (eerer is null)
                    {
                        MessageBox.Show("Valami hiba történt, nem találom a zenét a listában");
                        return;
                    }

                    Varolista.Add(eerer);
                    VaroListaFrissit(Varolista);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hiba a zene hozzáadásakor: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void VaroListaFrissit(List<Zenek> Varolista)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba a várólistának a frissítésekor: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void AktualisZeneFrissit(Zenek aktualisZene)
        {
            try
            {
                tb_cim.Text = aktualisZene.Cim;
                tb_eloado.Text = aktualisZene.Eloado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba az aktuális zene frissítésekor: {ex.Message}");
            }
        }

        public void AktualisZeneFrissit()
        {
            tb_cim.Text = "Cím";
            tb_eloado.Text = "Elõadó";
        }

        public void LetoltottZeneHozzaad(string substr)
        {
            try
            {
                List<Zenek> ujZenek = Zenek.Fajlbeolvas("zenek");
                var tagFile = TagLib.File.Create(substr);
                string eloado = tagFile.Tag.FirstPerformer ?? tagFile.Tag.JoinedPerformers ?? "Ismeretlen elõadó";
                string cim = tagFile.Tag.Title ?? Path.GetFileNameWithoutExtension(substr);
                TimeSpan hossz = tagFile.Properties.Duration;
                Zenek zene = new Zenek(substr, eloado, cim, Path.GetFileName(substr), hossz, Forras.Mappa);

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
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba a letöltött zene hozzáadásakor: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bt_eltavolit_Click(object sender, EventArgs e)
        {
            try
            {
                if (lv_varolista.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Nincs kijelölt zene!");
                    return;
                }

                Varolista.RemoveAt(lv_varolista.SelectedIndices[0]);
                VaroListaFrissit(Varolista);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba a zene eltávolításakor: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Lejatszo.IsPlaying == false && lv_varolista.SelectedItems.Count > 0)
                {
                    var eerer = Zenelista.Find(x => x.Cim == lv_varolista.SelectedItems[0].SubItems[1].Text);
                    if (eerer == null)
                    {
                        MessageBox.Show("Nem találom a kijelölt zenét!");
                        return;
                    }

                    string eleresiut = eerer.EleresiUt;
                    Lejatszo.Play(eleresiut);
                    AktualisZeneFrissit(eerer);
                }
                else
                {
                    Lejatszo.FadeOutAndStop();
                    AktualisZeneFrissit();
                }

                if (LejatszasIdoRunning == false)
                {
                    LejatszasIdoRunning = true;
                    _ = LejatszasIdo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba a lejátszáskor: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void csengetésiRendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (CsengetesiRendAktiv == true && AdatbazisHasznalat == false)
                {
                    CsengetesiRendAktiv = false;
                    button3.Text = "Automatikus lejátszás: KI";
                    button3.BackColor = Color.DarkGray;
                    CsengetesiMotorRunning = false;
                    _ = Lejatszo.FadeOutAndStop();
                }
                else
                {
                    if (!CsengetesiMotorRunning && AdatbazisHasznalat == false)
                    {
                        CsengetesiRendAktiv = true;
                        button3.Text = "Automatikus lejátszás: BE";
                        button3.BackColor = Color.DarkGreen;
                        CsengetesiMotorRunning = true;
                        _ = CsengetesiMotor(CsengetesiRend);
                    }
                }
                if (CsengetesiRendAktiv == true && AdatbazisHasznalat == true)
                {
                    CsengetesiRendAktiv = false;
                    button3.Text = "Automatikus lejátszás: KI";
                    button3.BackColor = Color.DarkGray;
                    CsengetesiMotorRunning = false;
                    //ide
                }
                else
                {
                    if (!CsengetesiMotorRunning && AdatbazisHasznalat == true)
                    {
                        CsengetesiRendAktiv = true;
                        button3.Text = "Automatikus lejátszás: BE";
                        button3.BackColor = Color.DarkGreen;
                        CsengetesiMotorRunning = true;
                        //ide
                    }
                }

                if (LejatszasIdoRunning == false)
                {
                    LejatszasIdoRunning = true;
                    _ = LejatszasIdo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba az automatikus lejátszáskor: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void letöltöttZenékMegnyitásaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AdatbazisHasznalat == true)
            {
                if (ApiKeresek == null)
                {
                    MessageBox.Show("Nincs bejelentkezett felhasználó. Kérlek jelentkezz be online mód használatához.", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string selectedPath = "zenek";
                List<Zenek> ujZenek = Zenek.Fajlbeolvas(selectedPath);

                int successCount = 0;
                int failCount = 0;
                var failMessages = new List<string>();

                foreach (var zene in ujZenek)
                {
                    try
                    {
                        // Prepare payload according to backend expectations
                        var payload = new Dictionary<string, object>
                        {
                            ["zeneurl"] = zene.EleresiUt ?? string.Empty,
                            ["eloado"] = zene.Eloado ?? string.Empty,
                            ["cim"] = zene.Cim ?? string.Empty,
                            ["hossz"] = (int)zene.Hossz.TotalSeconds,
                            ["lejatszhatoe"] = 1,
                            ["tema"] = string.Empty,
                            ["keresurl"] = zene.EleresiUt ?? string.Empty
                        };
                        //if (zene.Eloado != null) MessageBox.Show($"{zene.Eloado}");

                        bool siker = await ApiKeresek.ZeneFeltoltes(payload);
                        if (siker)
                        {
                            successCount++;
                            // Add to local list if not present
                            if (!Zenelista.Any(z => z.Eloado == zene.Eloado && z.Cim == zene.Cim))
                            {
                                Zenelista.Add(zene);
                            }
                        }
                        else
                        {
                            failCount++;
                            failMessages.Add($"{zene.Cim} - {zene.Eloado}");
                        }
                    }
                    catch (Exception ex)
                    {
                        failCount++;
                        failMessages.Add($"{zene.Cim} - {zene.Eloado}: {ex.Message}");
                    }
                }

                ZenelistaFrissit(Zenelista);

                string msg = $"Feltöltés befejezõdött. Sikeres: {successCount}, Sikertelen: {failCount}.";
                if (failCount > 0)
                {
                    msg += "\nSikertelen tételek:\n" + string.Join("\n", failMessages.Take(10));
                }
                MessageBox.Show(msg, "Feltöltés eredménye", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    List<Zenek> ujZenek = new List<Zenek>();

                    if (AdatbazisHasznalat && ApiKeresek != null)
                    {
                        ujZenek = await ApiKeresek.LejatszhatoZenek();
                    }
                    else
                    {
                        ujZenek = Zenek.Fajlbeolvas("zenek");
                    }

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
                catch (Exception ex)
                {
                    MessageBox.Show($"Hiba a letöltött zenék megnyitásakor: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void zeneletöltõToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Letolto letolto = new Letolto();
                letolto.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba a zeneletöltõ megnyitásakor: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void beállításokBetöltéseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AdatbazisHasznalat)
            {
                try
                {
                    DialogResult result = MessageBox.Show("Biztosan betöltöd a beállításokat? Ezzel felülírod a jelenlegi beállításokat!", "Beállítások betöltése", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        //ide jön a beállítások betöltése a backendrõl
                        Zenelista.Clear();
                        Zenelista = await ApiKeresek.LejatszhatoZenek();//ezittaz
                        ZenelistaFrissit(Zenelista);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hiba a beállítások betöltésekor: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    DialogResult result = MessageBox.Show("Biztosan betöltöd a beállításokat? Ezzel felülírod a jelenlegi beállításokat!", "Beállítások betöltése", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        BeallitasBetolto();
                        ZenelistaFrissit(Zenelista);
                        VaroListaFrissit(Varolista);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hiba a beállítások betöltésekor: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void beállításokMentéseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                BeallitasMento();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba a beállítások mentésekor: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void kilépésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (AdatbazisHasznalat && ApiKeresek != null)
                {
                    await ApiKeresek.Kilepes(); 
                }
                if (AdatbazisHasznalat == false)
                {
                    BeallitasMento();
                }
                this.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a kilépéskor: {ex.Message}");
                this.Close();
            }
        }

        private void fájlToolStripMenuItem_Click(object sender, EventArgs e) { }

        private void zenelistaFrissítéseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ZenelistaFrissit(Zenelista);
                VaroListaFrissit(Varolista);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba a zenelista frissítésekor: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lb_idosav_Click(object sender, EventArgs e)
        {
            try
            {
                lb_idosav.Text = Lejatszo.CurrentTime().ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba az idõ lekérésekor: {ex.Message}");
            }
        }

        public async Task LejatszasIdo()
        {
            while (LejatszasIdoRunning == true)
            {
                try
                {
                    if (Lejatszo.IsPlaying == true)
                    {
                        lb_idosav.Text = Lejatszo.CurrentTime().ToString();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hiba az lejátszás idejének frissítésekor: {ex.Message}");
                }
                await Task.Delay(200);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (AdatbazisHasznalat == true)
                {
                    if (ApiKeresek != null)
                    {
                        bool kilepesiSiker = await ApiKeresek.Kilepes();
                        if (kilepesiSiker || true) // true-val megadjuk, hogy mûködjön offline módban is
                        {
                            AdatbazisHasznalat = false;
                            ApiKeresek = null;
                            _currentToken = "";
                            button1.Text = "Offline mód";
                            button1.BackColor = Color.DarkGray;
                            BeallitasBetolto();
                            ZenelistaFrissit(Zenelista);
                            VaroListaFrissit(Varolista);
                            MessageBox.Show("Kijelentkeztettél!", "Siker", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Task _ = null;
                            Task __ = null;
                        }
                    }
                    else
                    {
                        AdatbazisHasznalat = false;
                        button1.Text = "Offline mód";
                        button1.BackColor = Color.DarkGray;
                    }
                }
                else
                {
                    // Bejelentkezési form megnyitása
                    BejelentkezesForm bejelentkezesForm = new BejelentkezesForm();
                    if (bejelentkezesForm.ShowDialog() == DialogResult.OK)
                    {
                        _currentToken = bejelentkezesForm.Token;
                        ApiKeresek = new ApiKeresek(_currentToken);

                        bool tesztSiker = await ApiKeresek.Teszt();
                        if (tesztSiker)
                        {
                            AdatbazisHasznalat = true;
                            button1.Text = "Online mód";
                            button1.BackColor = Color.DarkBlue;
                            Varolista.Clear();
                            Zenelista.Clear();
                            CsengetesiRend.Clear();
                            Zenelista = await ApiKeresek.LejatszhatoZenek();
                            VaroListaFrissit(Varolista);
                            ZenelistaFrissit(Zenelista);
                            Task _ = OnlineLetolto();
                            Task __ = OnlineVaroListaFrissit();
                            MessageBox.Show("Sikeresen bejelentkeztettél!", "Siker", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Backend kapcsolódási hiba!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            AdatbazisHasznalat = false;
                            ApiKeresek = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba az online/offline módváltáskor: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AdatbazisHasznalat = false;
            }
        }

        private void beállításokToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CsengetesiRendAktiv == true | Lejatszo.IsPlaying)
            {
                if (MessageBox.Show("Biztosan szerkeszti a csengetési rendet? Ez megállítja a zenét és az automatikus lejátszást.", "Figyelem", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    CsengetesiRendAktiv = false;
                    button3.Text = "Automatikus lejátszás: KI";
                    button3.BackColor = Color.DarkGray;
                    CsengetesiMotorRunning = false;
                    _ = Lejatszo.FadeOutAndStop();
                }
                else
                {
                    return;
                }
            }
            

            try
            {
                beallitasok1 beallitasok = new beallitasok1(this, ApiKeresek);
                beallitasok.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba a beállítások megnyitásakor: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void kérésekJóváhagyásaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AdatbazisHasznalat == true)
            {
                //Jovahagyo form megnyitása
                try
                {
                    Jovahagyo jovahagyo = new Jovahagyo(ApiKeresek);
                    jovahagyo.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hiba a kérések jóváhagyása megnyitásakor: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Kérések jóváhagyása csak online módban érhetõ el!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public async Task OnlineLetolto()
        {
            if (ApiKeresek == null)
                return;

            // Ensure ZeneLetolto can call back to API if needed
            ZeneLetolto.BeallitApiKeresek(ApiKeresek);

            // Download directory
            string downloadFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "zenek");
            try
            {
                if (!System.IO.Directory.Exists(downloadFolder))
                    System.IO.Directory.CreateDirectory(downloadFolder);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Nem sikerült létrehozni a letöltési mappát: {ex.Message}");
                return;
            }

            var downloadedUrls = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            // Loop: minden 30 másodpercben lekérdezzük a várólistát és letöltjük a talált item(ek)et
            while (AdatbazisHasznalat && ApiKeresek != null)
            {
                try
                {
                    var varoLista = await ApiKeresek.VaroLista();
                    if (varoLista != null && varoLista.Count > 0)
                    {
                        // VaroLista egy List<KeyValuePair<int, string>> : List[id, keresurl]
                        foreach (var item in varoLista)
                        {
                            int zeneId = item.Key;
                            string url = item.Value;

                            if (!string.IsNullOrWhiteSpace(url))
                            {
                                // Ha már letöltöttük ezt az URL-t, kihagyjuk
                                if (downloadedUrls.Contains(url) || Zenelista.Any(z => string.Equals(z.EleresiUt, url, StringComparison.OrdinalIgnoreCase)))
                                {
                                    // már letöltve / ismert
                                }
                                else
                                {
                                    // csak webes URL-eket próbálunk letölteni
                                    if (url.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                                    {
                                        try
                                        {
                                            Console.WriteLine($"Letöltés indítása: {url}");
                                            
                                            // letöltés (await-eljük)
                                            string letoltottFajlUtvonal = await ZeneLetolto.AutoLetoltes(url, downloadFolder);
                                            
                                            if (!string.IsNullOrWhiteSpace(letoltottFajlUtvonal))
                                            {
                                                // frissítjük a backend-et az új útvonallal
                                                await ApiKeresek.ZeneUtvonalFrissitesIdAlapjan(zeneId, letoltottFajlUtvonal);
                                                
                                                // jelöljük meg, hogy letöltöttük ezt az URL-t
                                                downloadedUrls.Add(url);
                                
                                                Console.WriteLine($"Sikeresen letöltöttük: {url} -> {letoltottFajlUtvonal}");
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine($"Hiba a zene letöltésekor ({url}): {ex.Message}");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hiba az OnlineLetolto futása közben: {ex.Message}");
                }

                // Várakozás 30 másodpercig
                await Task.Delay(TimeSpan.FromSeconds(30));
            }
        }
        public async Task OnlineVaroListaFrissit() {
            bool fut = true;
            bool talalt = false;
            while (fut == true)
            {
                //Itt használja az Apikeresek.MainapiOrarend-et, és azalapján frissíti a várólistát, ha változott valami
                List<(int id, int zeneid, TimeSpan mettol, TimeSpan meddig)> FrissOnlineVarolista = await ApiKeresek.MainapiOrarend();
                foreach (var item in FrissOnlineVarolista)
                {
                    var zene = OnlineVarolista.Find(z => z.zeneid == item.zeneid);
                    if (zene == default) 
                    { 
                        talalt = true;
                    }
                }
                if (talalt) { 
                    //--
                    // Frissítjük az OnlineVarolista-t az API-ból kapott értékekkel
                    OnlineVarolista = FrissOnlineVarolista;

                    // Frissítjük a helyi várólistát és a ListView-t közvetlenül (nem a VaroListaFrissit metódussal)
                    Varolista.Clear();

                    lv_varolista.Items.Clear();
                    lv_varolista.Columns.Clear();
                    lv_varolista.View = View.Details;
                    lv_varolista.Columns.Add("Elõadó", 150);
                    lv_varolista.Columns.Add("Cím", 150);
                    lv_varolista.Columns.Add("Hossz", 100);
                    lv_varolista.FullRowSelect = true;

                    foreach (var ov in OnlineVarolista)
                    {
                        try
                        {
                            Zenek zene = null;

                            // Próbáljuk lekérni az adatokat a backendrõl az adott zeneID alapján
                            if (ApiKeresek != null)
                            {
                                try
                                {
                                    zene = await ApiKeresek.ZeneAdataiIdAlapjan(ov.zeneid);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Hiba az API zeneadat lekérésekor (id={ov.zeneid}): {ex.Message}");
                                    zene = null;
                                }
                            }

                            // Ha nem sikerült online lekérni, próbáljuk meg a helyi listából
                            if (zene == null)
                            {
                                zene = Zenelista.Find(z => z.ZeneID == ov.zeneid || string.Equals(z.EleresiUt, ov.zeneid.ToString(), StringComparison.OrdinalIgnoreCase));
                            }

                            if (zene != null)
                            {
                                // Állítsuk be az online órarend szerinti idõket
                                zene.Mikortol = ov.mettol;
                                zene.Meddig = ov.meddig;

                                // Helyi várólistába felvesszük
                                Varolista.Add(zene);

                                // Frissítjük a ListView-t
                                ListViewItem lvi = new ListViewItem(zene.Eloado);
                                lvi.SubItems.Add(zene.Cim);
                                lvi.SubItems.Add(zene.Hossz.ToString(@"mm\:ss"));
                                lv_varolista.Items.Add(lvi);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Hiba az online várólista bejárásakor: {ex.Message}");
                        }
                    }
                    //--
                }
                if (AdatbazisHasznalat == false) { fut = false; }
                await Task.Delay(10000);
            }
        }
    }
}

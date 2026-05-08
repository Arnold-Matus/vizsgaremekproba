using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace suliradio
{
    public partial class Jovahagyo : Form
    {
        // API client can be set by the caller (e.g. Form1)
        public ApiKeresek Api { get; set; }

        public Jovahagyo(ApiKeresek api)
        {
            Api = api;
            InitializeComponent();
            // wire button handler
            button1.Click += Button1_Click;
        }

        private async void Jovahagyo_Load(object sender, EventArgs e)
        {
            // start loading requests
            await KeresBetolto();
        }

        /// <summary>
        /// Betölti a backendről a kéréseket és megjeleníti a listView1-ben.
        /// </summary>
        public async Task KeresBetolto()
        {
            try
            {
                listView1.Items.Clear();
                listView1.Columns.Clear();
                listView1.View = View.Details;
                listView1.FullRowSelect = true;

                // oszlopok: ID, URL, FelhasznaloID, Validalt, Mikor
                listView1.Columns.Add("ID", 60);
                listView1.Columns.Add("URL", 320);
                listView1.Columns.Add("Felhasználó ID", 80);
                listView1.Columns.Add("Validált", 60);
                listView1.Columns.Add("Mikor", 180);

                if (Api == null)
                {
                    MessageBox.Show("Nincs beállítva ApiKeresek. Jelentkezz be előbb.", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var keresek = await Api.KeresesListazasa();
                if (keresek == null || keresek.Count == 0)
                {
                    // nincs adat
                    return;
                }

                foreach (var z in keresek)
                {
                    var id = z.ZeneID;
                    var url = z.EleresiUt ?? string.Empty;
                    var felhasznaloid = z.FajlNev ?? string.Empty; // itt tároltuk felhasznaloid-ot
                    var validalt = z.Cim ?? string.Empty; // itt tároltuk a validalte értéket
                    var mikor = z.Eloado ?? string.Empty; // itt tároltuk a mikor időt

                    var lvi = new ListViewItem(new string[] { id.ToString(), url, felhasznaloid, validalt, mikor });
                    lvi.Tag = id; // tároljuk az ID-t
                    listView1.Items.Add(lvi);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba a kérések betöltésekor: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Api == null)
                {
                    MessageBox.Show("Nincs Api beállítva. Jelentkezz be előbb.", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (listView1.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Nincs kijelölt elem!", "Figyelem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selected = listView1.SelectedItems[0];
                if (selected.Tag == null)
                {
                    MessageBox.Show("A kiválasztott elem nem tartalmaz érvényes ID-t.", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!int.TryParse(selected.Tag.ToString(), out int id))
                {
                    MessageBox.Show("A kiválasztott elem ID-je nem szám.", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var result = await Api.ZeneValidacio(id);
                if (result != null)
                {
                    MessageBox.Show($"Jóváhagyás elküldve. Backend válasz: {result}", "Siker", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // opcionálisan frissítjük a listát
                    await Task.Delay(200);
                    await KeresBetolto();
                }
                else
                {
                    MessageBox.Show("A jóváhagyás sikertelen vagy nincs válasz a backendtől.", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba a jóváhagyás küldésekor: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}

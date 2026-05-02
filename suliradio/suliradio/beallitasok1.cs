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
    public partial class beallitasok1 : Form
    {
        private Form1 parentForm;

        public beallitasok1(Form1 form1)
        {
            InitializeComponent();
            parentForm = form1;

            lv_csengetes.Items.Clear();
            lv_csengetes.Columns.Clear();
            lv_csengetes.View = View.Details;
            lv_csengetes.Columns.Add("Sorszám", 60);
            lv_csengetes.Columns.Add("Kezdet", 60);
            lv_csengetes.Columns.Add("Vége", 60);
            lv_csengetes.FullRowSelect = true;
            foreach (var szunet in parentForm.CsengetesiRend)
            {
                lv_csengetes.Items.Add(new ListViewItem(new string[] { szunet.SzunetSorszam.ToString(), szunet.SzunetKezdet.ToString(@"hh\:mm"), szunet.SzunetVege.ToString(@"hh\:mm") }));
                tb_szunetszam.Text = $"{int.Parse(tb_szunetszam.Text) + 1}";

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ezt a gomb igazából nem csinál semmit, már mentve van 😋😋");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tb_kezdesora.Text != "" && tb_kezdesperc.Text != "" && tb_vegeora.Text != "" && tb_vegeperc.Text != "")
            {
                int szunetsorsszam = int.Parse(tb_szunetszam.Text);
                int kezdetora = int.Parse(tb_kezdesora.Text);
                int kezdetperc = int.Parse(tb_kezdesperc.Text);
                int vegeora = int.Parse(tb_vegeora.Text);
                int vegeperc = int.Parse(tb_vegeperc.Text);
                TimeSpan szunetkezdet = new TimeSpan(kezdetora, kezdetperc, 0);
                TimeSpan szunetvege = new TimeSpan(vegeora, vegeperc, 0);

                // Hozzáadás a ListView-hez
                lv_csengetes.Items.Add(new ListViewItem(new string[] { szunetsorsszam.ToString(), szunetkezdet.ToString(@"hh\:mm"), szunetvege.ToString(@"hh\:mm") }));

                // Hozzáadás a Form1 CsengetesiRend listájához
                parentForm.CsengetesiRend.Add(new CsengetesiRend(szunetsorsszam, szunetkezdet, szunetvege));

                tb_szunetszam.Text = $"{int.Parse(tb_szunetszam.Text) + 1}";
            }
            else {
                MessageBox.Show("Kérem adjon meg adatokat!");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (int.Parse(tb_szunetszam.Text) > 1)
            {


                // Utolsó elem eltávolítása a ListView-ből
                if (lv_csengetes.Items.Count > 0)
                {
                    lv_csengetes.Items.RemoveAt(lv_csengetes.Items.Count - 1);
                    parentForm.CsengetesiRend.RemoveAt(parentForm.CsengetesiRend.Count - 1);
                    tb_szunetszam.Text = $"{int.Parse(tb_szunetszam.Text) - 1}";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void tb_kezdesora_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(tb_kezdesora.Text, out int _))
            {
                tb_kezdesora.Text = "";
            }
            else if (int.Parse(tb_kezdesora.Text) < 0 || int.Parse(tb_kezdesora.Text) > 23)
            {
                tb_kezdesora.Text = "";
            }
        }

        private void tb_kezdesperc_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(tb_kezdesperc.Text, out int _))
            {
                tb_kezdesperc.Text = "";
            }
            else if (int.Parse(tb_kezdesperc.Text) < 0 || int.Parse(tb_kezdesperc.Text) > 59)
            {
                tb_kezdesperc.Text = "";
            }
        }

        private void tb_vegeora_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(tb_vegeora.Text, out int _))
            {
                tb_vegeora.Text = "";
            }
            else if (int.Parse(tb_vegeora.Text) < 0 || int.Parse(tb_vegeora.Text) > 23)
            {
                tb_vegeora.Text = "";
            }
        }

        private void tb_vegeperc_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(tb_vegeperc.Text, out int _))
            {
                tb_vegeperc.Text = "";
            }
            else if (int.Parse(tb_vegeperc.Text) < 0 || int.Parse(tb_vegeperc.Text) > 59)
            {
                tb_vegeperc.Text = "";
            }
        }
    }
}

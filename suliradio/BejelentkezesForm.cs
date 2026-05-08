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
    public partial class BejelentkezesForm : Form
    {
        private ApiKeresek _apiKeresek;
        public string Token { get; private set; } = "";

        public BejelentkezesForm()
        {
            InitializeComponent();
        }

        private async void BejelentkezesForm_Load(object sender, EventArgs e)
        {
            try
            {
                tb_email.Text = "";
                tb_jelszo.Text = "";
                btn_bejelentkezes.Enabled = true;
                btn_regisztracio.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba a form betöltésekor: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btn_bejelentkezes_Click(object sender, EventArgs e)
        {
            try
            {
                string email = tb_email.Text.Trim();
                string jelszo = tb_jelszo.Text;

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(jelszo))
                {
                    MessageBox.Show("Kérlek töltsd ki az összes mezõt!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                btn_bejelentkezes.Enabled = false;
                btn_regisztracio.Enabled = false;
                lbl_allapot.Text = "Bejelentkezés folyamatban...";

                // Jelszó Base64 kódolása
                byte[] jelszoBajt = Encoding.UTF8.GetBytes(jelszo);
                string jelszoBaze64 = Convert.ToBase64String(jelszoBajt);

                _apiKeresek = new ApiKeresek("");
                string token = await _apiKeresek.Bejelentkezes(email, jelszoBaze64);

                if (!string.IsNullOrEmpty(token))
                {
                    Token = token;
                    MessageBox.Show("Sikeres bejelentkezés!", "Siker", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"Bejelentkezés sikertelen! Ellenõrizd az email és jelszót! Token: {token}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lbl_allapot.Text = "Bejelentkezés sikertelen";
                    btn_bejelentkezes.Enabled = true;
                    btn_regisztracio.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba a bejelentkezéskor: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btn_bejelentkezes.Enabled = true;
                btn_regisztracio.Enabled = true;
            }
        }

        private async void btn_regisztracio_Click(object sender, EventArgs e)
        {
            try
            {
                string email = tb_email.Text.Trim();
                string jelszo = tb_jelszo.Text;

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(jelszo))
                {
                    MessageBox.Show("Kérlek töltsd ki az összes mezõt!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                btn_bejelentkezes.Enabled = false;
                btn_regisztracio.Enabled = false;
                lbl_allapot.Text = "Regisztráció folyamatban...";

                // Jelszó Base64 kódolása
                byte[] jelszoBajt = Encoding.UTF8.GetBytes(jelszo);
                string jelszoBaze64 = Convert.ToBase64String(jelszoBajt);

                _apiKeresek = new ApiKeresek("");
                bool sikeresRegisztracio = await _apiKeresek.Regisztracio(email, jelszoBaze64, "Felhasználó", "Név");

                if (sikeresRegisztracio)
                {
                    MessageBox.Show("Sikeres regisztráció! Most jelentkezz be!", "Siker", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lbl_allapot.Text = "Regisztráció sikeres";
                    btn_bejelentkezes.Enabled = true;
                    btn_regisztracio.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Regisztráció sikertelen! Lehet, hogy az email már foglalt.", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lbl_allapot.Text = "Regisztráció sikertelen";
                    btn_bejelentkezes.Enabled = true;
                    btn_regisztracio.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba a regisztrációkor: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btn_bejelentkezes.Enabled = true;
                btn_regisztracio.Enabled = true;
            }
        }

        private void btn_bezaras_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tb_email.Text = "suliradio004@gmail.com";
            tb_jelszo.Text = "suliradio";
        }
    }
}
   
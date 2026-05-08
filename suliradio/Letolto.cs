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
    public partial class Letolto : Form
    {
        public Letolto()
        {
            InitializeComponent();
        }

        private async void bt_letoltes_Click(object sender, EventArgs e)
        {

            await ZeneLetolto.Letoltes(tb_url.Text, "zenek");
        }
    }
}

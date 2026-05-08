namespace suliradio
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            tb_cim = new TextBox();
            groupBox1 = new GroupBox();
            button1 = new Button();
            button3 = new Button();
            lb_idosav = new Label();
            button2 = new Button();
            lb_vezerles = new Label();
            tb_eloado = new TextBox();
            label2 = new Label();
            menuStrip1 = new MenuStrip();
            fájlToolStripMenuItem = new ToolStripMenuItem();
            beállításokBetöltéseToolStripMenuItem = new ToolStripMenuItem();
            beállításokMentéseToolStripMenuItem = new ToolStripMenuItem();
            zenemappaHelyénekMegadásaToolStripMenuItem = new ToolStripMenuItem();
            letöltöttZenékMegnyitásaToolStripMenuItem = new ToolStripMenuItem();
            zenemappaBezárásaToolStripMenuItem = new ToolStripMenuItem();
            zenelistaFrissítéseToolStripMenuItem = new ToolStripMenuItem();
            kilépésToolStripMenuItem = new ToolStripMenuItem();
            beállításokToolStripMenuItem = new ToolStripMenuItem();
            zeneletöltőToolStripMenuItem = new ToolStripMenuItem();
            kérésekJóváhagyásaToolStripMenuItem = new ToolStripMenuItem();
            label3 = new Label();
            label4 = new Label();
            groupBox2 = new GroupBox();
            bt_adatmodosit = new Button();
            bt_eltavolit = new Button();
            bt_hozzaad = new Button();
            tb_kereses = new TextBox();
            label5 = new Label();
            tb_kijelolteloado = new TextBox();
            label7 = new Label();
            tb_kijeloltcim = new TextBox();
            label8 = new Label();
            lv_zenelista = new ListView();
            lv_varolista = new ListView();
            pictureBox1 = new PictureBox();
            groupBox1.SuspendLayout();
            menuStrip1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 22);
            label1.Name = "label1";
            label1.Size = new Size(32, 15);
            label1.TabIndex = 2;
            label1.Text = "Cím:";
            label1.Click += label1_Click;
            // 
            // tb_cim
            // 
            tb_cim.Location = new Point(6, 40);
            tb_cim.Name = "tb_cim";
            tb_cim.ReadOnly = true;
            tb_cim.Size = new Size(261, 23);
            tb_cim.TabIndex = 3;
            tb_cim.Text = "Cím";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(lb_idosav);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(lb_vezerles);
            groupBox1.Controls.Add(tb_eloado);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(tb_cim);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(1118, 28);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(273, 293);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Aktuális zene:";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // button1
            // 
            button1.BackColor = Color.DarkGray;
            button1.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 238);
            button1.ForeColor = Color.White;
            button1.Location = new Point(152, 222);
            button1.Name = "button1";
            button1.Size = new Size(115, 62);
            button1.TabIndex = 12;
            button1.Text = "Offline mód";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.DarkGray;
            button3.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 238);
            button3.ForeColor = Color.White;
            button3.Location = new Point(152, 155);
            button3.Name = "button3";
            button3.Size = new Size(115, 62);
            button3.TabIndex = 11;
            button3.Text = "Automatikus lejátszás: KI";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // lb_idosav
            // 
            lb_idosav.AutoSize = true;
            lb_idosav.Location = new Point(14, 223);
            lb_idosav.Name = "lb_idosav";
            lb_idosav.Size = new Size(24, 15);
            lb_idosav.TabIndex = 10;
            lb_idosav.Text = "Idő";
            lb_idosav.Click += lb_idosav_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point, 238);
            button2.Location = new Point(6, 155);
            button2.Name = "button2";
            button2.Size = new Size(140, 62);
            button2.TabIndex = 8;
            button2.Text = "⏯/⏩";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // lb_vezerles
            // 
            lb_vezerles.AutoSize = true;
            lb_vezerles.Location = new Point(6, 134);
            lb_vezerles.Name = "lb_vezerles";
            lb_vezerles.Size = new Size(51, 15);
            lb_vezerles.TabIndex = 6;
            lb_vezerles.Text = "Vezérlés:";
            // 
            // tb_eloado
            // 
            tb_eloado.Location = new Point(6, 95);
            tb_eloado.Name = "tb_eloado";
            tb_eloado.ReadOnly = true;
            tb_eloado.Size = new Size(261, 23);
            tb_eloado.TabIndex = 5;
            tb_eloado.Text = "Előadó";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 77);
            label2.Name = "label2";
            label2.Size = new Size(46, 15);
            label2.TabIndex = 4;
            label2.Text = "Előadó:";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fájlToolStripMenuItem, beállításokToolStripMenuItem, zeneletöltőToolStripMenuItem, kérésekJóváhagyásaToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1680, 24);
            menuStrip1.TabIndex = 5;
            menuStrip1.Text = "menuStrip1";
            // 
            // fájlToolStripMenuItem
            // 
            fájlToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { beállításokBetöltéseToolStripMenuItem, beállításokMentéseToolStripMenuItem, zenemappaHelyénekMegadásaToolStripMenuItem, letöltöttZenékMegnyitásaToolStripMenuItem, zenemappaBezárásaToolStripMenuItem, zenelistaFrissítéseToolStripMenuItem, kilépésToolStripMenuItem });
            fájlToolStripMenuItem.Name = "fájlToolStripMenuItem";
            fájlToolStripMenuItem.Size = new Size(37, 20);
            fájlToolStripMenuItem.Text = "Fájl";
            fájlToolStripMenuItem.Click += fájlToolStripMenuItem_Click;
            // 
            // beállításokBetöltéseToolStripMenuItem
            // 
            beállításokBetöltéseToolStripMenuItem.Name = "beállításokBetöltéseToolStripMenuItem";
            beállításokBetöltéseToolStripMenuItem.Size = new Size(244, 22);
            beállításokBetöltéseToolStripMenuItem.Text = "Beállítások betöltése";
            beállításokBetöltéseToolStripMenuItem.Click += beállításokBetöltéseToolStripMenuItem_Click;
            // 
            // beállításokMentéseToolStripMenuItem
            // 
            beállításokMentéseToolStripMenuItem.Name = "beállításokMentéseToolStripMenuItem";
            beállításokMentéseToolStripMenuItem.Size = new Size(244, 22);
            beállításokMentéseToolStripMenuItem.Text = "Beállítások mentése";
            beállításokMentéseToolStripMenuItem.Click += beállításokMentéseToolStripMenuItem_Click;
            // 
            // zenemappaHelyénekMegadásaToolStripMenuItem
            // 
            zenemappaHelyénekMegadásaToolStripMenuItem.Name = "zenemappaHelyénekMegadásaToolStripMenuItem";
            zenemappaHelyénekMegadásaToolStripMenuItem.Size = new Size(244, 22);
            zenemappaHelyénekMegadásaToolStripMenuItem.Text = "Zenemappa helyének megadása";
            zenemappaHelyénekMegadásaToolStripMenuItem.Click += zenemappaHelyénekMegadásaToolStripMenuItem_Click;
            // 
            // letöltöttZenékMegnyitásaToolStripMenuItem
            // 
            letöltöttZenékMegnyitásaToolStripMenuItem.Name = "letöltöttZenékMegnyitásaToolStripMenuItem";
            letöltöttZenékMegnyitásaToolStripMenuItem.Size = new Size(244, 22);
            letöltöttZenékMegnyitásaToolStripMenuItem.Text = "Letöltött zenék megnyitása";
            letöltöttZenékMegnyitásaToolStripMenuItem.Click += letöltöttZenékMegnyitásaToolStripMenuItem_Click;
            // 
            // zenemappaBezárásaToolStripMenuItem
            // 
            zenemappaBezárásaToolStripMenuItem.Name = "zenemappaBezárásaToolStripMenuItem";
            zenemappaBezárásaToolStripMenuItem.Size = new Size(244, 22);
            zenemappaBezárásaToolStripMenuItem.Text = "Zenelista törlése";
            zenemappaBezárásaToolStripMenuItem.Click += zenemappaBezárásaToolStripMenuItem_Click;
            // 
            // zenelistaFrissítéseToolStripMenuItem
            // 
            zenelistaFrissítéseToolStripMenuItem.Name = "zenelistaFrissítéseToolStripMenuItem";
            zenelistaFrissítéseToolStripMenuItem.Size = new Size(244, 22);
            zenelistaFrissítéseToolStripMenuItem.Text = "Zenelista frissítése";
            zenelistaFrissítéseToolStripMenuItem.Click += zenelistaFrissítéseToolStripMenuItem_Click;
            // 
            // kilépésToolStripMenuItem
            // 
            kilépésToolStripMenuItem.Name = "kilépésToolStripMenuItem";
            kilépésToolStripMenuItem.Size = new Size(244, 22);
            kilépésToolStripMenuItem.Text = "Kilépés";
            kilépésToolStripMenuItem.Click += kilépésToolStripMenuItem_Click;
            // 
            // beállításokToolStripMenuItem
            // 
            beállításokToolStripMenuItem.Name = "beállításokToolStripMenuItem";
            beállításokToolStripMenuItem.Size = new Size(159, 20);
            beállításokToolStripMenuItem.Text = "Csengetési rend szerkesztő";
            beállításokToolStripMenuItem.Click += beállításokToolStripMenuItem_Click;
            // 
            // zeneletöltőToolStripMenuItem
            // 
            zeneletöltőToolStripMenuItem.Name = "zeneletöltőToolStripMenuItem";
            zeneletöltőToolStripMenuItem.Size = new Size(79, 20);
            zeneletöltőToolStripMenuItem.Text = "Zeneletöltő";
            zeneletöltőToolStripMenuItem.Click += zeneletöltőToolStripMenuItem_Click;
            // 
            // kérésekJóváhagyásaToolStripMenuItem
            // 
            kérésekJóváhagyásaToolStripMenuItem.Name = "kérésekJóváhagyásaToolStripMenuItem";
            kérésekJóváhagyásaToolStripMenuItem.Size = new Size(127, 20);
            kérésekJóváhagyásaToolStripMenuItem.Text = "Kérések jóváhagyása";
            kérésekJóváhagyásaToolStripMenuItem.Click += kérésekJóváhagyásaToolStripMenuItem_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 25);
            label3.Name = "label3";
            label3.Size = new Size(176, 15);
            label3.TabIndex = 6;
            label3.Text = "Zenelista (Előadó - Cím - Hossz)";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(557, 24);
            label4.Name = "label4";
            label4.Size = new Size(216, 15);
            label4.TabIndex = 7;
            label4.Text = "Várólista (Előadó - Cím - Lejátszás ideje)";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(bt_adatmodosit);
            groupBox2.Controls.Add(bt_eltavolit);
            groupBox2.Controls.Add(bt_hozzaad);
            groupBox2.Controls.Add(tb_kereses);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(tb_kijelolteloado);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(tb_kijeloltcim);
            groupBox2.Controls.Add(label8);
            groupBox2.Location = new Point(1397, 28);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(273, 293);
            groupBox2.TabIndex = 11;
            groupBox2.TabStop = false;
            groupBox2.Text = "Kijelölt zene:";
            groupBox2.Enter += groupBox2_Enter;
            // 
            // bt_adatmodosit
            // 
            bt_adatmodosit.Location = new Point(6, 251);
            bt_adatmodosit.Name = "bt_adatmodosit";
            bt_adatmodosit.Size = new Size(261, 23);
            bt_adatmodosit.TabIndex = 10;
            bt_adatmodosit.Text = "Adatok módosítása";
            bt_adatmodosit.UseVisualStyleBackColor = true;
            // 
            // bt_eltavolit
            // 
            bt_eltavolit.Location = new Point(6, 222);
            bt_eltavolit.Name = "bt_eltavolit";
            bt_eltavolit.Size = new Size(261, 23);
            bt_eltavolit.TabIndex = 9;
            bt_eltavolit.Text = "Eltávolítás a várólistáról";
            bt_eltavolit.UseVisualStyleBackColor = true;
            bt_eltavolit.Click += bt_eltavolit_Click;
            // 
            // bt_hozzaad
            // 
            bt_hozzaad.Location = new Point(6, 193);
            bt_hozzaad.Name = "bt_hozzaad";
            bt_hozzaad.Size = new Size(261, 23);
            bt_hozzaad.TabIndex = 8;
            bt_hozzaad.Text = "Hozzáadás a várólistához";
            bt_hozzaad.UseVisualStyleBackColor = true;
            bt_hozzaad.Click += bt_hozzaad_Click;
            // 
            // tb_kereses
            // 
            tb_kereses.Location = new Point(6, 39);
            tb_kereses.Name = "tb_kereses";
            tb_kereses.Size = new Size(261, 23);
            tb_kereses.TabIndex = 7;
            tb_kereses.Text = "Keresés";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 21);
            label5.Name = "label5";
            label5.Size = new Size(49, 15);
            label5.TabIndex = 6;
            label5.Text = "Keresés:";
            // 
            // tb_kijelolteloado
            // 
            tb_kijelolteloado.Location = new Point(6, 149);
            tb_kijelolteloado.Name = "tb_kijelolteloado";
            tb_kijelolteloado.ReadOnly = true;
            tb_kijelolteloado.Size = new Size(261, 23);
            tb_kijelolteloado.TabIndex = 5;
            tb_kijelolteloado.Text = "Előadó";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 131);
            label7.Name = "label7";
            label7.Size = new Size(46, 15);
            label7.TabIndex = 4;
            label7.Text = "Előadó:";
            // 
            // tb_kijeloltcim
            // 
            tb_kijeloltcim.Location = new Point(6, 94);
            tb_kijeloltcim.Name = "tb_kijeloltcim";
            tb_kijeloltcim.ReadOnly = true;
            tb_kijeloltcim.Size = new Size(261, 23);
            tb_kijeloltcim.TabIndex = 3;
            tb_kijeloltcim.Text = "Cím";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(6, 76);
            label8.Name = "label8";
            label8.Size = new Size(32, 15);
            label8.TabIndex = 2;
            label8.Text = "Cím:";
            // 
            // lv_zenelista
            // 
            lv_zenelista.Location = new Point(12, 43);
            lv_zenelista.Name = "lv_zenelista";
            lv_zenelista.Size = new Size(539, 558);
            lv_zenelista.TabIndex = 12;
            lv_zenelista.UseCompatibleStateImageBehavior = false;
            lv_zenelista.SelectedIndexChanged += listView1_SelectedIndexChanged;
            // 
            // lv_varolista
            // 
            lv_varolista.Location = new Point(557, 42);
            lv_varolista.Name = "lv_varolista";
            lv_varolista.Size = new Size(555, 560);
            lv_varolista.TabIndex = 13;
            lv_varolista.UseCompatibleStateImageBehavior = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(1118, 327);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(551, 275);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 14;
            pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1680, 613);
            Controls.Add(pictureBox1);
            Controls.Add(lv_varolista);
            Controls.Add(lv_zenelista);
            Controls.Add(groupBox2);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(groupBox1);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Sulirádió - kezdőképernyő";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private TextBox tb_cim;
        private GroupBox groupBox1;
        private Button button2;
        private Label lb_vezerles;
        private TextBox tb_eloado;
        private Label label2;
        private Label lb_idosav;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fájlToolStripMenuItem;
        private ToolStripMenuItem zenemappaHelyénekMegadásaToolStripMenuItem;
        private Label label3;
        private Label label4;
        private GroupBox groupBox2;
        private TextBox tb_kijelolteloado;
        private Label label7;
        private TextBox tb_kijeloltcim;
        private Label label8;
        private Button bt_eltavolit;
        private Button bt_hozzaad;
        private TextBox tb_kereses;
        private Label label5;
        private Button bt_adatmodosit;
        private ToolStripMenuItem zenemappaBezárásaToolStripMenuItem;
        private ToolStripMenuItem beállításokToolStripMenuItem;
        private ToolStripMenuItem kilépésToolStripMenuItem;
        private ListView lv_zenelista;
        private ListView lv_varolista;
        private Button button3;
        private ToolStripMenuItem letöltöttZenékMegnyitásaToolStripMenuItem;
        private ToolStripMenuItem zeneletöltőToolStripMenuItem;
        private ToolStripMenuItem beállításokBetöltéseToolStripMenuItem;
        private ToolStripMenuItem beállításokMentéseToolStripMenuItem;
        private ToolStripMenuItem zenelistaFrissítéseToolStripMenuItem;
        private Button button1;
        private ToolStripMenuItem kérésekJóváhagyásaToolStripMenuItem;
        private PictureBox pictureBox1;
    }
}

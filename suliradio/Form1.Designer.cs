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
            lb_zenelista = new ListBox();
            lb_varolista = new ListBox();
            label1 = new Label();
            tb_cim = new TextBox();
            groupBox1 = new GroupBox();
            lb_idosav = new Label();
            trackBar1 = new TrackBar();
            button2 = new Button();
            button1 = new Button();
            lb_vezerles = new Label();
            tb_eloado = new TextBox();
            label2 = new Label();
            menuStrip1 = new MenuStrip();
            fájlToolStripMenuItem = new ToolStripMenuItem();
            zenemappaHelyénekMegadásaToolStripMenuItem = new ToolStripMenuItem();
            felhasználóToolStripMenuItem = new ToolStripMenuItem();
            bejelentkezésToolStripMenuItem = new ToolStripMenuItem();
            felhasználóváltásToolStripMenuItem = new ToolStripMenuItem();
            kijelentkezésToolStripMenuItem = new ToolStripMenuItem();
            label3 = new Label();
            label4 = new Label();
            groupBox2 = new GroupBox();
            bt_adatmodosit = new Button();
            bt_eltavolit = new Button();
            bt_hozzaad = new Button();
            tb_kereses = new TextBox();
            label5 = new Label();
            rb_kijelolteloado = new TextBox();
            label7 = new Label();
            tb_kijeloltcim = new TextBox();
            label8 = new Label();
            beállításokToolStripMenuItem = new ToolStripMenuItem();
            csengetésiRendToolStripMenuItem = new ToolStripMenuItem();
            továbbiBeállításokToolStripMenuItem = new ToolStripMenuItem();
            zenemappaBezárásaToolStripMenuItem = new ToolStripMenuItem();
            kilépésToolStripMenuItem = new ToolStripMenuItem();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            menuStrip1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // lb_zenelista
            // 
            lb_zenelista.FormattingEnabled = true;
            lb_zenelista.ItemHeight = 15;
            lb_zenelista.Location = new Point(12, 43);
            lb_zenelista.Name = "lb_zenelista";
            lb_zenelista.Size = new Size(274, 559);
            lb_zenelista.TabIndex = 0;
            // 
            // lb_varolista
            // 
            lb_varolista.FormattingEnabled = true;
            lb_varolista.ItemHeight = 15;
            lb_varolista.Location = new Point(292, 43);
            lb_varolista.Name = "lb_varolista";
            lb_varolista.Size = new Size(280, 559);
            lb_varolista.TabIndex = 1;
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
            tb_cim.Size = new Size(261, 23);
            tb_cim.TabIndex = 3;
            tb_cim.Text = "Cím";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lb_idosav);
            groupBox1.Controls.Add(trackBar1);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(lb_vezerles);
            groupBox1.Controls.Add(tb_eloado);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(tb_cim);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(578, 27);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(273, 574);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Aktuális zene:";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // lb_idosav
            // 
            lb_idosav.AutoSize = true;
            lb_idosav.Location = new Point(14, 223);
            lb_idosav.Name = "lb_idosav";
            lb_idosav.Size = new Size(24, 15);
            lb_idosav.TabIndex = 10;
            lb_idosav.Text = "Idő";
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(6, 241);
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(261, 45);
            trackBar1.TabIndex = 9;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 30F);
            button2.Location = new Point(6, 155);
            button2.Name = "button2";
            button2.Size = new Size(67, 62);
            button2.TabIndex = 8;
            button2.Text = "⏯";
            button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 30F);
            button1.Location = new Point(79, 155);
            button1.Name = "button1";
            button1.Size = new Size(67, 62);
            button1.TabIndex = 7;
            button1.Text = "⏭";
            button1.UseVisualStyleBackColor = true;
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
            menuStrip1.Items.AddRange(new ToolStripItem[] { fájlToolStripMenuItem, felhasználóToolStripMenuItem, beállításokToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1155, 24);
            menuStrip1.TabIndex = 5;
            menuStrip1.Text = "menuStrip1";
            // 
            // fájlToolStripMenuItem
            // 
            fájlToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { zenemappaHelyénekMegadásaToolStripMenuItem, zenemappaBezárásaToolStripMenuItem, kilépésToolStripMenuItem });
            fájlToolStripMenuItem.Name = "fájlToolStripMenuItem";
            fájlToolStripMenuItem.Size = new Size(37, 20);
            fájlToolStripMenuItem.Text = "Fájl";
            // 
            // zenemappaHelyénekMegadásaToolStripMenuItem
            // 
            zenemappaHelyénekMegadásaToolStripMenuItem.Name = "zenemappaHelyénekMegadásaToolStripMenuItem";
            zenemappaHelyénekMegadásaToolStripMenuItem.Size = new Size(244, 22);
            zenemappaHelyénekMegadásaToolStripMenuItem.Text = "Zenemappa helyének megadása";
            // 
            // felhasználóToolStripMenuItem
            // 
            felhasználóToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { bejelentkezésToolStripMenuItem, felhasználóváltásToolStripMenuItem, kijelentkezésToolStripMenuItem });
            felhasználóToolStripMenuItem.Name = "felhasználóToolStripMenuItem";
            felhasználóToolStripMenuItem.Size = new Size(80, 20);
            felhasználóToolStripMenuItem.Text = "Felhasználó";
            // 
            // bejelentkezésToolStripMenuItem
            // 
            bejelentkezésToolStripMenuItem.Name = "bejelentkezésToolStripMenuItem";
            bejelentkezésToolStripMenuItem.Size = new Size(180, 22);
            bejelentkezésToolStripMenuItem.Text = "Bejelentkezés";
            // 
            // felhasználóváltásToolStripMenuItem
            // 
            felhasználóváltásToolStripMenuItem.Name = "felhasználóváltásToolStripMenuItem";
            felhasználóváltásToolStripMenuItem.Size = new Size(180, 22);
            felhasználóváltásToolStripMenuItem.Text = "Felhasználóváltás";
            // 
            // kijelentkezésToolStripMenuItem
            // 
            kijelentkezésToolStripMenuItem.Name = "kijelentkezésToolStripMenuItem";
            kijelentkezésToolStripMenuItem.Size = new Size(180, 22);
            kijelentkezésToolStripMenuItem.Text = "Kijelentkezés";
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
            label4.Location = new Point(292, 24);
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
            groupBox2.Controls.Add(rb_kijelolteloado);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(tb_kijeloltcim);
            groupBox2.Controls.Add(label8);
            groupBox2.Location = new Point(857, 28);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(273, 574);
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
            // 
            // bt_hozzaad
            // 
            bt_hozzaad.Location = new Point(6, 193);
            bt_hozzaad.Name = "bt_hozzaad";
            bt_hozzaad.Size = new Size(261, 23);
            bt_hozzaad.TabIndex = 8;
            bt_hozzaad.Text = "Hozzáadás a várólistához";
            bt_hozzaad.UseVisualStyleBackColor = true;
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
            // rb_kijelolteloado
            // 
            rb_kijelolteloado.Location = new Point(6, 149);
            rb_kijelolteloado.Name = "rb_kijelolteloado";
            rb_kijelolteloado.Size = new Size(261, 23);
            rb_kijelolteloado.TabIndex = 5;
            rb_kijelolteloado.Text = "Előadó";
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
            // beállításokToolStripMenuItem
            // 
            beállításokToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { csengetésiRendToolStripMenuItem, továbbiBeállításokToolStripMenuItem });
            beállításokToolStripMenuItem.Name = "beállításokToolStripMenuItem";
            beállításokToolStripMenuItem.Size = new Size(75, 20);
            beállításokToolStripMenuItem.Text = "Beállítások";
            // 
            // csengetésiRendToolStripMenuItem
            // 
            csengetésiRendToolStripMenuItem.Name = "csengetésiRendToolStripMenuItem";
            csengetésiRendToolStripMenuItem.Size = new Size(180, 22);
            csengetésiRendToolStripMenuItem.Text = "Csengetési rend";
            // 
            // továbbiBeállításokToolStripMenuItem
            // 
            továbbiBeállításokToolStripMenuItem.Name = "továbbiBeállításokToolStripMenuItem";
            továbbiBeállításokToolStripMenuItem.Size = new Size(180, 22);
            továbbiBeállításokToolStripMenuItem.Text = "További beállítások";
            // 
            // zenemappaBezárásaToolStripMenuItem
            // 
            zenemappaBezárásaToolStripMenuItem.Name = "zenemappaBezárásaToolStripMenuItem";
            zenemappaBezárásaToolStripMenuItem.Size = new Size(244, 22);
            zenemappaBezárásaToolStripMenuItem.Text = "Zenemappa bezárása";
            // 
            // kilépésToolStripMenuItem
            // 
            kilépésToolStripMenuItem.Name = "kilépésToolStripMenuItem";
            kilépésToolStripMenuItem.Size = new Size(244, 22);
            kilépésToolStripMenuItem.Text = "Kilépés";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1155, 613);
            Controls.Add(groupBox2);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(groupBox1);
            Controls.Add(lb_varolista);
            Controls.Add(lb_zenelista);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lb_zenelista;
        private ListBox lb_varolista;
        private Label label1;
        private TextBox tb_cim;
        private GroupBox groupBox1;
        private Button button2;
        private Button button1;
        private Label lb_vezerles;
        private TextBox tb_eloado;
        private Label label2;
        private Label lb_idosav;
        private TrackBar trackBar1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fájlToolStripMenuItem;
        private ToolStripMenuItem zenemappaHelyénekMegadásaToolStripMenuItem;
        private Label label3;
        private Label label4;
        private ToolStripMenuItem felhasználóToolStripMenuItem;
        private ToolStripMenuItem bejelentkezésToolStripMenuItem;
        private ToolStripMenuItem felhasználóváltásToolStripMenuItem;
        private ToolStripMenuItem kijelentkezésToolStripMenuItem;
        private GroupBox groupBox2;
        private TextBox rb_kijelolteloado;
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
        private ToolStripMenuItem csengetésiRendToolStripMenuItem;
        private ToolStripMenuItem továbbiBeállításokToolStripMenuItem;
        private ToolStripMenuItem kilépésToolStripMenuItem;
    }
}

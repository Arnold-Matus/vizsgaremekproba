namespace suliradio
{
    partial class beallitasok1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            lv_csengetes = new ListView();
            label2 = new Label();
            tb_kezdesora = new TextBox();
            tb_kezdesperc = new TextBox();
            tb_szunetszam = new TextBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            tb_vegeora = new TextBox();
            tb_vegeperc = new TextBox();
            label6 = new Label();
            label7 = new Label();
            button2 = new Button();
            button3 = new Button();
            tb_csuszas = new TextBox();
            label8 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label1.ForeColor = SystemColors.HotTrack;
            label1.Location = new Point(109, 9);
            label1.Name = "label1";
            label1.Size = new Size(311, 45);
            label1.TabIndex = 0;
            label1.Text = "Csengetési rend 🔔";
            // 
            // lv_csengetes
            // 
            lv_csengetes.Location = new Point(12, 90);
            lv_csengetes.Name = "lv_csengetes";
            lv_csengetes.Size = new Size(236, 196);
            lv_csengetes.TabIndex = 1;
            lv_csengetes.UseCompatibleStateImageBehavior = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 68);
            label2.Name = "label2";
            label2.Size = new Size(91, 15);
            label2.TabIndex = 2;
            label2.Text = "Csengetési rend";
            // 
            // tb_kezdesora
            // 
            tb_kezdesora.Location = new Point(380, 123);
            tb_kezdesora.Name = "tb_kezdesora";
            tb_kezdesora.Size = new Size(53, 23);
            tb_kezdesora.TabIndex = 4;
            tb_kezdesora.TextChanged += tb_kezdesora_TextChanged;
            // 
            // tb_kezdesperc
            // 
            tb_kezdesperc.Location = new Point(457, 123);
            tb_kezdesperc.Name = "tb_kezdesperc";
            tb_kezdesperc.Size = new Size(54, 23);
            tb_kezdesperc.TabIndex = 5;
            tb_kezdesperc.TextChanged += tb_kezdesperc_TextChanged;
            // 
            // tb_szunetszam
            // 
            tb_szunetszam.Enabled = false;
            tb_szunetszam.Location = new Point(457, 90);
            tb_szunetszam.Name = "tb_szunetszam";
            tb_szunetszam.Size = new Size(54, 23);
            tb_szunetszam.TabIndex = 6;
            tb_szunetszam.Text = "1";
            tb_szunetszam.TextChanged += tb_szunetszam_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(264, 93);
            label3.Name = "label3";
            label3.Size = new Size(191, 15);
            label3.TabIndex = 7;
            label3.Text = "Szünet száma (ez hanyadik szünet):";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(264, 126);
            label4.Name = "label4";
            label4.Size = new Size(85, 15);
            label4.TabIndex = 8;
            label4.Text = "Szünet kezdete";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(264, 157);
            label5.Name = "label5";
            label5.Size = new Size(73, 15);
            label5.TabIndex = 9;
            label5.Text = "Szünet vége:";
            // 
            // tb_vegeora
            // 
            tb_vegeora.Location = new Point(380, 154);
            tb_vegeora.Name = "tb_vegeora";
            tb_vegeora.Size = new Size(53, 23);
            tb_vegeora.TabIndex = 10;
            tb_vegeora.TextChanged += tb_vegeora_TextChanged;
            // 
            // tb_vegeperc
            // 
            tb_vegeperc.Location = new Point(457, 157);
            tb_vegeperc.Name = "tb_vegeperc";
            tb_vegeperc.Size = new Size(54, 23);
            tb_vegeperc.TabIndex = 11;
            tb_vegeperc.TextChanged += tb_vegeperc_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(441, 126);
            label6.Name = "label6";
            label6.Size = new Size(10, 15);
            label6.TabIndex = 12;
            label6.Text = ":";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(441, 160);
            label7.Name = "label7";
            label7.Size = new Size(10, 15);
            label7.TabIndex = 13;
            label7.Text = ":";
            // 
            // button2
            // 
            button2.Location = new Point(296, 196);
            button2.Name = "button2";
            button2.Size = new Size(176, 23);
            button2.TabIndex = 14;
            button2.Text = "Szünet hozzáadása";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(296, 225);
            button3.Name = "button3";
            button3.Size = new Size(176, 23);
            button3.TabIndex = 15;
            button3.Text = "Utolsó szünet eltávolítása";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // tb_csuszas
            // 
            tb_csuszas.Location = new Point(411, 259);
            tb_csuszas.Name = "tb_csuszas";
            tb_csuszas.Size = new Size(100, 23);
            tb_csuszas.TabIndex = 16;
            tb_csuszas.TextChanged += textBox1_TextChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(264, 262);
            label8.Name = "label8";
            label8.Size = new Size(135, 15);
            label8.TabIndex = 17;
            label8.Text = "Csengő csúsztatás (mp):";
            // 
            // beallitasok1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(523, 299);
            Controls.Add(label8);
            Controls.Add(tb_csuszas);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(tb_vegeperc);
            Controls.Add(tb_vegeora);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(tb_szunetszam);
            Controls.Add(tb_kezdesperc);
            Controls.Add(tb_kezdesora);
            Controls.Add(label2);
            Controls.Add(lv_csengetes);
            Controls.Add(label1);
            Name = "beallitasok1";
            Text = "Csengetési rend szerkesztő";
            Load += beallitasok1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ListView lv_csengetes;
        private Label label2;
        private TextBox tb_kezdesora;
        private TextBox tb_kezdesperc;
        private TextBox tb_szunetszam;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox tb_vegeora;
        private TextBox tb_vegeperc;
        private Label label6;
        private Label label7;
        private Button button2;
        private Button button3;
        private TextBox tb_csuszas;
        private Label label8;
    }
}
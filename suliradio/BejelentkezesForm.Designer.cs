namespace suliradio
{
    partial class BejelentkezesForm
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
            lbl_email = new Label();
            tb_email = new TextBox();
            lbl_jelszo = new Label();
            tb_jelszo = new TextBox();
            btn_bejelentkezes = new Button();
            btn_regisztracio = new Button();
            btn_bezaras = new Button();
            lbl_allapot = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // lbl_email
            // 
            lbl_email.AutoSize = true;
            lbl_email.Location = new Point(12, 15);
            lbl_email.Name = "lbl_email";
            lbl_email.Size = new Size(39, 15);
            lbl_email.TabIndex = 0;
            lbl_email.Text = "Email:";
            // 
            // tb_email
            // 
            tb_email.Location = new Point(12, 33);
            tb_email.Name = "tb_email";
            tb_email.Size = new Size(260, 23);
            tb_email.TabIndex = 1;
            // 
            // lbl_jelszo
            // 
            lbl_jelszo.AutoSize = true;
            lbl_jelszo.Location = new Point(12, 70);
            lbl_jelszo.Name = "lbl_jelszo";
            lbl_jelszo.Size = new Size(40, 15);
            lbl_jelszo.TabIndex = 2;
            lbl_jelszo.Text = "Jelszó:";
            // 
            // tb_jelszo
            // 
            tb_jelszo.Location = new Point(12, 88);
            tb_jelszo.Name = "tb_jelszo";
            tb_jelszo.PasswordChar = '*';
            tb_jelszo.Size = new Size(260, 23);
            tb_jelszo.TabIndex = 3;
            // 
            // btn_bejelentkezes
            // 
            btn_bejelentkezes.Location = new Point(12, 130);
            btn_bejelentkezes.Name = "btn_bejelentkezes";
            btn_bejelentkezes.Size = new Size(125, 35);
            btn_bejelentkezes.TabIndex = 4;
            btn_bejelentkezes.Text = "Bejelentkezés";
            btn_bejelentkezes.UseVisualStyleBackColor = true;
            btn_bejelentkezes.Click += btn_bejelentkezes_Click;
            // 
            // btn_regisztracio
            // 
            btn_regisztracio.Location = new Point(147, 130);
            btn_regisztracio.Name = "btn_regisztracio";
            btn_regisztracio.Size = new Size(125, 35);
            btn_regisztracio.TabIndex = 5;
            btn_regisztracio.Text = "Regisztráció";
            btn_regisztracio.UseVisualStyleBackColor = true;
            btn_regisztracio.Click += btn_regisztracio_Click;
            // 
            // btn_bezaras
            // 
            btn_bezaras.Location = new Point(12, 175);
            btn_bezaras.Name = "btn_bezaras";
            btn_bezaras.Size = new Size(260, 30);
            btn_bezaras.TabIndex = 6;
            btn_bezaras.Text = "Bezárás";
            btn_bezaras.UseVisualStyleBackColor = true;
            btn_bezaras.Click += btn_bezaras_Click;
            // 
            // lbl_allapot
            // 
            lbl_allapot.AutoSize = true;
            lbl_allapot.Location = new Point(12, 115);
            lbl_allapot.Name = "lbl_allapot";
            lbl_allapot.Size = new Size(87, 15);
            lbl_allapot.TabIndex = 7;
            lbl_allapot.Text = "Kész a kezdésre";
            // 
            // button1
            // 
            button1.Location = new Point(100, 213);
            button1.Name = "button1";
            button1.Size = new Size(81, 23);
            button1.TabIndex = 8;
            button1.Text = "Kitöltés! 😎";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // BejelentkezesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 248);
            ControlBox = false;
            Controls.Add(button1);
            Controls.Add(lbl_allapot);
            Controls.Add(btn_bezaras);
            Controls.Add(btn_regisztracio);
            Controls.Add(btn_bejelentkezes);
            Controls.Add(tb_jelszo);
            Controls.Add(lbl_jelszo);
            Controls.Add(tb_email);
            Controls.Add(lbl_email);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "BejelentkezesForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Bejelentkezés / Regisztráció";
            Load += BejelentkezesForm_Load;
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private Label lbl_email;
        private TextBox tb_email;
        private Label lbl_jelszo;
        private TextBox tb_jelszo;
        private Button btn_bejelentkezes;
        private Button btn_regisztracio;
        private Button btn_bezaras;
        private Label lbl_allapot;
        private Button button1;
    }
}

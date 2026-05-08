namespace suliradio
{
    partial class Letolto
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
            tb_url = new TextBox();
            label2 = new Label();
            bt_letoltes = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label1.ForeColor = SystemColors.HotTrack;
            label1.Location = new Point(178, 9);
            label1.Name = "label1";
            label1.Size = new Size(191, 45);
            label1.TabIndex = 1;
            label1.Text = "Zeneletöltő";
            // 
            // tb_url
            // 
            tb_url.Location = new Point(12, 97);
            tb_url.Name = "tb_url";
            tb_url.Size = new Size(519, 23);
            tb_url.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(188, 79);
            label2.Name = "label2";
            label2.Size = new Size(166, 15);
            label2.TabIndex = 3;
            label2.Text = "Youtube / Youtube Music link:";
            // 
            // bt_letoltes
            // 
            bt_letoltes.Location = new Point(230, 126);
            bt_letoltes.Name = "bt_letoltes";
            bt_letoltes.Size = new Size(75, 23);
            bt_letoltes.TabIndex = 4;
            bt_letoltes.Text = "Letöltés!";
            bt_letoltes.UseVisualStyleBackColor = true;
            bt_letoltes.Click += bt_letoltes_Click;
            // 
            // Letolto
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(543, 177);
            Controls.Add(bt_letoltes);
            Controls.Add(label2);
            Controls.Add(tb_url);
            Controls.Add(label1);
            Name = "Letolto";
            Text = "Letolto";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox tb_url;
        private Label label2;
        private Button bt_letoltes;
    }
}
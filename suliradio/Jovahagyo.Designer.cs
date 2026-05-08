namespace suliradio
{
    partial class Jovahagyo
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
            listView1 = new ListView();
            label2 = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label1.ForeColor = SystemColors.HotTrack;
            label1.Location = new Point(214, 9);
            label1.Name = "label1";
            label1.Size = new Size(384, 45);
            label1.TabIndex = 1;
            label1.Text = "Kérések jóváhagyása ✅";
            // 
            // listView1
            // 
            listView1.Location = new Point(12, 78);
            listView1.Name = "listView1";
            listView1.Size = new Size(586, 367);
            listView1.TabIndex = 2;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 60);
            label2.Name = "label2";
            label2.Size = new Size(106, 15);
            label2.TabIndex = 3;
            label2.Text = "Beérkezett kérések:";
            // 
            // button1
            // 
            button1.Location = new Point(604, 78);
            button1.Name = "button1";
            button1.Size = new Size(184, 23);
            button1.TabIndex = 4;
            button1.Text = "Kijelölt jóváhagyása";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // Jovahagyo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(listView1);
            Controls.Add(label1);
            Name = "Jovahagyo";
            Text = "Jovahagyo";
            Load += Jovahagyo_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ListView listView1;
        private Label label2;
        private Button button1;
    }
}
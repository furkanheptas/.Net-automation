namespace AtYarisi
{
    partial class hesap
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
            this.button1 = new System.Windows.Forms.Button();
            this.tutar = new System.Windows.Forms.Label();
            this.txtTutar = new System.Windows.Forms.TextBox();
            this.BorcAl = new System.Windows.Forms.Button();
            this.BorcVer = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 15);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 1;
            this.button1.Text = "Geri Dön";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tutar
            // 
            this.tutar.AutoSize = true;
            this.tutar.Location = new System.Drawing.Point(187, 95);
            this.tutar.Name = "tutar";
            this.tutar.Size = new System.Drawing.Size(54, 16);
            this.tutar.TabIndex = 2;
            this.tutar.Text = "TUTAR";
            // 
            // txtTutar
            // 
            this.txtTutar.Location = new System.Drawing.Point(294, 89);
            this.txtTutar.Name = "txtTutar";
            this.txtTutar.Size = new System.Drawing.Size(100, 22);
            this.txtTutar.TabIndex = 3;
            this.txtTutar.Text = "txtTutar";
            // 
            // BorcAl
            // 
            this.BorcAl.Location = new System.Drawing.Point(453, 88);
            this.BorcAl.Name = "BorcAl";
            this.BorcAl.Size = new System.Drawing.Size(75, 23);
            this.BorcAl.TabIndex = 4;
            this.BorcAl.Text = "Borç Al";
            this.BorcAl.UseVisualStyleBackColor = true;
            this.BorcAl.Click += new System.EventHandler(this.BorcAl_Click);
            // 
            // BorcVer
            // 
            this.BorcVer.Location = new System.Drawing.Point(453, 132);
            this.BorcVer.Name = "BorcVer";
            this.BorcVer.Size = new System.Drawing.Size(75, 23);
            this.BorcVer.TabIndex = 5;
            this.BorcVer.Text = "Borç Ver";
            this.BorcVer.UseVisualStyleBackColor = true;
            this.BorcVer.Click += new System.EventHandler(this.BorcVer_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(453, 179);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 6;
            this.button4.Text = "BorcOde";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(711, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "DURUM:";
            // 
            // hesap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 690);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.BorcVer);
            this.Controls.Add(this.BorcAl);
            this.Controls.Add(this.txtTutar);
            this.Controls.Add(this.tutar);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "hesap";
            this.Text = "hesap";
            this.Load += new System.EventHandler(this.hesap_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label tutar;
        private System.Windows.Forms.TextBox txtTutar;
        private System.Windows.Forms.Button BorcAl;
        private System.Windows.Forms.Button BorcVer;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label2;
    }
}
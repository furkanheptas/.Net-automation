namespace AtYarisi
{
    partial class yaris
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(yaris));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.gifTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.gerisayimLabel = new System.Windows.Forms.Label();
            this.gerisayimTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.Controls.Add(this.gerisayimLabel);
            this.panel1.Location = new System.Drawing.Point(12, 81);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1043, 463);
            this.panel1.TabIndex = 1;
            // 
            // gerisayimLabel
            // 
            this.gerisayimLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.gerisayimLabel.Location = new System.Drawing.Point(427, 168);
            this.gerisayimLabel.Name = "gerisayimLabel";
            this.gerisayimLabel.Size = new System.Drawing.Size(208, 115);
            this.gerisayimLabel.TabIndex = 2;
            this.gerisayimLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gerisayimTimer
            // 
            this.gerisayimTimer.Interval = 1000;
            this.gerisayimTimer.Tick += new System.EventHandler(this.gerisayimTimer_Tick);
            // 
            // yaris
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 603);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "yaris";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Yarış";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.yaris_FormClosed);
            this.Load += new System.EventHandler(this.yaris_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer gifTimer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label gerisayimLabel;
        private System.Windows.Forms.Timer gerisayimTimer;
    }
}
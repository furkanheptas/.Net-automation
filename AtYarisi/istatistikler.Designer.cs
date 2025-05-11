namespace AtYarisi
{
    partial class istatistikler
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(istatistikler));
            this.button1 = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblToplamBahis = new System.Windows.Forms.Label();
            this.lblKazanilanBahis = new System.Windows.Forms.Label();
            this.lblKaybedilenBahis = new System.Windows.Forms.Label();
            this.lblKazanc = new System.Windows.Forms.Label();
            this.lblKayip = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Location = new System.Drawing.Point(26, 24);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 37);
            this.button1.TabIndex = 1;
            this.button1.Text = "Geri Dön";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.MouseEnter += new System.EventHandler(this.button1_MouseEnter);
            this.button1.MouseLeave += new System.EventHandler(this.button1_MouseLeave);
            // 
            // chart1
            // 
            this.chart1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Cursor = System.Windows.Forms.Cursors.Default;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(250, 72);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(395, 200);
            this.chart1.TabIndex = 2;
            this.chart1.Text = "chart1";
            // 
            // lblToplamBahis
            // 
            this.lblToplamBahis.AutoSize = true;
            this.lblToplamBahis.Font = new System.Drawing.Font("Verdana", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblToplamBahis.Location = new System.Drawing.Point(245, 292);
            this.lblToplamBahis.Name = "lblToplamBahis";
            this.lblToplamBahis.Size = new System.Drawing.Size(90, 28);
            this.lblToplamBahis.TabIndex = 3;
            this.lblToplamBahis.Text = "label1";
            // 
            // lblKazanilanBahis
            // 
            this.lblKazanilanBahis.AutoSize = true;
            this.lblKazanilanBahis.Font = new System.Drawing.Font("Verdana", 13.8F, System.Drawing.FontStyle.Bold);
            this.lblKazanilanBahis.Location = new System.Drawing.Point(245, 333);
            this.lblKazanilanBahis.Name = "lblKazanilanBahis";
            this.lblKazanilanBahis.Size = new System.Drawing.Size(90, 28);
            this.lblKazanilanBahis.TabIndex = 4;
            this.lblKazanilanBahis.Text = "label1";
            // 
            // lblKaybedilenBahis
            // 
            this.lblKaybedilenBahis.AutoSize = true;
            this.lblKaybedilenBahis.Font = new System.Drawing.Font("Verdana", 13.8F, System.Drawing.FontStyle.Bold);
            this.lblKaybedilenBahis.Location = new System.Drawing.Point(245, 378);
            this.lblKaybedilenBahis.Name = "lblKaybedilenBahis";
            this.lblKaybedilenBahis.Size = new System.Drawing.Size(90, 28);
            this.lblKaybedilenBahis.TabIndex = 5;
            this.lblKaybedilenBahis.Text = "label1";
            // 
            // lblKazanc
            // 
            this.lblKazanc.AutoSize = true;
            this.lblKazanc.Font = new System.Drawing.Font("Verdana", 13.8F, System.Drawing.FontStyle.Bold);
            this.lblKazanc.Location = new System.Drawing.Point(245, 423);
            this.lblKazanc.Name = "lblKazanc";
            this.lblKazanc.Size = new System.Drawing.Size(90, 28);
            this.lblKazanc.TabIndex = 6;
            this.lblKazanc.Text = "label1";
            // 
            // lblKayip
            // 
            this.lblKayip.AutoSize = true;
            this.lblKayip.Font = new System.Drawing.Font("Verdana", 13.8F, System.Drawing.FontStyle.Bold);
            this.lblKayip.Location = new System.Drawing.Point(245, 464);
            this.lblKayip.Name = "lblKayip";
            this.lblKayip.Size = new System.Drawing.Size(90, 28);
            this.lblKayip.TabIndex = 7;
            this.lblKayip.Text = "label1";
            // 
            // istatistikler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 553);
            this.Controls.Add(this.lblKayip);
            this.Controls.Add(this.lblKazanc);
            this.Controls.Add(this.lblKaybedilenBahis);
            this.Controls.Add(this.lblKazanilanBahis);
            this.Controls.Add(this.lblToplamBahis);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "istatistikler";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "İstatistikler";
            this.Load += new System.EventHandler(this.istatistikler_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label lblToplamBahis;
        private System.Windows.Forms.Label lblKazanilanBahis;
        private System.Windows.Forms.Label lblKaybedilenBahis;
        private System.Windows.Forms.Label lblKazanc;
        private System.Windows.Forms.Label lblKayip;
    }
}
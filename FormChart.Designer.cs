namespace RealtimeAnalysis
{
    partial class FormChart
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend7 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.chartPlot = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.labelSchedulable = new System.Windows.Forms.Label();
            this.labelMinPi = new System.Windows.Forms.Label();
            this.labelMinTms = new System.Windows.Forms.Label();
            this.labelMinEms = new System.Windows.Forms.Label();
            this.labelOptimal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chartPlot)).BeginInit();
            this.SuspendLayout();
            // 
            // chartPlot
            // 
            chartArea7.Name = "ChartArea1";
            this.chartPlot.ChartAreas.Add(chartArea7);
            this.chartPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            legend7.Name = "Legend1";
            this.chartPlot.Legends.Add(legend7);
            this.chartPlot.Location = new System.Drawing.Point(0, 0);
            this.chartPlot.Name = "chartPlot";
            this.chartPlot.Size = new System.Drawing.Size(836, 457);
            this.chartPlot.SuppressExceptions = true;
            this.chartPlot.TabIndex = 0;
            this.chartPlot.Text = "Plot";
            // 
            // labelSchedulable
            // 
            this.labelSchedulable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSchedulable.AutoSize = true;
            this.labelSchedulable.BackColor = System.Drawing.Color.White;
            this.labelSchedulable.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelSchedulable.Location = new System.Drawing.Point(702, 423);
            this.labelSchedulable.Name = "labelSchedulable";
            this.labelSchedulable.Size = new System.Drawing.Size(37, 12);
            this.labelSchedulable.TabIndex = 1;
            this.labelSchedulable.Text = "label";
            // 
            // labelMinPi
            // 
            this.labelMinPi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMinPi.AutoSize = true;
            this.labelMinPi.BackColor = System.Drawing.Color.White;
            this.labelMinPi.Location = new System.Drawing.Point(683, 350);
            this.labelMinPi.Name = "labelMinPi";
            this.labelMinPi.Size = new System.Drawing.Size(37, 12);
            this.labelMinPi.TabIndex = 2;
            this.labelMinPi.Text = "minPi";
            // 
            // labelMinTms
            // 
            this.labelMinTms.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMinTms.AutoSize = true;
            this.labelMinTms.BackColor = System.Drawing.Color.White;
            this.labelMinTms.Location = new System.Drawing.Point(683, 386);
            this.labelMinTms.Name = "labelMinTms";
            this.labelMinTms.Size = new System.Drawing.Size(52, 12);
            this.labelMinTms.TabIndex = 3;
            this.labelMinTms.Text = "minTms";
            // 
            // labelMinEms
            // 
            this.labelMinEms.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMinEms.AutoSize = true;
            this.labelMinEms.BackColor = System.Drawing.Color.White;
            this.labelMinEms.Location = new System.Drawing.Point(683, 368);
            this.labelMinEms.Name = "labelMinEms";
            this.labelMinEms.Size = new System.Drawing.Size(52, 12);
            this.labelMinEms.TabIndex = 4;
            this.labelMinEms.Text = "minEms";
            // 
            // labelOptimal
            // 
            this.labelOptimal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelOptimal.AutoSize = true;
            this.labelOptimal.BackColor = System.Drawing.Color.White;
            this.labelOptimal.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelOptimal.Location = new System.Drawing.Point(683, 330);
            this.labelOptimal.Name = "labelOptimal";
            this.labelOptimal.Size = new System.Drawing.Size(53, 12);
            this.labelOptimal.TabIndex = 5;
            this.labelOptimal.Text = "optimal";
            // 
            // FormChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 457);
            this.Controls.Add(this.labelOptimal);
            this.Controls.Add(this.labelMinEms);
            this.Controls.Add(this.labelMinTms);
            this.Controls.Add(this.labelMinPi);
            this.Controls.Add(this.labelSchedulable);
            this.Controls.Add(this.chartPlot);
            this.KeyPreview = true;
            this.Name = "FormChart";
            this.Text = "Plot";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormChart_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.chartPlot)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartPlot;
        private System.Windows.Forms.Label labelSchedulable;
        private System.Windows.Forms.Label labelMinPi;
        private System.Windows.Forms.Label labelMinTms;
        private System.Windows.Forms.Label labelMinEms;
        private System.Windows.Forms.Label labelOptimal;
    }
}
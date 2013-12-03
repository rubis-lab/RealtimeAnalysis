using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace RealtimeAnalysis
{
    public partial class FormChart : Form
    {
        public FormChart()
        {
            InitializeComponent();
        }

        public Label GetLable
        {
            get { return this.labelSchedulable; }
        }

        public void SetMinValues(double pi, double minEmsTheta, double minTmsTheta)
        {
            labelOptimal.Text = "Optimal PRMs";
            labelMinPi.Text = "- Period: " + pi.ToString();
            labelMinEms.Text = "- Budget of EMS: " + minEmsTheta.ToString();
            labelMinTms.Text = "- Budget of TMS: " + minTmsTheta.ToString();
        }

        public FormChart(string title)
        {
            InitializeComponent();

            Title t = new Title(title);
            t.Font = new System.Drawing.Font(t.Font.FontFamily, 20f);
            chartPlot.Titles.Add(t);

            labelOptimal.Text = string.Empty;
            labelSchedulable.Text = string.Empty;
            labelMinPi.Text = string.Empty;
            labelMinEms.Text = string.Empty;
            labelMinTms.Text = string.Empty;
        }

        public Axis AxisX
        {
            get { return chartPlot.ChartAreas[0].AxisX; }
        }

        public Axis AxisY
        {
            get { return chartPlot.ChartAreas[0].AxisY; }
        }

        public void AddSeries(Series series)
        {
            chartPlot.Series.Add(series);
        }

        public bool EnableLegend
        {
            get { return chartPlot.Legends[0].Enabled; }
            set { chartPlot.Legends[0].Enabled = value; }
        }

        private void FormChart_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }


    }
}

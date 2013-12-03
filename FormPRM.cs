
#define KEFICO

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace RealtimeAnalysis
{
    public partial class FormPRM : Form
    {

        double _LcmEms;
        double _LcmTms;
        double _intervalLength;
        double _incrementUnit;
        double _PI;
        double _THETA;
        double _overhead;

        double _minimumEmsTheta = 0;
        double _minimumTmsTheta = 0;
        double _minimumPi = 0;

        private void SetMinimumValues(double pi, double ems, double tms)
        {
            _minimumEmsTheta = ems;
            _minimumTmsTheta = tms;
            _minimumPi = pi;

            textBoxEmsPi.Text = pi.ToString();
            textBoxTmsPi.Text = pi.ToString();
            textBoxEmsTheta.Text = ems.ToString();
            textBoxTmsTheta.Text = tms.ToString();
        }

        Dictionary<string, double> dict_r = new Dictionary<string, double>();
        Dictionary<string, double> dict_i = new Dictionary<string, double>();

        public FormPRM()
        {
            InitializeComponent();
            
            comboBoxScheduler.SelectedIndex = 0;
            comboBoxApproach.SelectedIndex = 0;
            comboBoxTaskSet.SelectedIndex = 0;
            
            DataTable dtEMS = new DataTable();
            dtEMS.Columns.Add("Period");
            dtEMS.Columns.Add("ExecutionTime");
            dtEMS.Columns.Add("Priority");

            DataTable dtTMS = new DataTable();
            dtTMS.Columns.Add("Period");
            dtTMS.Columns.Add("ExecutionTime");
            dtTMS.Columns.Add("Priority");
            
            DataTable dtSync = new DataTable();
            dtSync.Columns.Add("Period");
            dtSync.Columns.Add("ExecutionTime");
            dtSync.Columns.Add("Phase");
            

#if KEFICO
            NewWorkload(dtEMS, 1000, 13.375, 1);
            NewWorkload(dtEMS, 2000, 13.405, 2);
            NewWorkload(dtEMS, 5000, 3.43, 3);
            NewWorkload(dtEMS, 10000, 1401.215, 4);
            NewWorkload(dtEMS, 20000, 543.8, 5);
            NewWorkload(dtEMS, 50000, 218.855, 6);
            NewWorkload(dtEMS, 100000, 1361.82, 7);
            NewWorkload(dtEMS, 200000, 289.105, 8);
            NewWorkload(dtEMS, 1000000, 222.025, 9);

            NewWorkload(dtTMS, 1000, 5.375, 1);
            NewWorkload(dtTMS, 2000, 5.405, 2);
            NewWorkload(dtTMS, 5000, 50.43, 3);
            NewWorkload(dtTMS, 10000, 510.215, 4);
            NewWorkload(dtTMS, 20000, 430.8, 5);
            NewWorkload(dtTMS, 50000, 1080.855, 6);
            NewWorkload(dtTMS, 100000, 2101.82, 7);
            NewWorkload(dtTMS, 200000, 3389.105, 8);
            NewWorkload(dtTMS, 1000000, 4120.025, 9);

            NewSyncTask(dtSync, 5000, 122.945, 0);
            NewSyncTask(dtSync, 5000, 42.77, 1000);

            textBoxPi.Text = "600";
            textBoxTheta.Text = "200";
            
            textBoxEmsPi.Text = "600";
            textBoxEmsTheta.Text = "200";

            textBoxTmsPi.Text = "600";
            textBoxTmsTheta.Text = "200";
#else
            NewWorkload(dtEMS, 10, 1, 1);
            NewWorkload(dtEMS, 30, 4, 2);

            NewWorkload(dtTMS, 12, 2, 1);
            NewWorkload(dtTMS, 28, 3, 2);

            NewSyncTask(dtSync, 50, 15, 0);
            NewSyncTask(dtSync, 50, 5, 5);
            
            textBoxTheta.Text = "3.5";
            textBoxPi.Text = "10";
#endif
            
            dataGridView1.DataSource = dtEMS;
            dataGridView2.DataSource = dtTMS;
            dataGridViewSync.DataSource = dtSync;
            
            SettingParameters();

            SettingPictureBox();
            SettingLinePanel();
        }

        private void SettingLinePanel()
        {
            this.panelLine.Paint += new PaintEventHandler(panelLine_Paint);
        }
        void panelLine_Paint(object sender, PaintEventArgs e)
        {
            Panel p = sender as Panel;
            e.Graphics.DrawLine(Pens.Black, new Point(0, 0), new Point(0, p.Height / 2));
            e.Graphics.DrawLine(Pens.Black, new Point(p.Width-1, 0), new Point(p.Width-1, p.Height / 2));
            e.Graphics.DrawLine(Pens.Black, new Point(0, p.Height/2), new Point(p.Width, p.Height / 2));
            e.Graphics.DrawLine(Pens.Black, new Point(p.Width / 2, p.Height / 2), new Point(p.Width / 2, p.Height));
        }

        private void SettingPictureBox()
        {
            this.pictureBoxEMS.Paint += new PaintEventHandler(pictureBoxEMS_Paint);
            this.pictureBoxTMS.Paint += new PaintEventHandler(pictureBoxTMS_Paint);
        }
        private void SettingIntervalLength()
        {
            if (comboBoxTaskSet.SelectedItem.ToString() == "EMS")
                _intervalLength = _LcmEms;
            else
                _intervalLength = _LcmTms;
        }
        void pictureBoxTMS_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            string component = "TMS Component";
            Font fontComp = new Font(FontFamily.GenericSansSerif, 12);
            SizeF sizeComp = e.Graphics.MeasureString(component, fontComp);
            PointF locationComp = new PointF();
            locationComp.X = (pictureBoxTMS.Width - sizeComp.Width) / 2;
            locationComp.Y = (pictureBoxTMS.Height - sizeComp.Height) / 10;
            e.Graphics.DrawString(component, fontComp, Brushes.Black, locationComp);

            string title = "TMS Periodic Tasks";
            Font titleFont = new Font(this.Font, FontStyle.Bold);
            SizeF titleSize = e.Graphics.MeasureString(title, titleFont);
            PointF titleLocation = new PointF();
            titleLocation.X = (pictureBoxTMS.Width / 2) - (titleSize.Width / 2);
            titleLocation.Y = (pictureBoxTMS.Height / 2) - (titleSize.Height);
            e.Graphics.DrawString(title, titleFont, Brushes.Black, titleLocation);

            string pi = "Period (Π)";
            SizeF piSize = e.Graphics.MeasureString(pi, Font);
            PointF piLocation = new PointF();
            piLocation.X = textBoxTmsPi.Location.X - piSize.Width - 8;
            piLocation.Y = textBoxTmsPi.Location.Y + (piSize.Height / 4);
            e.Graphics.DrawString(pi, Font, Brushes.Black, piLocation);

            string theta = "Execution (Θ)";
            SizeF thetaSize = e.Graphics.MeasureString(theta, Font);
            PointF thetaLocation = new PointF();
            thetaLocation.X = textBoxTmsTheta.Location.X - thetaSize.Width - 8;
            thetaLocation.Y = textBoxTmsTheta.Location.Y + (thetaSize.Height / 4);
            e.Graphics.DrawString(theta, Font, Brushes.Black, thetaLocation);
        }
        void pictureBoxEMS_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            
            string component = "EMS Component";
            Font fontComp = new Font(FontFamily.GenericSansSerif, 12);
            SizeF sizeComp = e.Graphics.MeasureString(component, fontComp);
            PointF locationComp = new PointF();
            locationComp.X = (pictureBoxEMS.Width - sizeComp.Width) / 2;
            locationComp.Y = (pictureBoxEMS.Height - sizeComp.Height) / 10;
            e.Graphics.DrawString(component, fontComp, Brushes.Black, locationComp);

            string title = "EMS Periodic Tasks";
            Font titleFont = new Font(this.Font, FontStyle.Bold);
            SizeF titleSize = e.Graphics.MeasureString(title, titleFont);
            PointF titleLocation = new PointF();
            titleLocation.X = (pictureBoxEMS.Width / 4) - (titleSize.Width / 2);
            titleLocation.Y = (pictureBoxEMS.Height / 2) - (titleSize.Height);
            e.Graphics.DrawString(title, titleFont, Brushes.Black, titleLocation);
            
            string pi = "Period (Π)";
            SizeF piSize = e.Graphics.MeasureString(pi, Font);
            PointF piLocation = new PointF();
            piLocation.X = textBoxEmsPi.Location.X - piSize.Width - 8;
            piLocation.Y = textBoxEmsPi.Location.Y + (piSize.Height / 4);
            e.Graphics.DrawString(pi, Font, Brushes.Black, piLocation);

            string theta = "Execution (Θ)";
            SizeF thetaSize = e.Graphics.MeasureString(theta, Font);
            PointF thetaLocation = new PointF();
            thetaLocation.X = textBoxEmsTheta.Location.X - thetaSize.Width - 8;
            thetaLocation.Y = textBoxEmsTheta.Location.Y + (thetaSize.Height / 4);
            e.Graphics.DrawString(theta, Font, Brushes.Black, thetaLocation);

            string sync = "Sync tasks";
            SizeF sizeSync = e.Graphics.MeasureString(sync, titleFont);
            PointF locationSync = new PointF();
            locationSync.X = dataGridViewSync.Location.X + (dataGridViewSync.Size.Width - sizeSync.Width) / 2;
            locationSync.Y = dataGridViewSync.Location.Y - (sizeSync.Height * (float)1.1);
            e.Graphics.DrawString(sync, titleFont, Brushes.Black, locationSync);
        }
        
        private void SettingParameters()
        {
            DataTable dtEMS = dataGridView1.DataSource as DataTable;
            DataTable dtTMS = dataGridView2.DataSource as DataTable;

            double pminEMS = Get_Pmin(dtEMS);
            double pminTMS = Get_Pmin(dtTMS);


            _incrementUnit = (Math.Min(pminEMS, pminTMS) / 10);
            textBoxIncrementUnit.Text = _incrementUnit.ToString();
            
            _LcmEms = GetLCM_forWorkload(dtEMS);
            _LcmTms = GetLCM_forWorkload(dtTMS);
            
            textBoxLcmEms.Text = _LcmEms.ToString();
            textBoxUwEms.Text = GetUtilizationOfWorkload(dtEMS).ToString();
            textBoxPminEms.Text = Get_Pmin(dtEMS).ToString();

            textBoxLcmTms.Text = _LcmTms.ToString();
            textBoxUwTms.Text = GetUtilizationOfWorkload(dtTMS).ToString();
            textBoxPminTms.Text = Get_Pmin(dtTMS).ToString();
            
            textBoxContextSwitchOverhead.Text = "0";
            
            _overhead = 0;

            SettingIntervalLength();
        }

        private long GetLCM_forWorkload(DataTable dt)
        {
            Task firstTask = new Task(dt.Rows[0]);
            long LCM = (long)firstTask.Period;
            
            foreach (DataRow dr in dt.Rows)
            {
                Task t = new Task(dr);
                LCM = GetLCM(LCM, (long)t.Period);
            }

            return LCM;
        }
        private double GetUtilizationOfWorkload(DataTable dt)
        {
            double SumUtilization = 0;
            foreach (DataRow dr in dt.Rows)
            {
                Task t = new Task(dr);
                SumUtilization += t.Utilization;
            }

            return SumUtilization;
        }

        public long GetGCD(long p, long q)
        {
            if (q == 0) 
                return p;
            
            long r = p % q;
            
            return GetGCD(q, r);
        }
        private long GetLCM(long num1, long num2)
        {
            if (num1 < num2)
            {
                long temp = num1;
                num1 = num2;
                num2 = temp;
            }
            return (num1 * num2) / GetGCD(num1, num2);
        }

        private void NewWorkload(DataTable dt, double period, double executionTime, int priority)
        {
            DataRow dr = dt.NewRow();
            dr["Period"] = period;
            dr["ExecutionTime"] = executionTime;
            dr["Priority"] = priority;
            dt.Rows.Add(dr);
        }
        private void NewSyncTask(DataTable dtSync, double period, double execution, double phase)
        {
            DataRow dr = dtSync.NewRow();
            dr["Period"] = period;
            dr["ExecutionTime"] = execution;
            dr["Phase"] = phase;
            dtSync.Rows.Add(dr);
        }

        private DataTable GetSelectedTaskSet()
        {
            DataTable dt;
            if (comboBoxTaskSet.SelectedItem.ToString() == "EMS")
                dt = dataGridView1.DataSource as DataTable;
            else
                dt = dataGridView2.DataSource as DataTable;

            return dt;
        }

        private void buttonPlotEDF_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            FormChart chart = new FormChart("Resource demand / supply");
            chart.GetLable.Text = "Schedulable";

            try
            {
                Series seriesDBF = new Series("Resource Demand");
                seriesDBF.ChartArea = "ChartArea1";
                seriesDBF.ChartType = SeriesChartType.StepLine;
                seriesDBF.Legend = "Legend1";
                seriesDBF.Color = Color.Blue;

                Series seriesSBF = new Series("Resurce Supply");
                seriesSBF.ChartArea = "ChartArea1";
                seriesSBF.ChartType = SeriesChartType.Line;
                seriesSBF.Legend = "Legend1";
                seriesSBF.Color = Color.Red;

                DataTable dt = GetSelectedTaskSet();

                
                List<long> list = GetList(dt);
                foreach (long l in list)
                {
                    double t = (double)l;

                    double dbf = GetDBF_W(dt, t);
                    double sbf = GetSBF_W(dt, _PI, _THETA, t);

                    if (dbf > sbf)
                        chart.GetLable.Text = "Not Schedulable";

                    seriesDBF.Points.Add(new DataPoint(t, dbf));
                    seriesSBF.Points.Add(new DataPoint(t, sbf));
                }
                
                /*
                for (double t = 0; t <= _intervalLength; t += 1)
                {
                    double dbf = GetDBF_W(dt, t);
                    double sbf = GetSBF_W(dt, _PI, _THETA, t);

                    if (dbf > sbf)
                        chart.GetLable.Text = "Not Schedulable";

                    seriesDBF.Points.Add(new DataPoint(t, dbf));
                    seriesSBF.Points.Add(new DataPoint(t, sbf));
                }
                */

                chart.AxisX.Title = "Interval Length";
                chart.AxisY.Title = "Resource demand / supply";
                chart.AxisX.Minimum = 0;
                
                //chart.AxisX.Maximum = 7;
                //chart.AxisX.Interval = 1;
                //chart.AxisY.Interval = 1;
                //chart.AxisY.Maximum = 8;
                //chart.EnableLegend = false;

                chart.AddSeries(seriesDBF);
                chart.AddSeries(seriesSBF);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            chart.Show();

        }
        private void buttonRM_Click(object sender, EventArgs e)
        {
            DataTable dt = GetSelectedTaskSet();

            string message;
            string result;
            if (CheckSchedulabilityRM(dt, _PI, _THETA, out message))
                result = "Schedulable";
            else
                result = "Not Schedulable";

            MessageBox.Show("[" + comboBoxTaskSet.SelectedItem.ToString() + " Task set]\r\n\r\n" + "For given Γ(" + _PI + "," + _THETA + "),\r\n"
                +  message + "W is " + result + ".", "Exact Analysis under RM");
        }
        [Obsolete]
        private void buttonRM_Click_OLD(object sender, EventArgs e)
        {
            dict_r.Clear();
            dict_i.Clear();

            /*
            // For debugging
            double i_1_1 = I_of_k(1, 1);
            double r_1_1 = r_of_k(1, 1);
            double i_1_2 = I_of_k(1, 2);
            double r_1_2 = r_of_k(1, 2);

            double i_2_1 = I_of_k(2, 1);
            double r_2_1 = r_of_k(2, 1);
            double i_2_2 = I_of_k(2, 2);
            double r_2_2 = r_of_k(2, 2);
            double i_2_3 = I_of_k(2, 3);
            double r_2_3 = r_of_k(2, 3);
            double i_2_4 = I_of_k(2, 4);
            double r_2_4 = r_of_k(2, 4);
            */


            // CAUTION!!! 
            // i = priority = the task number

            DataTable dt = GetSelectedTaskSet();
            int number_of_tasks = dt.Rows.Count;

            bool result = true;
            for (int i = 1; i <= number_of_tasks; i++)
            {
                int k = 1;
                double i_value = GetI_of_k(dt, i, k);
                double r_value = GetR_of_k(dt, i, k);
                double r_old_value = r_value;

                while (true)
                {
                    i_value = GetI_of_k(dt, i, k);
                    r_value = GetR_of_k(dt, i, k);

                    if (r_old_value == r_value)
                        break;

                    r_old_value = r_value;
                    k++;
                }

                bool b = r_value <= GetPeriodByTaskNumber(dt, i);
                Console.WriteLine("r_" + i + " = " + r_value + "\t[Schedulability = " + b + "]");

                if (false == b)
                    result = false;

            }
            Console.WriteLine("RM schedulability = " + result);
        }
        private void buttonABunderEDF_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            FormChart chart = new FormChart("Solution Space under EDF");
            try
            {
                DataTable dt = GetSelectedTaskSet();
                double period_min = Get_Pmin(dt);
                
                Series s2 = new Series("Solution space by AB under EDF");
                s2.ChartArea = "ChartArea1";
                s2.ChartType = SeriesChartType.Line;
                s2.Legend = "Legend1";
                s2.Color = Color.Tomato;
                
                for (double i = _incrementUnit; i <= period_min; i += _incrementUnit)
                {
                    double maxTheta = i - 1;
                    int max_k = GetK_underEDF(dt, i, maxTheta);
                    double AB = GetAB_EDF(dt, max_k);
                    if (double.IsInfinity(AB))
                        continue;
                    s2.Points.Add(new DataPoint(i, AB + 2 * _overhead / i));
                }

                chart.AxisX.Title = "Resource period (Π)";
                chart.AxisY.Title = "Resource capacity (Θ/Π)";// "Resource allocation (Θ)";
                chart.AxisX.Minimum = _incrementUnit;
                chart.AxisY.Maximum = 1;

                chart.EnableLegend = false;

                chart.AddSeries(s2);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            chart.Show();
        }
        private void buttonABunderRM_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            FormChart chart = new FormChart("Solution Space under RM");
            try
            {
                DataTable dt = GetSelectedTaskSet();
                double period_min = Get_Pmin(dt);
                
                Series s2 = new Series("Solution space by AB under RM");
                s2.ChartArea = "ChartArea1";
                s2.ChartType = SeriesChartType.Line;
                s2.Legend = "Legend1";
                s2.Color = Color.Tomato;
                
                for (double i = _incrementUnit; i <= period_min; i += _incrementUnit)
                {
                    double maxTheta = i - 1;
                    int max_k = GetK_underRM(dt, i, maxTheta);
                    double AB = GetAB_RM(dt, max_k);
                    if (double.IsInfinity(AB))
                        continue;
                    s2.Points.Add(new DataPoint(i, AB + 2 * _overhead / i));
                }

                chart.AxisX.Title = "Resource period (Π)";
                chart.AxisY.Title = "Resource capacity (Θ/Π)";// "Resource allocation (Θ)";
                chart.AxisX.Minimum = _incrementUnit;
                chart.AxisY.Maximum = 1;

                chart.EnableLegend = false;

                chart.AddSeries(s2);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            chart.Show();
        }
 
        /* Obsolete Plot Sum with no CS overhead
        [Obsolete]
        private void PlotSumOfEmsTms()
        {
            this.Cursor = Cursors.WaitCursor;
            FormChart chart = new FormChart("Solution Space under RM (by Exhaustive search)");
            try
            {
                chart.AxisX.Title = "Resource period (Π)";
                chart.AxisY.Title = "Resource capacity (Θ/Π)";// "Resource allocation (Θ)";
                chart.AxisX.Minimum = 1;
                chart.AxisY.Maximum = 1;

                DataTable dtEMS = dataGridView1.DataSource as DataTable;
                DataTable dtTMS = dataGridView2.DataSource as DataTable;

                Series seriesSum = GetExactAnalysisSeriesRM_SumOfTwo(dtEMS, dtTMS, "Sum of EMS and TMS");

                if (checkBoxShowEmsTms.Checked)
                {
                    Series seriesEMS = GetExactAnalysisSeriesRM(dtEMS, "EMS");
                    Series seriesTMS = GetExactAnalysisSeriesRM(dtTMS, "TMS");
                    seriesTMS.Color = Color.Green;
                    seriesEMS.Color = Color.Blue;
                    seriesSum.Color = Color.Red;

                    chart.AddSeries(seriesEMS);
                    chart.AddSeries(seriesTMS);
                }                
                
                chart.AddSeries(seriesSum);

            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            chart.SetMinValues(_minimumPi, _minimumEmsTheta, _minimumTmsTheta);
            chart.Show();
        }
        */

        private void buttonEMS_TMS_Click(object sender, EventArgs e)
        {
            InputInvalidationCheck();

            this.Cursor = Cursors.WaitCursor;

            _overhead = Math.Max(0, _overhead);

            FormChart chart = new FormChart("Solution Space under RM");
            try
            {
                chart.AxisX.Title = "Resource period (Π)";
                chart.AxisY.Title = "Resource capacity (Θ/Π)";// "Resource allocation (Θ)";
                chart.AxisX.Minimum = _incrementUnit;
                chart.EnableLegend = true;

                DataTable dtEMS = dataGridView1.DataSource as DataTable;
                DataTable dtTMS = dataGridView2.DataSource as DataTable;

                Series seriesSum;
                seriesSum = GetExactAnalysisSeriesRM_SumOfTwo(dtEMS, dtTMS, "EMS + TMS", _overhead);

                if (checkBoxShowEmsTms.Checked)
                {
                    Series seriesEMS = GetExactAnalysisSeriesRM(dtEMS, "EMS", _overhead);
                    Series seriesTMS = GetExactAnalysisSeriesRM(dtTMS, "TMS", _overhead);
                    seriesTMS.Color = Color.Green;
                    seriesEMS.Color = Color.Blue;
                    seriesSum.Color = Color.Red;

                    chart.AddSeries(seriesEMS);
                    chart.AddSeries(seriesTMS);
                }                

                chart.AddSeries(seriesSum);

            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            chart.SetMinValues(_minimumPi, _minimumEmsTheta, _minimumTmsTheta);
            chart.ShowDialog();
        }
        private void buttonSolutionSpaceEDF_Click(object sender, EventArgs e)
        {
            DataTable dt = GetSelectedTaskSet();
            DrawSolutionSpaceEDF(dt);
        }
        private void buttonKplot_Click(object sender, EventArgs e)
        {
            DataTable dt = GetSelectedTaskSet();

            FormChart chart = new FormChart("Abstraction Bound under RM");

            Series series = new Series("k space");
            series.ChartArea = "ChartArea1";
            series.ChartType = SeriesChartType.Line;
            series.Legend = "Legend1";

            double p_min = Get_Pmin(dt);
            for (double i = 0; i < p_min; i += 1)
            {
                for (double j = 0; j < i; j += 1)
                {
                    //double[] jk = new double[2];
                    //jk[0] = j;
                    //jk[1] = GetK_underRM(i, j);
                    Console.WriteLine(i + "\t" + j + "\t" + GetK_underRM(dt, i, j));
                    //series.Points.Add(new DataPoint(i, jk));
                }
            }

            //chart.AddSeries(series);
            //chart.Show();


        }
        private void buttonGetTheta_Click(object sender, EventArgs e)
        {
            DataTable dt = GetSelectedTaskSet();
            double theta = GetTheLeastThetaForGivenPi_RM(dt, _PI, _overhead);
            Console.WriteLine("For given Π=" + _PI + ", the least Θ=" + theta);
            MessageBox.Show("For given Π=" + _PI + ", the least Θ=" + theta, "Exact Analysis");
        }
        private void buttonSolutionSpaceRM_Click(object sender, EventArgs e)
        {
            DataTable dt = GetSelectedTaskSet();
            DrawSoultionSpaceRM(dt);
        }
        
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            SettingParameters();
        }
        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            SettingParameters();
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.DataSource == null)
                return;

            DataTable dt = dataGridView1.DataSource as DataTable;

            try
            {

                switch (e.ColumnIndex)
                {
                    case 0:
                        double.Parse(dt.Rows[e.RowIndex][e.ColumnIndex].ToString());
                        break;
                    case 1:
                        double.Parse(dt.Rows[e.RowIndex][e.ColumnIndex].ToString());
                        break;
                    case 2:
                        int.Parse(dt.Rows[e.RowIndex][e.ColumnIndex].ToString());
                        break;
                    default:
                        break;
                }
            }
            catch (IndexOutOfRangeException ioex)
            {
            }
            catch (Exception)
            {
                dt.Rows[e.RowIndex][e.ColumnIndex] = "1";
                //MessageBox.Show("Invalid input!\r\nCheck your input again.", "Caution", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.DataSource == null)
                return;

            DataTable dt = dataGridView2.DataSource as DataTable;

            try
            {

                switch (e.ColumnIndex)
                {
                    case 0:
                        double.Parse(dt.Rows[e.RowIndex][e.ColumnIndex].ToString());
                        break;
                    case 1:
                        double.Parse(dt.Rows[e.RowIndex][e.ColumnIndex].ToString());
                        break;
                    case 2:
                        int.Parse(dt.Rows[e.RowIndex][e.ColumnIndex].ToString());
                        break;
                    default:
                        break;
                }
            }
            catch (IndexOutOfRangeException ioex)
            {
            }
            catch (Exception)
            {
                dt.Rows[e.RowIndex][e.ColumnIndex] = "1";
                //MessageBox.Show("Invalid input!\r\nCheck your input again.", "Caution", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void textBoxTheta_TextChanged(object sender, EventArgs e)
        {
            try
            {
                _THETA = double.Parse(textBoxTheta.Text);
            }
            catch (FormatException fe)
            {
            }
            catch (Exception)
            {
                _THETA = 1;
                textBoxTheta.Text = "1";
                //MessageBox.Show("Invalid input!\r\nCheck your input again.", "Caution", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (comboBoxTaskSet.SelectedItem.ToString() == "EMS")
                textBoxEmsTheta.Text = _THETA.ToString();
            else
                textBoxTmsTheta.Text = _THETA.ToString();

            if (_THETA > _PI)
                textBoxPi.Text = textBoxTheta.Text;
        }
        private void textBoxPi_TextChanged(object sender, EventArgs e)
        {
            try
            {
                _PI = double.Parse(textBoxPi.Text);
            }
            catch (FormatException fe)
            {
            }
            catch (Exception)
            {
                _PI = 1;
                textBoxPi.Text = "1";
                //MessageBox.Show("Invalid input!\r\nCheck your input again.", "Caution", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (comboBoxTaskSet.SelectedItem.ToString() == "EMS")
                textBoxEmsPi.Text = _PI.ToString();
            else
                textBoxTmsPi.Text = _PI.ToString();
        }
        private void textBoxIncrementUnit_TextChanged(object sender, EventArgs e)
        {
            try
            {
                _incrementUnit = double.Parse(textBoxIncrementUnit.Text);

            }
            catch (FormatException fe)
            {
            }
            catch (Exception)
            {
                _incrementUnit = 1;
                textBoxIncrementUnit.Text = "1";
                //MessageBox.Show("Invalid input!\r\nCheck your input again.", "Caution", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void textBoxContextSwitchOverhead_TextChanged(object sender, EventArgs e)
        {
            TextBox t = sender as TextBox;
            try
            {
                _overhead = double.Parse(textBoxContextSwitchOverhead.Text);
            }
            catch (FormatException fe)
            {
            }
            catch (Exception)
            {
                _overhead = 0;
                textBoxContextSwitchOverhead.Text = "0";
            }

            if (_overhead < 0)
            {
                _overhead = 0;
                textBoxContextSwitchOverhead.Text = "0";
            }

            if (_overhead > double.Parse(textBoxIncrementUnit.Text))
                textBoxIncrementUnit.Text = t.Text;
        }
        private void textBoxLCM_TextChanged(object sender, EventArgs e)
        {
            try
            {
                _LcmEms = double.Parse(textBoxLcmEms.Text);
            }
            catch (Exception)
            {
                //MessageBox.Show("Invalid input!\r\nCheck your input again.", "Caution", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void textBoxEmsTheta_TextChanged(object sender, EventArgs e)
        {
            TextBox t = sender as TextBox;
            double theta = 0;

            try
            {
                theta = double.Parse(t.Text);
            }
            catch (FormatException fe)
            {
            }
            catch (Exception)
            {
                t.Text = "0";
                //MessageBox.Show("Invalid input!\r\nCheck your input again.", "Caution", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (theta > double.Parse(textBoxEmsPi.Text))
                textBoxEmsPi.Text = t.Text;
        }
        private void textBoxTmsTheta_TextChanged(object sender, EventArgs e)
        {
            TextBox t = sender as TextBox;
            double theta = 0;

            try
            {
                theta = double.Parse(t.Text);
            }
            catch (FormatException fe)
            {
            }
            catch (Exception)
            {
                t.Text = "0";
                //MessageBox.Show("Invalid input!\r\nCheck your input again.", "Caution", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (theta > double.Parse(textBoxTmsPi.Text))
                textBoxTmsPi.Text = t.Text;
        }
        private void textBoxEmsPi_TextChanged(object sender, EventArgs e)
        {            
            TextBox t = sender as TextBox;
            double pi = 1;

            try
            {
                pi = double.Parse(t.Text);
            }
            catch (FormatException fe)
            {
            }
            catch (Exception)
            {
                t.Text = "1";
                //MessageBox.Show("Invalid input!\r\nCheck your input again.", "Caution", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private double GetDBF_W(DataTable dt, double t)
        {
            double dbf_sum = 0;
            foreach (DataRow dr in dt.Rows)
            {
                double p_i = double.Parse(dr["Period"].ToString());
                double e_i = double.Parse(dr["ExecutionTime"].ToString()) + _overhead;
                dbf_sum += Math.Floor(t / p_i) * e_i;
            }

            return dbf_sum;
        }
        private double GetSBF_W(DataTable dt, double pi, double theta, double t)
        {
            double temp = Math.Floor(((t - (pi - theta)) / pi));
            double sbf = temp * theta + Math.Max(t - 2 * (pi - theta) - pi * temp, 0);
            return Math.Max(0, sbf);
        }
        private double GetTBF(double t)
        {
            double t_floor = Math.Floor(t / _THETA);
            double t_ep = t - _THETA * t_floor;

            double t_epsilon = 0;
            if (t_ep > 0)
                t_epsilon = _PI - _THETA + t_ep;

            // Error??
            return (_PI - _THETA) + _PI * t_floor + t_epsilon;
        }
        private double GetR_of_k(DataTable dt, int i, int k)
        {
            // Initial Condition
            if (k == 0)
                return GetExecutionTimeByTaskNumber(dt, i);

            // Dynamic programming
            string key = i + "_" + k;
            if (dict_r.ContainsKey(key))
                return dict_r[key];

            double value = GetTBF(GetI_of_k(dt, i, k));
            dict_r.Add(key, value);
            return value;
        }
        private double Get_i(DataTable dt, int i)
        {
            double value = GetExecutionTimeByTaskNumber(dt, i);

            List<DataRow> listDr = GetAllHigherPriorityTasks(dt, i);
            foreach (DataRow dr in listDr)
            {
                int currentTaskNumber = int.Parse(dr["Priority"].ToString());
                value += Math.Ceiling(GetPeriodByTaskNumber(dt, i) / GetPeriodByTaskNumber(dt, currentTaskNumber)) * GetExecutionTimeByTaskNumber(dt, currentTaskNumber);
            }

            return value;
        }
        private double GetI_of_k(DataTable dt, int i, int k)
        {
            // Dynamic programming
            string key = i + "_" + k;
            if (dict_i.ContainsKey(key))
                return dict_i[key];

            // CAUTION!!! 
            // i = priority = the task number

            double e_i = GetExecutionTimeByTaskNumber(dt, i);

            List<DataRow> listDr = GetAllHigherPriorityTasks(dt, i);
            double sum = 0;
            foreach (DataRow dr in listDr)
            {
                // Current task infos
                int taskNumber = int.Parse(dr["Priority"].ToString());
                double taskPeriod = double.Parse(dr["Period"].ToString());
                double taskExecution = double.Parse(dr["ExecutionTime"].ToString());

                sum += Math.Ceiling(GetR_of_k(dt, i, k - 1) / taskPeriod) * taskExecution;
            }

            double value = e_i + sum;
            dict_i.Add(key, value);
            return value;
        }
        private double GetDBF_RM(DataTable dt, double t, int i, double csOverhead)
        {
            double result = GetExecutionTimeByTaskNumber(dt, i);

            List<DataRow> higherPriorities = GetAllHigherPriorityTasks(dt, i);
            foreach (DataRow dr in higherPriorities)
            {
                double taskPeriod = double.Parse(dr["Period"].ToString());
                double taskExecution = double.Parse(dr["ExecutionTime"].ToString());
                taskExecution += 2 * csOverhead;

                result += Math.Ceiling(t / taskPeriod) * taskExecution;
            }

            return result;
        }
        [Obsolete]
        private double GetDBF_RM(DataTable dt, double t, int i)
        {
            double result = GetExecutionTimeByTaskNumber(dt, i);

            List<DataRow> higherPriorities = GetAllHigherPriorityTasks(dt, i);
            foreach (DataRow dr in higherPriorities)
            {
                double taskPeriod = double.Parse(dr["Period"].ToString());
                double taskExecution = double.Parse(dr["ExecutionTime"].ToString());

                result += Math.Ceiling(t / taskPeriod) * taskExecution;
            }

            return result;
        }
        private double GetSBF_RM(double t, double Pi, double Theta)
        {
            double result;

            double k_temp = Math.Ceiling((t - (Pi - Theta)) / Pi);
            double k = Math.Max(1, k_temp);

            if (t >= ((k + 1) * Pi - 2 * Theta) && t <= ((k + 1) * Pi - Theta))
                result = t - (k + 1) * (Pi - Theta);
            else
                result = (k - 1) * Theta;

            return result;
        }
        private double GetDBF_Sporadic_EDF(List<Task> tasks, double t)
        {
            double dbf_sum = 0;
            foreach (Task task in tasks)
            {
                double floor = Math.Floor((t - task.Phase - task.Deadline) / task.Period + 1);
                double times = floor * task.ExecutionTime;
                dbf_sum += Math.Max(0, times);
            }

            return dbf_sum;
        }

        private bool CheckSchedulabilityRM(DataTable dt, double pi, double theta, out string message)
        {
            message = String.Empty;

            foreach (DataRow dr in dt.Rows)
            {
                double taskPeriod = double.Parse(dr["Period"].ToString());
                int i = int.Parse(dr["Priority"].ToString());

                double dbf = GetDBF_RM(dt, taskPeriod, i, _overhead);
                double sbf = GetSBF_RM(taskPeriod, pi, theta);

                bool b = (dbf > sbf);

                //Console.WriteLine("Task " + i + " dbf=" + dbf + " sbf=" + sbf + " : " + b + " Schedulability : " + schedulable);
                message += "   Task " + i + " Schedulability : " + !b + "\r\n";

                if (b)
                    return false;
            }

            return true;
        }
        private bool CheckSchedulabilityRM(DataTable dt, double pi, double theta, double csOverhead)
        {
            bool TrueForAllTasks = true;
            foreach (DataRow dr in dt.Rows)
            {
                Task t = new Task(dr);

                double dbf = GetDBF_RM(dt, t.Period, t.Priority, csOverhead);
                double sbf = GetSBF_RM(t.Period, pi, theta);

                if (dbf > sbf)
                    TrueForAllTasks = false;
            }

            return TrueForAllTasks;
        }
        private bool CheckSchedulabilityRM(DataTable dt, double pi, double theta)
        {
            bool TrueForAllTasks = true;
            foreach (DataRow dr in dt.Rows)
            {
                Task t = new Task(dr);

                double dbf = GetDBF_RM(dt, t.Period, t.Priority, _overhead);
                double sbf = GetSBF_RM(t.Period, pi, theta);

                if (dbf > sbf)
                    TrueForAllTasks = false;
            }

            return TrueForAllTasks;
        }
        private bool CheckSchedulabilityEDF(DataTable dt, double pi, double theta, double t)
        {
            for (double i = 1; i <= t; i += _incrementUnit)
            {
                double dbf = GetDBF_W(dt, i);
                double sbf = GetSBF_W(dt, pi, theta, i);

                if (dbf > sbf)
                    return false;
            }

            return true;
        }

        private List<DataRow> GetAllHigherPriorityTasks(DataTable dt, int priority)
        {
            List<DataRow> listDataRow = new List<DataRow>();

            foreach (DataRow dr in dt.Rows)
            {
                // Priority value lower has higher priority
                if (int.Parse(dr["Priority"].ToString()) < priority)
                    listDataRow.Add(dr);
            }

            return listDataRow;
        }
        private DataRow GetOneHigherPriorityWorkload(DataTable dt, int priority)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (int.Parse(dr["Priority"].ToString()) == priority - 1)
                    return dr;
            }

            return null;
        }
        private double GetPeriodByTaskNumber(DataTable dt, int i)
        {
            // i : not an index. It is the task number and also is priority
            foreach (DataRow dr in dt.Rows)
            {
                if (int.Parse(dr["Priority"].ToString()) == i)
                    return double.Parse(dr["Period"].ToString());
            }

            Console.WriteLine("ERROR: cannot find " + i + "-th task.");
            throw new ArgumentException();
        }
        private double GetExecutionTimeByTaskNumber(DataTable dt, int i)
        {
            // i : not an index. It is the task number and also is priority
            foreach (DataRow dr in dt.Rows)
            {
                if (int.Parse(dr["Priority"].ToString()) == i)
                    return double.Parse(dr["ExecutionTime"].ToString());
            }

            Console.WriteLine("ERROR: cannot find " + i + "-th task.");
            throw new ArgumentException();
        }
        private double Get_Pmin(DataTable dt)
        {
            double period = double.MaxValue;
            foreach (DataRow dr in dt.Rows)
                period = Math.Min(double.Parse(dr["Period"].ToString()), period);

            return period;
        }
        [Obsolete]
        private void GetPCB_EDF(DataTable dt)
        {
            double max = 0;
            for (double t = Get_Pmin(dt); t <= _intervalLength; t = t + _incrementUnit)
            {
                double t_power = Math.Pow(t - 2 * _PI, 2);
                double t_plus = 8 * _PI * GetDBF_W(dt, t);
                double t_sqrt = Math.Sqrt(t_power + t_plus);
                double t_minus = t - 2 * _PI;
                double temp = (t_sqrt - t_minus) / (double)4;
                max = Math.Max(max, temp);

                //Console.WriteLine(t_power + " " + t_plus + " " + t_sqrt + " " + t_minus + " " + temp);
            }

            Console.WriteLine("PCB for EDF = " + max + " / " + _PI + "\t" + max / _PI);
        }
        [Obsolete]
        private void GetPCB_RM(DataTable dt)
        {
            double max = 0;
            for (int i = 1; i <= dt.Rows.Count; i++)
            {
                double t_minus = 2 * _PI - GetPeriodByTaskNumber(dt, i);
                double t_pow = Math.Pow(GetPeriodByTaskNumber(dt, i) - 2 * _PI, 2);
                double t_sqrt = Math.Sqrt(t_pow + 8 * _PI * Get_i(dt, i));
                double t_value = (t_minus + t_sqrt) / 4;
                max = Math.Max(max, t_value);
            }

            Console.WriteLine("PCB for RM = " + max + " / " + _PI + "\t" + max / _PI);
        }
        [Obsolete]
        private double GetTheLeastThetaForGivenPi_RM(DataTable dt, double pi)
        {
            double inc = pi / 100;
            for (double i = inc; i < pi; i += inc)
            {
                if (CheckSchedulabilityRM(dt, pi, i))
                    return i;
            }

            return pi;
        }            
        private double GetTheLeastThetaForGivenPi_RM(DataTable dt, double pi, double csOverhead)
        {
            double inc = pi/100;
            for (double i = Math.Max(inc, csOverhead); i < pi; i += inc)
            {
                if (CheckSchedulabilityRM(dt, pi, i, csOverhead))
                    return i;
            }

            return pi;
        }
        private double GetTheLeastThetaForGivenPi_EDF(DataTable dt, double pi)
        {
            double inc = pi / 100;
            for (double i = inc; i < pi; i += inc)
            {
                if (CheckSchedulabilityEDF(dt, pi, i, _LcmEms))
                    return i;
            }

            return pi;
        }

        private void DrawSolutionSpaceEDF(DataTable dt)
        {
            this.Cursor = Cursors.WaitCursor;
            FormChart chart = new FormChart("Solution Space under EDF");
            try
            {
                chart.AxisX.Title = "Resource period (Π)";
                chart.AxisY.Title = "Resource capacity (Θ/Π)";// "Resource allocation (Θ)";
                chart.AxisY.Maximum = 1;
                chart.AxisX.Minimum = _incrementUnit;
                chart.AddSeries(GetExactAnalysisSeriesEDF(dt, "Exact Analysis under EDF"));

                chart.EnableLegend = false;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            chart.Show();
        }
        private void DrawSoultionSpaceRM(DataTable dt)
        {
            this.Cursor = Cursors.WaitCursor;
            FormChart chart = new FormChart("Solution Space under RM");
            try
            {
                chart.AxisX.Title = "Resource period (Π)";
                chart.AxisY.Title = "Resource capacity (Θ/Π)";// "Resource allocation (Θ)";
                chart.AxisY.Maximum = 1;
                chart.AxisX.Minimum = _incrementUnit;
                chart.AddSeries(GetExactAnalysisSeriesRM(dt, "Exact Analysis under RM", _overhead));

                chart.EnableLegend = false;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            } 
            chart.Show();
        }

        private Series GetExactAnalysisSeriesEDF(DataTable dt, string title)
        {
            double period_min = Get_Pmin(dt);

            Series series = new Series(title);
            series.ChartArea = "ChartArea1";
            series.ChartType = SeriesChartType.Line;
            series.Legend = "Legend1";
            
            for (double i = _incrementUnit; i <= period_min; i += _incrementUnit)
            {
                series.Points.Add(new DataPoint(i, (GetTheLeastThetaForGivenPi_EDF(dt, i) + 2*_overhead) / i));
            }

            return series;
        }
        [Obsolete]
        private Series GetExactAnalysisSeriesRM(DataTable dt, string title)
        {
            double period_min = Get_Pmin(dt);

            Series series = new Series(title);
            series.ChartArea = "ChartArea1";
            series.ChartType = SeriesChartType.Line;
            series.Legend = "Legend1";
            
            for (double i = _incrementUnit; i <= period_min; i += _incrementUnit)
            {
                series.Points.Add(new DataPoint(i, GetTheLeastThetaForGivenPi_RM(dt, i) / i));
            }

            return series;
        }
        private Series GetExactAnalysisSeriesRM(DataTable dt, string title, double csOverhead)
        {
            double period_min = Get_Pmin(dt);

            Series series = new Series(title);
            series.ChartArea = "ChartArea1";
            series.ChartType = SeriesChartType.Line;
            series.Legend = "Legend1";

            for (double i = _incrementUnit; i <= period_min; i += _incrementUnit)
            {
                series.Points.Add(new DataPoint(i, (GetTheLeastThetaForGivenPi_RM(dt, i, csOverhead) + 2 * csOverhead)/ i));
            }

            return series;
        }

        private double GetAB_EDF(DataTable dt, int k)
        {
            double Uw = GetUtilizationOfWorkload(dt);
            return ((k + 2) * Uw) / (k + 2 * Uw);
        }
        private double GetAB_RM(DataTable dt, int k)
        {
            double Uw = GetUtilizationOfWorkload(dt);
            return (Uw) / (Math.Log((2 * k + 2 * (1 - Uw)) / (k + 2 * (1 - Uw)), Math.E));
        }
        private double GetAB_RM(int k, double Uw)
        {
            return (Uw) / (Math.Log((2 * k + 2 * (1 - Uw)) / (k + 2 * (1 - Uw)), Math.E));
        }

        private int GetK_underEDF(DataTable dt, double pi, double theta)
        {
            double p_min = Get_Pmin(dt);

            int k = 0;
            while ((k + 1) * pi - theta - ((k * theta) / (k + 2)) < p_min)
                k++;
            k--;

            return k;
        }
        private int GetK_underRM(DataTable dt, double pi, double theta)
        {
            double p_min = Get_Pmin(dt);

            int k = 0;
            while ((k + 1) * pi - theta < p_min)
                k++;
            k--;

            return k;
        }

        [Obsolete]
        private Series GetExactAnalysisSeriesRM_SumOfTwo(DataTable dt1, DataTable dt2, string title)
        {
            double period_min = Math.Min(Get_Pmin(dt1), Get_Pmin(dt2));

            Series series = new Series(title);
            series.ChartArea = "ChartArea1";
            series.ChartType = SeriesChartType.Line;
            series.Legend = "Legend1";
            
            double minimum = double.MaxValue;
            for (double i = _incrementUnit; i <= period_min; i += _incrementUnit)
            {
                double ems = GetTheLeastThetaForGivenPi_RM(dt1, i, _overhead);
                double tms = GetTheLeastThetaForGivenPi_RM(dt2, i, _overhead);
                double sum = (ems + tms) / i;
                
                if (minimum > sum)
                {
                    SetMinimumValues(i, ems, tms);

                    minimum = sum;
                }

                series.Points.Add(new DataPoint(i, sum));
            }

            return series;
        }
        private Series GetExactAnalysisSeriesRM_SumOfTwo(DataTable dt1, DataTable dt2, string title, double csOverhead)
        {
            double period_min = Math.Min(Get_Pmin(dt1), Get_Pmin(dt2));

            Series series = new Series(title);
            series.ChartArea = "ChartArea1";
            series.ChartType = SeriesChartType.Line;
            series.Legend = "Legend1";

            double minimum = double.MaxValue;
            for (double i = _incrementUnit; i <= period_min; i += _incrementUnit)
            {
                if (i == 0)
                    continue;

                double ems = GetTheLeastThetaForGivenPi_RM(dt1, i, csOverhead);
                double tms = GetTheLeastThetaForGivenPi_RM(dt2, i, csOverhead);
                double sum = (ems + tms + (4 * csOverhead)) / i;

                // if we don't care the CS overhead
                if (csOverhead == 0)
                {
                    if (minimum > sum)  // equality is the difference
                    {
                        SetMinimumValues(i, ems, tms);
                        minimum = sum;
                    }
                }
                else
                {
                    if (minimum >= sum)
                    {
                        SetMinimumValues(i, ems, tms);
                        minimum = sum;
                    }
                }

                series.Points.Add(new DataPoint(i, sum));
            }

            return series;
        }
        private void comboBoxTaskSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            SettingIntervalLength();
        }

        private void InputInvalidationCheck()
        {
            if (textBoxPi.Text.Trim() == string.Empty)
                textBoxPi.Text = "600";

            if (textBoxTheta.Text.Trim() == string.Empty || _PI < _THETA)
                textBoxTheta.Text = textBoxPi.Text;

            if (_incrementUnit <= 0 || textBoxIncrementUnit.Text.Trim() == string.Empty)
            {
                _incrementUnit = 1;
                textBoxIncrementUnit.Text = "1";
            }

            if (_overhead <= 0 || _overhead >= _incrementUnit || textBoxContextSwitchOverhead.Text.Trim() == string.Empty)
            {
                _overhead = 0;
                textBoxContextSwitchOverhead.Text = "0";
            }
        }

        private void buttonPlotSolutionSpace_Click(object sender, EventArgs e)
        {
            InputInvalidationCheck();

            if (comboBoxScheduler.SelectedItem.ToString() == "RM")
            {
                if (comboBoxApproach.SelectedItem.ToString() == "Exhaustive search")
                {
                    buttonSolutionSpaceRM_Click(null, null);
                }
                else if (comboBoxApproach.SelectedItem.ToString() == "Abstraction bound")
                {
                    buttonABunderRM_Click(null, null);
                }
            }
            else if (comboBoxScheduler.SelectedItem.ToString() == "EDF")
            {
                if (comboBoxApproach.SelectedItem.ToString() == "Exhaustive search")
                {
                    buttonSolutionSpaceEDF_Click(null, null);
                }
                else if (comboBoxApproach.SelectedItem.ToString() == "Abstraction bound")
                {
                    buttonABunderEDF_Click(null, null);
                }
            }

        }
        private void buttonCheck_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            List<Task> listTask = new List<Task>();

            Task taskEms = new Task(
                double.Parse(textBoxEmsPi.Text),
                double.Parse(textBoxEmsTheta.Text),
                double.Parse(textBoxEmsPi.Text),    // deadline = period
                0);
                  
            Task taskTms = new Task(
                double.Parse(textBoxTmsPi.Text),
                double.Parse(textBoxTmsTheta.Text),
                double.Parse(textBoxTmsPi.Text),    // deadline = period
                0);

            listTask.Add(taskEms);
            listTask.Add(taskTms);

            DataTable dtSync = dataGridViewSync.DataSource as DataTable;
            foreach (DataRow dr in dtSync.Rows)
            {
                Task t = new Task(
                    double.Parse(dr["Period"].ToString()), 
                    double.Parse(dr["ExecutionTime"].ToString()) + 2 * _overhead,
                    double.Parse(dr["ExecutionTime"].ToString()) + 2 * _overhead,   // deadline = execution
                    double.Parse(dr["Phase"].ToString()),
                    0);

                listTask.Add(t);                
            }

            double interval = listTask[0].Period;
            foreach (Task t in listTask)
            {
                interval = (double)GetLCM((long)interval, (long)t.Period);
            }
            
            FormChart chart = new FormChart("Resource demand / supply");
            chart.GetLable.Text = "Schedulable";

            try
            {
                Series seriesDBF = new Series("Resource Demand");
                seriesDBF.ChartArea = "ChartArea1";
                seriesDBF.ChartType = SeriesChartType.StepLine;
                seriesDBF.Legend = "Legend1";
                seriesDBF.Color = Color.Blue;

                Series seriesSBF = new Series("Resurce Supply");
                seriesSBF.ChartArea = "ChartArea1";
                seriesSBF.ChartType = SeriesChartType.Line;
                seriesSBF.Legend = "Legend1";
                seriesSBF.Color = Color.Red;

                DataTable dt = GetSelectedTaskSet();
                
                for (double t = 0; t <= interval; t += 1)
                {
                    double dbf = GetDBF_Sporadic_EDF(listTask, t);
                    if (dbf > t)
                        chart.GetLable.Text = "Not Schedulable";

                    seriesDBF.Points.Add(new DataPoint(t, dbf));
                    seriesSBF.Points.Add(new DataPoint(t, t));
                }

                chart.AxisX.Title = "Interval Length";
                chart.AxisY.Title = "Resource demand / supply";
                chart.AxisX.Minimum = 0;

                chart.AddSeries(seriesDBF);
                chart.AddSeries(seriesSBF);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            chart.Show();
        }

        private List<long> GetList(DataTable dt)
        {
            List<long> list = new List<long>();
            
            long lcm = GetLCM_forWorkload(dt);
                       
            foreach (DataRow dr in dt.Rows)
            {
                Task t = new Task(dr);
                for (long i = 0; i <= lcm / t.Period; i++)
                {
                    long current = i * (long)t.Period;
                    if (false == list.Contains(current))
                        list.Add(current);
                }
            }

            list.Sort();

            return list;
        }

    }
}

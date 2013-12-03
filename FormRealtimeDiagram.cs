using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RealtimeAnalysis
{
    public partial class FormRealtimeDiagram : Form
    {
        public FormRealtimeDiagram()
        {
            InitializeComponent();

            DataTable dtTaskSet = new DataTable();
            dtTaskSet.Columns.Add("TaskNumber");
            dtTaskSet.Columns.Add("Period");
            dtTaskSet.Columns.Add("ExecutionTime");
            dtTaskSet.Columns.Add("SoftDeadline");
            dtTaskSet.Columns.Add("Probability");
            dtTaskSet.Columns.Add("HardDeadline");
            dtTaskSet.Columns.Add("Priority");

            NewTask(dtTaskSet, 0, 5, 2, 2.9, 0.5, 5);
            NewTask(dtTaskSet, 1, 6, 3, 3, 0.8, 6);

            dataGridViewTasks.DataSource = dtTaskSet;
        }

        private void NewTask(DataTable dtTaskSet, int number, double Period, double ExecutionTime, double SoftDeadline, double Probability, double HardDeadline)
        {
            DataRow dr = dtTaskSet.NewRow();
            dr["TaskNumber"] = number;
            dr["Period"] = Period;
            dr["ExecutionTime"] = ExecutionTime;
            dr["SoftDeadline"] = SoftDeadline;
            dr["Probability"] = Probability;
            dr["HardDeadline"] = HardDeadline;

            dtTaskSet.Rows.Add(dr);
        }

        private long GetLCM_forWorkload(DataTable dt)
        {
            PeriodicTask firstTask = new PeriodicTask(dt.Rows[0]);
            long LCM = (long)firstTask.Period;

            foreach (DataRow dr in dt.Rows)
            {
                PeriodicTask t = new PeriodicTask(dr);
                LCM = GetLCM(LCM, (long)t.Period);
            }

            return LCM;
        }
        private double GetUtilizationOfWorkload(DataTable dt)
        {
            double SumUtilization = 0;
            foreach (DataRow dr in dt.Rows)
            {
                PeriodicTask t = new PeriodicTask(dr);
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


        private void buttonScheduleEDF_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dataGridViewTasks.DataSource;
            List<PeriodicTask> taskSet = new List<PeriodicTask>();
            foreach (DataRow dr in dt.Rows)
                taskSet.Add(new PeriodicTask(dr));

            long LCM = GetLCM_forWorkload(dt);

            List<double> listHardDeadline = new List<double>();
            foreach (PeriodicTask task in taskSet)
            {
                listHardDeadline.AddRange(task.GetHardDeadline(0, LCM));                
            }
            listHardDeadline.Sort();

            List<List<double>> listListSoftDeadline = new List<List<double>>();
            foreach (PeriodicTask task in taskSet)
            {
                listListSoftDeadline.Add(task.GetSoftDeadline(0, LCM));
            }

            bool bHard = true;
            foreach (double deadline in listHardDeadline)
            {
                if (false == TimeDemandAnalysis(dt, 0, deadline))
                {
                    Console.WriteLine("[Hard] Not schedulable at " + deadline);
                    bHard = false;
                }
            }

            if (bHard == true)
                Console.WriteLine("[Hard] Schedulable.");

            bool bSoft = true;
            for(int i = 0; i<listListSoftDeadline.Count; i++)
            {
                List<double> listSoftDeadline = listListSoftDeadline[i];
                foreach (double deadline in listSoftDeadline)
                {
                    if (false == TimeDemandAnalysis(dt, 0, deadline))
                    {
                        taskSet[i].MissCount++;
                        Console.WriteLine("[Task" + i + "] [Soft] Not schedulable at " + deadline);
                        bSoft = false;
                    }
                    else
                    {
                        taskSet[i].SuccessCount++;
                    }
                }
                int total = taskSet[i].MissCount + taskSet[i].SuccessCount;
                Console.WriteLine("[Task" + i + "] " + taskSet[i].SuccessCount + "/" + total);
            }

            if (bSoft == true)
                Console.WriteLine("[Soft] Schedulable.");
        }

        private bool TimeDemandAnalysis(DataTable dt, double startTime, double endTime)
        {
            List<PeriodicTask> taskSet = new List<PeriodicTask>();
            foreach (DataRow dr in dt.Rows)
                taskSet.Add(new PeriodicTask(dr));

            double sum = 0;
            foreach (PeriodicTask task in taskSet)
            {
                //sum += Math.Floor(endTime / task.Period) * task.ExecutionTime;
                sum += (Math.Floor( (endTime - task.SoftDeadline) / task.Period) + 1) * task.ExecutionTime;
            }

            Console.WriteLine("D[" + startTime + "," + endTime + "] = " + sum);

            return sum <= endTime;
        }

        private void buttonDrawDiagram_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dataGridViewTasks.DataSource;
            List<PeriodicTask> taskSet = new List<PeriodicTask>();
            foreach (DataRow dr in dt.Rows)
                taskSet.Add(new PeriodicTask(dr));

            long LCM = GetLCM_forWorkload(dt);

            FormTimeDiagram form = new FormTimeDiagram();
            form.SetTask(taskSet[0], 0, LCM);
            form.ShowDialog();
        }

        private void buttonSchduleEDF_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dataGridViewTasks.DataSource;
            List<PeriodicTask> taskSet = new List<PeriodicTask>();
            foreach (DataRow dr in dt.Rows)
                taskSet.Add(new PeriodicTask(dr));
            long LCM = GetLCM_forWorkload(dt);

            Scheduler edf = new Scheduler(taskSet, 0, LCM);
            edf.ScheduleEDF();

            FormTimeDiagram form = new FormTimeDiagram();
            form.SetTask(edf.ListListEventOutput, 0, LCM);
            form.ShowDialog();

        }
    }
}

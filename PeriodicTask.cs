using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeAnalysis
{
    public class PeriodicTask
    {
        int _taskNumber;

        public int TaskNumber
        {
            get { return _taskNumber; }
            set { _taskNumber = value; }
        }

        int _missCount;

        public int MissCount
        {
            get { return _missCount; }
            set { _missCount = value; }
        }
        int _successCount;

        public int SuccessCount
        {
            get { return _successCount; }
            set { _successCount = value; }
        }


        double _period;

        public double Period
        {
            get { return _period; }
            set { _period = value; }
        }
        double _executionTime;

        public double ExecutionTime
        {
            get { return _executionTime; }
            set { _executionTime = value; }
        }

        double _hardDeadline;

        public double HardDeadline
        {
            get { return _hardDeadline; }
            set { _hardDeadline = value; }
        }
        double _softDeadline;

        public double SoftDeadline
        {
            get { return _softDeadline; }
            set { _softDeadline = value; }
        }
        double _probability;

        public double Probability
        {
            get { return _probability; }
            set { _probability = value; }
        }

        int _priority;

        public int Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

        public double Utilization
        {
            get { return ExecutionTime / Period; }
        }

        public PeriodicTask(String name, double period, double executionTime, double softDeadline, double probability, double hardDeadline, int priority)
        {
            _period = period;
            _executionTime = executionTime;
            _softDeadline = softDeadline;
            _hardDeadline = hardDeadline;
            _probability = probability;
            _priority = priority;
        }

        public PeriodicTask(String name, double period, double executionTime, double softDeadline, double probability, double hardDeadline, double phase, int priority)
        {
            _period = period;
            _executionTime = executionTime;
            _softDeadline = softDeadline;
            _hardDeadline = hardDeadline;
            _probability = probability;
            _priority = priority;
        }

        public PeriodicTask(DataRow dr)
        {
            _taskNumber = int.Parse(dr["TaskNumber"].ToString());
            _period = double.Parse(dr["Period"].ToString());
            _executionTime = double.Parse(dr["ExecutionTime"].ToString());
            _softDeadline = double.Parse(dr["SoftDeadline"].ToString()); ;
            _hardDeadline = double.Parse(dr["HardDeadline"].ToString()); ;
            _probability = double.Parse(dr["Probability"].ToString()); ;
        }

        public List<JobEvent> GetEventList(double startTime, double endTime)
        {
            List<JobEvent> listEvent = new List<JobEvent>();

            double max = endTime / Period;
            for (int i = 0; i < max; i++)
            {
                double value = i * Period;
                if (value < startTime)
                    continue;

                JobEvent e = new JobEvent(this);
                e.AbsReleaseTime = value;
                e.AbsStartTime = value;
                e.AbsSoftDeadline = value + SoftDeadline;
                e.AbsHardDeadline = value + HardDeadline;
                e.AbsCompleteTime = value + ExecutionTime;
                e.RemainExecution = ExecutionTime;
                e.JobNumber = i;
                
                listEvent.Add(e);
            }

            return listEvent;
        }

        public List<double> GetReleaseTime(double startTime, double endTime)
        {
            List<double> list = new List<double>();

            double max = endTime / Period;
            for (int i = 0; i < max; i++)
            {
                double value = i * Period;
                if (value < startTime)
                    continue;

                list.Add(value);
            }

            return list;
        }

        public List<double> GetHardDeadline(double startTime, double endTime)
        {
            List<double> listHardDeadline = new List<double>();

            double max = endTime / Period;
            for (int i = 0; i < max; i++)
            {
                double value = i * Period + HardDeadline;
                if (value < startTime)
                    continue;

                listHardDeadline.Add(value);
            }

            return listHardDeadline;
        }

        public List<double> GetSoftDeadline(double startTime, double endTime)
        {
            List<double> listSoftDeadline = new List<double>();

            double max = endTime / Period;
            for (int i = 0; i < max; i++)
            {
                double value = i * Period + SoftDeadline;
                if (value < startTime)
                    continue;

                listSoftDeadline.Add(value);
            }

            return listSoftDeadline;
        }
    }
}

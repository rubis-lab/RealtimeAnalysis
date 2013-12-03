using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace RealtimeAnalysis
{
    class Task
    {
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

        double _deadline;

        public double Deadline
        {
            get { return _deadline; }
            set { _deadline = value; }
        }

        int _priority;

        public int Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

        double _phase;

        public double Phase
        {
            get { return _phase; }
            set { _phase = value; }
        }

        public double Utilization
        {
            get { return ExecutionTime / Period; }
        }

        public Task(double period, double executionTime, double deadline, int priority)
        {
            _period = period;
            _executionTime = executionTime;
            _deadline = deadline;
            _priority = priority;
            _phase = 0;
        }

        public Task(double period, double executionTime, double deadline, double phase, int priority)
        {
            _period = period;
            _executionTime = executionTime;
            _deadline = deadline;
            _priority = priority;
            _phase = phase;
        }

        public Task(DataRow dr)
        {
            try
            {
                _period = double.Parse(dr["Period"].ToString());
                _executionTime = double.Parse(dr["ExecutionTime"].ToString());
                _priority = int.Parse(dr["Priority"].ToString());
                _deadline = _period;
                _phase = 0;
            }
            catch (Exception e)
            {
            }
        }
    }
}

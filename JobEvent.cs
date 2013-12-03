using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeAnalysis
{
    public class JobEvent
    {
        PeriodicTask _parentTask;

        public PeriodicTask ParentTask
        {
            get { return _parentTask; }
            set { _parentTask = value; }
        }

        int _jobNumber;

        public int JobNumber
        {
            get { return _jobNumber; }
            set { _jobNumber = value; }
        }
        bool _isSoftDeadlineMissed;

        public bool IsSoftDeadlineMissed
        {
            get { return _isSoftDeadlineMissed; }
            set { _isSoftDeadlineMissed = value; }
        }
        bool _isHardDeadlineMissed;

        public bool IsHardDeadlineMissed
        {
            get { return _isHardDeadlineMissed; }
            set { _isHardDeadlineMissed = value; }
        }

        double _absReleaseTime;

        public double AbsReleaseTime
        {
            get { return _absReleaseTime; }
            set { _absReleaseTime = value; }
        }

        double _absStartTime;

        public double AbsStartTime
        {
            get { return _absStartTime; }
            set { _absStartTime = value; }
        }
        double _absSoftDeadline;

        public double AbsSoftDeadline
        {
            get { return _absSoftDeadline; }
            set { _absSoftDeadline = value; }
        }
        double _absHardDeadline;

        public double AbsHardDeadline
        {
            get { return _absHardDeadline; }
            set { _absHardDeadline = value; }
        }
        double _absCompleteTime;

        public double AbsCompleteTime
        {
            get { return _absCompleteTime; }
            set { _absCompleteTime = value; }
        }

        double _remainExecution;

        public double RemainExecution
        {
            get { return _remainExecution; }
            set { _remainExecution = value; }
        }

        public JobEvent(PeriodicTask parentTask)
        {
            _parentTask = parentTask;
        }
    }
}

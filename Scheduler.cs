using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeAnalysis
{
    class Scheduler
    {
        double _startTime;
        double _endTime;

        List<PeriodicTask> _listTaskSet;

        public List<PeriodicTask> ListTaskSet
        {
            get { return _listTaskSet; }
            set { _listTaskSet = value; }
        }

        List<List<PeriodicTask>> _listListPriodicTask = new List<List<PeriodicTask>>();
        List<List<JobEvent>> _listListEventOutput = new List<List<JobEvent>>();

        public List<List<JobEvent>> ListListEventOutput
        {
            get { return _listListEventOutput; }
            set { _listListEventOutput = value; }
        }

        public Scheduler(List<PeriodicTask> taskSet, double startTime, double endTime)
        {
            _listTaskSet = taskSet;
            _startTime = startTime;
            _endTime = endTime;

            for (int i=0; i<taskSet.Count; i++)
                _listListEventOutput.Add(new List<JobEvent>());
        }

        private int compareStartTime(JobEvent e1, JobEvent e2) 
        { 
            return e1.AbsStartTime.CompareTo(e2.AbsStartTime); 
        }

        private int compareHardDeadline(JobEvent x, JobEvent y)
        {
            return x.AbsHardDeadline.CompareTo(y.AbsHardDeadline);
        }

        private int compareSoftDeadline(JobEvent x, JobEvent y)
        {
            return x.AbsSoftDeadline.CompareTo(y.AbsSoftDeadline);
        }

        private List<JobEvent> GetAllJobRelease()
        {
            List<JobEvent> listReleaseEvent = new List<JobEvent>();
            foreach (PeriodicTask task in _listTaskSet)
            {
                listReleaseEvent.AddRange(task.GetEventList(_startTime, _endTime));
            }
            listReleaseEvent.Sort(compareStartTime);

            return listReleaseEvent;
        }

        private Queue<double> BuildTimeline()
        {
            List<double> listReleaseTime = new List<double>();
            foreach (PeriodicTask task in _listTaskSet)
            {
                List<double> listTime = task.GetReleaseTime(_startTime, _endTime);
                foreach (double t in listTime)
                {
                    if (false == listReleaseTime.Contains(t))
                        listReleaseTime.Add(t);
                }
            }
            listReleaseTime.Sort();

            Queue<double> queueTimeline = new Queue<double>();
            foreach (double t in listReleaseTime)
                queueTimeline.Enqueue(t);

            return queueTimeline;
        }
        
        public void ScheduleEDF()
        {
            // Timeline 만들고 Job release 목록 생성
            List<JobEvent> listReleaseEvent = GetAllJobRelease();
            Queue<double> queueTimeline = BuildTimeline();

            List<KeyValuePair<int, int>> listSoftDeadlineMissedJobs = new List<KeyValuePair<int, int>>();
            List<KeyValuePair<int, int>> listHardDeadlineMissedJobs = new List<KeyValuePair<int, int>>();

            // 시간 진행
            while(queueTimeline.Count != 0)
            {
                double presentTime = queueTimeline.Dequeue();

                // 주어진 time interval 안에서
                if (presentTime > _endTime)
                    break;

                // 해당 시각에 큐에 있는 이벤트들 목록
                List<JobEvent> nextEvents = GetSameTimeEvents(listReleaseEvent, presentTime);
                if (nextEvents.Count == 0)
                    continue;
                else if (nextEvents.Count >= 2)
                {
                    // Swap point
                    Console.WriteLine("Swap point: " + presentTime);
                }


                // 큐에 있는 것들 중 hardDeadline 이 가장 먼저인 것
                //nextEvents.Sort(compareHardDeadline);
                nextEvents.Sort(compareSoftDeadline);
                
                //if (presentTime == 0 || presentTime == 6)
                //    nextEvents.Reverse();



                Queue<JobEvent> queueJob = new Queue<JobEvent>();
                foreach (JobEvent job in nextEvents)
                    queueJob.Enqueue(job);

                double nextTime = _endTime;
                if (queueTimeline.Count != 0)
                    nextTime = queueTimeline.First();

                while(queueJob.Count != 0)
                {
                    // Dequeue the job
                    JobEvent job = queueJob.Dequeue();

                    job.AbsStartTime = presentTime;
                    job.AbsCompleteTime = presentTime + job.RemainExecution;

                    // Soft deadline miss 검사
                    if (job.AbsSoftDeadline < job.AbsCompleteTime)
                    {
                        KeyValuePair<int, int> pair = new KeyValuePair<int, int>(job.ParentTask.TaskNumber, job.JobNumber);
                        if (listSoftDeadlineMissedJobs.Contains(pair) == false)
                            listSoftDeadlineMissedJobs.Add(pair);

                        job.ParentTask.MissCount++;
                    }

                    // Hard deadline 을 넘어갔다면, 해당 Job의 실행을 포기
                    if (job.AbsHardDeadline < job.AbsCompleteTime)
                    {
                        KeyValuePair<int, int> pair = new KeyValuePair<int, int>(job.ParentTask.TaskNumber, job.JobNumber);
                        if (listHardDeadlineMissedJobs.Contains(pair) == false)
                            listHardDeadlineMissedJobs.Add(pair);
                        continue;
                    }

                    // 다음 Job의 릴리즈에도 완료하지 못하였다면
                    if (nextTime < job.AbsCompleteTime)
                    {
                        job.AbsCompleteTime = nextTime;

                        // 남은 수행시간을 가진 새로운 이벤트를 생성
                        // 이 이벤트를 어떻게 할 것인지는 다음 time 에게 맡긴다.                   
                        JobEvent newEvent = new JobEvent(job.ParentTask);
                        newEvent.JobNumber = job.JobNumber;
                        newEvent.AbsStartTime = nextTime;
                        newEvent.AbsReleaseTime = job.AbsReleaseTime;
                        newEvent.AbsSoftDeadline = job.AbsSoftDeadline;
                        newEvent.AbsHardDeadline = job.AbsHardDeadline;                        
                        newEvent.RemainExecution = job.RemainExecution - (nextTime - presentTime);
                        newEvent.AbsCompleteTime = nextTime + newEvent.RemainExecution;

                        if (newEvent.AbsStartTime < _endTime)
                        {
                            listReleaseEvent.Add(newEvent);

                            if (false == queueTimeline.Contains(nextTime))
                            {
                                queueTimeline.Enqueue(nextTime);
                                List<double> temp = queueTimeline.ToList();
                                temp.Sort();
                                queueTimeline.Clear();

                                foreach (double d in temp)
                                    queueTimeline.Enqueue(d);
                            }
                        }
                    }

                    _listListEventOutput[job.ParentTask.TaskNumber].Add(job);

                    presentTime = job.AbsCompleteTime;
                }
            }

            // Soft deadline miss 한 Job 처리
            foreach (KeyValuePair<int, int> pair in listSoftDeadlineMissedJobs)
            {
                foreach (JobEvent e in _listListEventOutput[pair.Key])
                {
                    if (e.JobNumber == pair.Value)
                        e.IsSoftDeadlineMissed = true;
                }
            }

            // Hard deadline miss 한 Job 처리
            foreach (KeyValuePair<int, int> pair in listHardDeadlineMissedJobs)
            {
                foreach (JobEvent e in _listListEventOutput[pair.Key])
                {
                    if (e.JobNumber == pair.Value)
                        e.IsHardDeadlineMissed = true;
                }
            }

        }


        public List<JobEvent> GetSameTimeEvents(List<JobEvent> list, double time)
        {  
            return list.FindAll(delegate(JobEvent e) { return e.AbsStartTime == time; });
        }

     
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Domains
{

    public interface ICreateGanttItems
    {
        public IList<GanttJob> CreateGanttJobs(IList<Job> jobs);
    }
    public class GanttItemsCreator : ICreateGanttItems
    {
        public IList<GanttJob> CreateGanttJobs(IList<Job> jobs)
        {
            IList<GanttJob> ganttJobs = new List<GanttJob>(jobs.Count);
            foreach(var job in jobs)
            {
                var name = job.Name;
                GanttLine expected = new GanttLine(job.TimeReport.Expected);
                GanttLine actual = CreateActualLine(job.TimeReport.Actual, job.Duration);
                ganttJobs.Add(new GanttJob(name, expected, actual));
            }
            return ganttJobs;
        }

        private GanttLine CreateActualLine(DaySpan actual, int jobDuration)
        {
            if(actual.End == DateTime.MinValue)
            {
                if(actual.Start == DateTime.MinValue)
                {
                    var newStart = DateTime.Now;
                    var newEnd = DateTime.Now.AddDays(jobDuration);
                    return new GanttLine(new DaySpan(newStart, newEnd));
                }
                else
                {
                    var newEnd = DateTime.Now;
                    return new GanttLine(new DaySpan(actual.Start, newEnd));
                }
            }
            else
            {
                return new GanttLine(actual);
            }
        }
    }

    public class GanttJob
    {
        private readonly string _name;
        private readonly GanttLine _expected;
        private readonly GanttLine _actual;

        public GanttJob(string name, GanttLine expected, GanttLine actual)
        {
            _name = name;
            _expected = expected;
            _actual = actual;
        }

        public string Name => _name;

        public GanttLine Expected => _expected;
        public GanttLine Actual => _actual;
    }

    public class GanttLine
    {
        private readonly DaySpan _daySpan;

        public GanttLine(DaySpan daySpan)
        {
            _daySpan = daySpan;
        }

        public int StartX => (_daySpan.Start - DateTime.Now).Days;
        public int EndX => (_daySpan.End - DateTime.Now).Days;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Domains
{
    public class JobConflictDetector : IDetectConflict
    {
        public IEnumerable<JobConflict> DetectConflicts(IEnumerable<Job> jobs)
        {
            ISet<Job> jobSet = new HashSet<Job>(jobs);
            IList<JobConflict> conflicts = new List<JobConflict>();
            foreach(Job jobA in jobSet)
            {
                jobSet.Remove(jobA);
                foreach(Job jobB in jobSet)
                {
                    JobConflict conflict = GetConflict(jobA, jobB);
                    if (conflict != null) conflicts.Add(conflict);
                }
            }
            return conflicts;
        }

        public JobConflict GetConflict(Job jobA, Job jobB)
        {
            IEnumerable<DateTime> conflictDates = null;
            DaySpan jobDaySpan = jobA.TimeReport.Expected;
            conflictDates = jobDaySpan.GetConflictDates(jobB.TimeReport.Expected);
            if(conflictDates.Count() > 0)
            {
                return new JobConflict(jobA, jobB, conflictDates);
            }
            else
            {
                return null;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Domains
{
    public abstract class SortDecorator : SortService
    {
        protected SortService _wrappee;

        public SortDecorator(SortService wrappee)
        {
            _wrappee = wrappee;
        }

        public override IEnumerable<Job> Sort(IEnumerable<Job> jobs, SortParams sortParams)
        {
            if(_wrappee != null)
            {
                return _wrappee.Sort(jobs, sortParams);
            }
            else
            {
                return jobs;
            }
        }
    }

    public class BaseSort : SortService
    {
        public override IEnumerable<Job> Sort(IEnumerable<Job> jobs, SortParams sortParams)
        {
            if (sortParams.Param == SortOption.StartDate)
            {
                jobs = sortParams.Ascending ? jobs.OrderBy(j => j.TimeReport.ExpectedStartDate)
                                         : jobs.OrderByDescending(j => j.TimeReport.ExpectedStartDate);
            }
            return jobs;
        }
    }

    public class PlanningSort : SortDecorator
    {
        public PlanningSort(SortService wrappee) : base(wrappee)
        {

        }

        public override IEnumerable<Job> Sort(IEnumerable<Job> jobs, SortParams sortParams)
        {
            if (sortParams.Param == SortOption.Planning)
            {
                jobs = sortParams.Ascending ? jobs.OrderBy(j => j.Planning)
                                         : jobs.OrderByDescending(j => j.Planning);
            }
            return base.Sort(jobs, sortParams);
        }
    }

    public class StatusSort : SortDecorator
    {
        public StatusSort(SortService wrappee) : base(wrappee)
        {

        }

        public override IEnumerable<Job> Sort(IEnumerable<Job> jobs, SortParams sortParams)
        {
            if (sortParams.Param == SortOption.Status)
            {
                jobs = sortParams.Ascending ? jobs.OrderBy(j => j.GetStatus())
                                         : jobs.OrderByDescending(j => j.GetStatus());
            }
            return base.Sort(jobs, sortParams);
        }
    }
}

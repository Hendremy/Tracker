using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Domains
{
    public abstract class FilterDecorator : FilterService
    {
        protected FilterService _wrappee;

        public FilterDecorator(FilterService wrappee)
        {
            _wrappee = wrappee;
        }

        public override IEnumerable<Job> Filter(IEnumerable<Job> jobs, FilterParams filterParams)
        {
            if (_wrappee != null)
            {
                return _wrappee.Filter(jobs, filterParams);
            }
            else
            {
                return jobs;
            }
        }
    }

    public class BaseFilter : FilterService
    {
        public override IEnumerable<Job> Filter(IEnumerable<Job> jobs, FilterParams filterParams)
        {
            if (filterParams.Param == FilterOption.Date && !string.IsNullOrWhiteSpace(filterParams.Value))
            {
                DateTime date;
                if (DateTime.TryParse(filterParams.Value, out date))
                {
                    jobs = jobs.Where(j => j.HasDate(date));
                }   
            }
            return jobs;
        }
    }

    public class PlanningFilter : FilterDecorator
    {
        public PlanningFilter(FilterService wrappee) : base(wrappee)
        {

        }

        public override IEnumerable<Job> Filter(IEnumerable<Job> jobs, FilterParams filterParams)
        {
            if (filterParams.Param == FilterOption.Planning && !string.IsNullOrWhiteSpace(filterParams.Value))
            {
                jobs = jobs.Where(j => j.Planning.Contains(filterParams.Value));
            }
            return base.Filter(jobs, filterParams);
        }
    }

    public class StatusFilter : FilterDecorator
    {
        public StatusFilter(FilterService wrappee) : base(wrappee)
        {

        }

        public override IEnumerable<Job> Filter(IEnumerable<Job> jobs, FilterParams filterParams)
        {
            if (filterParams.Param == FilterOption.Status && !string.IsNullOrWhiteSpace(filterParams.Value))
            {
                jobs = jobs.Where(j => j.GetStatusString().Contains(filterParams.Value));
            }
            return base.Filter(jobs, filterParams);
        }
    }


}

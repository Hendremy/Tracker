using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Domains
{
    public class FilterHandler : IFilterHandler
    {
        private readonly FilterService _filterService;
        private FilterParams _filterParams;

        public FilterHandler(FilterService filterService, FilterParams filterParams)
        {
            _filterService = filterService;
            _filterParams = filterParams;
        }

        public FilterParams Params
        {
            set => _filterParams = value;
        }

        public IEnumerable<Job> handle(IEnumerable<Job> jobs)
        {
            return _filterService.Filter(jobs, _filterParams);
        }
    }

    public abstract class FilterService
    {
        public abstract IEnumerable<Job> Filter(IEnumerable<Job> jobs, FilterParams sortParams);
    }
}

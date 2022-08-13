using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Domains
{
    public class SortHandler : ISortHandler
    {
        private readonly SortService _sortService;
        private SortParams _sortParams;

        public SortHandler(SortService decorator)
        {
            _sortService = decorator;
            Params = new SortParams();
        }

        public SortParams Params
        {
            set => _sortParams = value;
        }

        public IEnumerable<Job> handle(IEnumerable<Job> jobs)
        {
            return _sortService.Sort(jobs, _sortParams);
        }
    }

    public abstract class SortService
    {
        public abstract IEnumerable<Job> Sort(IEnumerable<Job> jobs, SortParams sortParams);
    }

    
}

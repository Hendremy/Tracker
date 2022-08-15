using Hendricé.Rémy.Poo.Tracker.Datas;
using Hendricé.Rémy.Poo.Tracker.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Presentations
{
    public class TrackerServiceProvider : ITrackerServices
    {
        private readonly ITrackerRepository _repository;
        public TrackerServiceProvider(string jsonDir, string usersFile, string planningsDir)
        {
            _repository = new JSONTrackerRepository(GetPlanningParser(), GetUserParser(), jsonDir, usersFile, planningsDir);
        }

        private IParsePlanning GetPlanningParser()
        {
            return new JSONPlanningParser();
        }

        private IParseUser GetUserParser()
        {
            return new JSONUserParser();
        }

        public IAuthenticate GetAuthenticator()
        {
            return new Authenticator();
        }

        public ISortHandler GetSortHandler()
        {
            var planningsort = new PlanningSort(new StatusSort(new BaseSort()));
            return new SortHandler(planningsort, new SortParams());
        }

        public IFilterHandler GetFilterHandler()
        {
            var planning = new PlanningFilter(new StatusFilter(new BaseFilter()));
            return new FilterHandler(planning, new FilterParams());
        }

        public IDetectConflict GetConflictDetector()
        {
            return new ConflictDetector();
        }

        public ITrackerRepository GetTrackerRepository()
        {
            return _repository;
        }
    }

    public interface ITrackerServices
    {
        public IAuthenticate GetAuthenticator();
        public ISortHandler GetSortHandler();
        public IFilterHandler GetFilterHandler();
        public IDetectConflict GetConflictDetector();
        public ITrackerRepository GetTrackerRepository();
    }
}

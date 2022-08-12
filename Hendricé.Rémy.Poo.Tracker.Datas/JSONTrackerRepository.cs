using Hendricé.Rémy.Poo.Tracker.Domains;
using System;
using System.Collections.Generic;

namespace Hendricé.Rémy.Poo.Tracker.Datas
{
    public class JSONTrackerRepository : ITrackerRepository
    {
        public IEnumerable<Job> GetUserJobs(string code)
        {
            throw new TrackerRepositoryException("Can't find users");
        }

        public IEnumerable<User> GetUsers()
        {
            throw new TrackerRepositoryException("Can't find users");
        }
    }
}

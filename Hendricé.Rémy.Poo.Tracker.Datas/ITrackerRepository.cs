using Hendricé.Rémy.Poo.Tracker.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Datas
{
    public interface ITrackerRepository : IDisposable
    {
        IEnumerable<UserCredentials> GetUsersCredentials();
        IEnumerable<Job> GetUserJobs(string code, out string errormessage);
    }
}

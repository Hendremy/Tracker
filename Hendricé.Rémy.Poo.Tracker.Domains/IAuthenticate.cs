using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Domains
{
    public interface IAuthenticate
    {
        public string TryAuthentify(IEnumerable<UserCredentials> users, string code, string password);
    }
}

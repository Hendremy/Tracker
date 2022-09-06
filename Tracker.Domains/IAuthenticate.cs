using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Domains
{
    public interface IAuthenticate
    {
        public bool CredentialsAreValid(IEnumerable<UserCredentials> users, string code, string password);
    }
}

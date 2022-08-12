using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Domains
{
    public class Authenticator : IAuthenticate
    {

        public string TryAuthentify(IEnumerable<UserCredentials> users, string code, string password)
        {
            UserCredentials user = users.FirstOrDefault(u => u.Code.Equals(code) && u.Password.Equals(password));
            return user != null ? code : null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Domains
{
    public class Authenticator : IAuthenticate
    {

        public string TryAuthentify(IEnumerable<User> users, string code, string password)
        {
            User user = users.FirstOrDefault(u => u.Code == code && u.Password == password);
            return user != null ? code : null;
        }
    }
}

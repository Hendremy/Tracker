using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Domains
{
    public record UserCredentials
    {
        public string Code { get; init; }
        public string Password { get; init; }

        public UserCredentials(string code, string password)
        {
            Code = code;
            Password = password;
        }
    }
}

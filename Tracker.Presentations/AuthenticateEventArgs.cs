using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Presentations
{
    public record AuthenticateEventArgs
    {

        public AuthenticateEventArgs(string code, string password)
        {
            Code = code;
            Password = password;
        }

        public string Code { get; init; }
        public string Password { get; init; }
    }
}

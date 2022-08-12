using System;

namespace Hendricé.Rémy.Poo.Tracker.Domains
{
    public class User
    {
        private readonly string _code;
        private readonly string _firstName;
        private readonly string _lastName;
        private readonly string _password;

        public string Code => _code;
        public string FullName => _firstName + _lastName;
        public string Password => _password;
    }
}

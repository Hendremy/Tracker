using System;

namespace Hendricé.Rémy.Poo.Tracker.Domains
{
    public class User
    {
        private readonly string _code;
        private readonly string _firstName;
        private readonly string _lastName;

        public User(string code, string firstName, string lastName)
        {
            _code = code;
            _firstName = firstName;
            _lastName = lastName;
        }

        public string Code => _code;
        public string FullName => _firstName + _lastName;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Domains
{
    public class Planning
    {
        private readonly string _name;

        public Planning(string name)
        {
            _name = name;
        }
        
        public string Name => _name;
    }
}

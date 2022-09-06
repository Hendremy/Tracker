using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Datas
{
    public class TrackerRepositoryException : Exception
    {
        public TrackerRepositoryException(string message) : base(message)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Domains
{
    public record DaySpan
    {
        private DateTime _start;
        private DateTime _end;

        public DaySpan(DateTime start, DateTime end)
        {
            Start = start;
        }

        public DateTime Start
        {
            get => _start;
            set => _start = value;
        }

        public DateTime End
        {
            get => _end;
            set => _end = value;
        }
    }
}

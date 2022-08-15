using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Domains
{
    public struct DaySpan
    {
        private DateTime _start;
        private DateTime _end;

        public DaySpan(DateTime start, DateTime end)
        {
            _start = start;
            _end = end;
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

        public DateTime EndDate => End.Date;
        public DateTime StartDate => Start.Date;

        public bool CollidesWith(DaySpan span)
        {
            return this.Start <= span.End && span.Start <= this.End;
        }

        public IEnumerable<DateTime> GetConflictDates(DaySpan span)
        {
            ISet<DateTime> conflictDates = new HashSet<DateTime>();
            if (CollidesWith(span))
            {
                DateTime maxStart = Max(this.Start, span.Start);
                DateTime minEnd = Min(this.End, span.End);
                while(maxStart <= minEnd)
                {
                    conflictDates.Add(new DateTime(maxStart.Ticks));
                    maxStart = maxStart.AddDays(1);
                }
            }
            return conflictDates;
        } 

        private DateTime Max(DateTime a, DateTime b)
        {
            return a > b ? a : b;
        }

        private DateTime Min(DateTime a, DateTime b)
        {
            return a < b ? a : b;
        }
    }
}

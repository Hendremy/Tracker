using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Domains
{
    public class TimeReport
    {
        private readonly DaySpan _expected;
        private DaySpan _actual;

        public TimeReport(DaySpan expected, DaySpan actual)
        {
            _expected = expected;
            _actual = actual;
        }

        public DaySpan Expected => _expected;

        public DateTime ExpectedStartDate => Expected.StartDate;

        public DateTime ExpectedEndDate => Expected.EndDate;

        public DaySpan Actual => _actual;

        public DateTime ActualStartDate => Actual.StartDate;

        public DateTime ActualEndDate => Actual.EndDate;


        public DateTime ActualStart
        {
            get => _actual.Start;
            set => _actual.Start = value;
        }

        public DateTime ActualEnd
        {
            get => _actual.End;
            set => _actual.End = value;
        }

        public int GetDelay()
        {
            int delay;
            if (GetStatus() == JobStatus.Done)
            {
                delay = (ActualStart - Expected.Start).Days;
            }
            else
            {
                delay = (Expected.Start - DateTime.Now).Days;
            }
            return Math.Max(0, delay);
        }

        public JobStatus GetStatus()
        {
            JobStatus reportStatus;
            if(Actual.Start != DateTime.MinValue)
            {
                reportStatus = ActualEnd != DateTime.MinValue ? JobStatus.Done : JobStatus.Doing;
            }
            else
            {
                reportStatus = JobStatus.Todo;
            }
            return reportStatus;
        }

        public bool HasDate(DateTime date)
        {
            return ExpectedStartDate == date || ExpectedEndDate == date
                || ActualStartDate == date || ActualEndDate == date;
        }

        public bool Start()
        {
            if(GetStatus() == JobStatus.Todo)
            {
                _actual.Start = DateTime.Now;
                return true;
            }
            return false;
        }

        public bool End()
        {
            if(GetStatus() == JobStatus.Doing)
            {
                _actual.End = DateTime.Now;
                return true;
            }
            return false;
        }


        public int Duration => Expected.Duration;
    }
}

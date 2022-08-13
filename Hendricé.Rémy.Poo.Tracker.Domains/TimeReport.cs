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

        public DateTime ExpectedStartDate => Expected.Start.Date;

        public DateTime ExpectedEndDate => Expected.End.Date;

        public DaySpan Actual => _actual;

        public DateTime ActualStartDate => Actual.Start.Date;

        public DateTime ActualEndDate => Actual.End.Date;


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

        public double GetDelay()
        {
            double delay;
            if (GetStatus() == JobStatus.Done)
            {
                delay = (ActualStart - Expected.Start).TotalDays;
            }
            else
            {
                delay = (Expected.Start - DateTime.Now).TotalDays;
            }
            return Math.Max(0, delay);
        }

        public JobStatus GetStatus()
        {
            JobStatus reportStatus;
            if(Actual != null)
            {
                if(ActualEnd != DateTime.MinValue)
                {
                    reportStatus = JobStatus.Done;
                }
                reportStatus = JobStatus.Doing;
            }
            else
            {
                reportStatus = JobStatus.Todo;
            }
            return reportStatus;
        }

        public bool HasDate(DateTime date)
        {
            return ExpectedEndDate == date || ExpectedStartDate == date;
                //|| ActualStartDate == date || ActualEndDate == date;
        }
    }

    
}

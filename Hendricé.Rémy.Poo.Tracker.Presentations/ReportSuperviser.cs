using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Presentations
{
    public class ReportSuperviser
    {

        private readonly IReportView _view;

        public ReportSuperviser(IReportView view)
        {
            _view = view;
        }

        public void OnUserAuthentified(object sender, string code)
        {

        }
    }
}

using Hendricé.Rémy.Poo.Tracker.Datas;
using Hendricé.Rémy.Poo.Tracker.Domains;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Hendricé.Rémy.Poo.Tracker.Presentations
{
    public class MainSuperviser
    {
        private readonly ITabProvider _superviserProvider;
        private readonly IMainView _view;

        public MainSuperviser(IMainView view, ITabProvider superviserProvider)
        {
            _view = view;
            _superviserProvider = superviserProvider;
        }

        public void OnUserAuthentified(object sender, string code)
        {

        }

    }
}

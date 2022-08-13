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
        private readonly IMainView _view;

        public event EventHandler<string> UserAuthentified;

        public MainSuperviser(IMainView view)
        {
            _view = view;
        }

        public void OnUserAuthentified(object sender, string code)
        {
            UserAuthentified?.Invoke(sender, code);
        }

    }
}

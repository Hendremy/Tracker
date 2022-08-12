using Hendricé.Rémy.Poo.Tracker.Presentations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Cli
{
    public class MainView : CliView, IMainView
    {

        public MainView()
        {
            Show();
        }

        public void Show()
        {
            Console.WriteLine("IN-B2-UE11-C# : Tracker" + "\\n=======================");
        }

        public void OnUserAuthentified()
        {
            throw new NotImplementedException();
        }

    }
}

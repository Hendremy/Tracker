using Hendricé.Rémy.Poo.Tracker.Cli.Controllers;
using Hendricé.Rémy.Poo.Tracker.Presentations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Cli
{
    public class MainView : CliView
    {
        private readonly ITrack _controller;

        public MainView(ITrack controller)
        {
            _controller = controller;
        }

        public void show()
        {
            Console.WriteLine("IN-B2-UE11-C# : Tracker" + "\\n=======================");
        }

        public void showAuthentify()
        {
            throw new NotImplementedException();
        }

        public void start()
        {

        }

    }
}

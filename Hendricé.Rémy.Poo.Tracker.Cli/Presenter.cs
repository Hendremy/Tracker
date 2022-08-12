using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Cli
{
    public class Presenter
    {
        private static Presenter singleton;
        
        public static Presenter GetInstance()
        {
            if(singleton == null)
            {
                singleton = new Presenter();
            }
            return singleton;
        }

        private Presenter() { }
    }
}

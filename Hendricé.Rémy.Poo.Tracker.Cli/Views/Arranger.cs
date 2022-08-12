using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Cli
{
    public class Arranger
    {
        private static Arranger singleton;
        
        public static Arranger GetInstance()
        {
            if(singleton == null)
            {
                singleton = new Arranger();
            }
            return singleton;
        }

        private Arranger() { }
    }
}

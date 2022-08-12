using Hendricé.Rémy.Poo.Tracker.Domains;
using Hendricé.Rémy.Poo.Tracker.Presentations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Cli
{
    class ChooseSortView : CliView
    {
        public SortEventArgs AskChoice()
        {
            SortOption param = GetParam();
            bool ascending = GetAscending();
            return new SortEventArgs(param, ascending);
        }

        private SortOption GetParam()
        {
            int choice = -1;
            do
            {
                choice = AskInt("\n0. Date de début"
                    + "\n1. Chantier"
                    + "\n2. Statut\n"
                    + "\nChoix : ");
            }
            while (choice < 0 || Enum.GetValues<SortOption>().Length < choice);
            return (SortOption) choice;
        }

        private bool GetAscending()
        {
            int choice = -1;
            do
            {
                choice = AskInt("\n0. Croissant"
                    + "\n1. Décroissant"
                    + "\nChoix : ");
            }
            while (choice < 0 || 2 < choice);
            return choice == 0;
        }
    }
}

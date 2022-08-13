using Hendricé.Rémy.Poo.Tracker.Domains;
using Hendricé.Rémy.Poo.Tracker.Presentations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Cli
{
    public class ChooseFilterView : CliView
    {
        public FilterParams AskChoice()
        {
            FilterOption param = GetParam();
            string value = AskString("Valeur du filtre :");
            return new FilterParams(param, value);
        }

        private FilterOption GetParam()
        {
            int choice = -1;
            do
            {
                choice = AskInt("\n0. Aucun"
                    + "\n1. Date"
                    + "\n2. Chantier\n"
                    + "\n3. Statut\n"
                    + "\nChoix : ");
            }
            while (choice < 0 || Enum.GetValues<FilterOption>().Length < choice);
            return (FilterOption)choice;
        }
    }
}

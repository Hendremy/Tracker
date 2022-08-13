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
        public SortParams AskChoice()
        {
            SortOption param = GetParam();
            bool ascending = GetAscending();
            return new SortParams(param, ascending);
        }

        private SortOption GetParam()
        {
            int choice = -1;
            string question = BuildSortOptions();
            do
            {
                choice = AskInt(question + "\nChoix : ");
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
            while (choice < 0 || 1 < choice);
            return choice == 0;
        }

        private string BuildSortOptions()
        {
            var sb = new StringBuilder();
            foreach (SortOption opt in Enum.GetValues<SortOption>())
            {
                sb.Append($"\n{(int)opt}. {_presenter.SortOptionToString(opt)}");
            }
            return sb.ToString();
        }
    }
}

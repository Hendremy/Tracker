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
            string question = BuildFilterOptions();
            do
            {
                choice = AskInt( question + "\nChoix : ");
            }
            while (choice < 0 || Enum.GetValues<FilterOption>().Length < choice);
            return (FilterOption)choice;
        }

        private string BuildFilterOptions()
        {
            var sb = new StringBuilder();
            foreach(FilterOption opt in Enum.GetValues<FilterOption>())
            {
                sb.Append($"\n{(int)opt}. {_presenter.FilterOptionToString(opt)}");
            }
            return sb.ToString();
        }
    }
}

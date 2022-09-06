using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Hendricé.Rémy.Poo.Tracker.Domains;
using System;
using System.Collections.Generic;

namespace Hendricé.Rémy.Poo.Tracker.Gui
{
    public partial class JobConflictControl : UserControl
    {
        private TextBlock _planningA;
        private TextBlock _nameA;
        private TextBlock _planningB;
        private TextBlock _nameB;
        private ItemsControl _dates;

        public JobConflictControl()
        {
            InitializeComponent();
            LocateControls();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void LocateControls()
        {
            _planningA = this.FindControl<TextBlock>("PlanningA");
            _nameA = this.FindControl<TextBlock>("NameA");
            _planningB = this.FindControl<TextBlock>("PlanningB");
            _nameB = this.FindControl<TextBlock>("NameB");
            _dates = this.FindControl<ItemsControl>("Dates");
        }

        public void InitConflict(JobConflict conflict) 
        {
            _planningA.Text = conflict.JobAPlanning;
            _nameA.Text = conflict.JobAName;
            _planningB.Text = conflict.JobBPlanning;
            _nameB.Text = conflict.JobBName;
            var dateTexts = new HashSet<TextBlock>();
            foreach(DateTime date in conflict.ConflictDates)
            {
                dateTexts.Add(new TextBlock() { Text = date.ToShortDateString()});
            }
            _dates.Items = dateTexts;
        }

    }
}

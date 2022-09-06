using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Hendricé.Rémy.Poo.Tracker.Domains;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hendricé.Rémy.Poo.Tracker.Gui
{
    public partial class ConflictsControl : UserControl
    {
        private readonly ObservableCollection<JobConflictControl> _conflicts;
        private Expander _expander;

        public ConflictsControl()
        {
            InitializeComponent();
            LocateControls();
            _conflicts = new ObservableCollection<JobConflictControl>();
            DataContext = _conflicts;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void LocateControls()
        {
            _expander = this.FindControl<Expander>("Conflicts");
            _expander.Header = GetHeader();
        }

        public void SetConflicts(IEnumerable<JobConflict> conflicts)
        {
            InitConflicts(conflicts);
        }

        private void InitConflicts(IEnumerable<JobConflict> conflicts)
        {
            foreach(var conflict in conflicts)
            {
                var jobConflict = new JobConflictControl();
                jobConflict.InitConflict(conflict);
                _conflicts.Add(jobConflict);
            }
        }

        private TextBlock GetHeader()
        {
            var header = new TextBlock();
            header.Text = "< ! > Des conflits entre vos tâches ont été détectés < ! >";
            header.Foreground = Brushes.White;
            header.TextAlignment = TextAlignment.Center;

            return header;
        }

        
    }
}

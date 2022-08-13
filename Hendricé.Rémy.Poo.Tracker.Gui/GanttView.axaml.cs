using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Hendricé.Rémy.Poo.Tracker.Presentations;

namespace Hendricé.Rémy.Poo.Tracker.Gui
{
    public partial class GanttView : UserControl, IGanttView
    {
        public GanttView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

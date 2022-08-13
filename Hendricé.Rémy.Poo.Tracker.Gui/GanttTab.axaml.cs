using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Hendricé.Rémy.Poo.Tracker.Gui
{
    public partial class GanttTab : UserControl
    {
        public GanttTab()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

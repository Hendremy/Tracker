using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Hendricé.Rémy.Poo.Tracker.Gui
{
    public partial class RapportTab : UserControl
    {
        public RapportTab()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

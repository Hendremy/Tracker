using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Hendricé.Rémy.Poo.Tracker.Presentations;

namespace Hendricé.Rémy.Poo.Tracker.Gui
{
    public partial class JobControl : UserControl, IJobView
    {
        public JobControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void StartJob_Click(object sender, RoutedEventArgs args)
        {

        }

        private void FinishJob_Click(object sender, RoutedEventArgs args)
        {

        }
    }
}

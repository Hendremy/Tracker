using Avalonia.Controls;
using Hendricé.Rémy.Poo.Tracker.Presentations;
using System;

namespace Hendricé.Rémy.Poo.Tracker.Gui
{
    public partial class MainWindow : Window, IMainView
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public event EventHandler<string> SortRequested;
        public event EventHandler<string> FilterRequested;

        public void ShowUserJobs()
        {
            throw new NotImplementedException();
        }
    }
}

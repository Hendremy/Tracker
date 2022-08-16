using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Hendricé.Rémy.Poo.Tracker.Domains;
using Hendricé.Rémy.Poo.Tracker.Presentations;
using ScottPlot;
using ScottPlot.Avalonia;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Hendricé.Rémy.Poo.Tracker.Gui
{
    public partial class GanttView : UserControl, IGanttView
    {
        private AvaPlot _ganttChart;
        private Plot Plot => _ganttChart.Plot;

        public GanttView()
        {
            InitializeComponent();
            LocateControls();
            InitializeChart();
        }

        private void LocateControls()
        {
            _ganttChart = this.FindControl<AvaPlot>("GanttChart");
        }

        private void InitializeChart()
        {
            Plot.YAxis.Ticks(false);
            Plot.Title("Tâches");
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void UpdateItems(IList<GanttJob> jobs)
        {
            int yValue = 0;
            string[] labels = new string[jobs.Count];
            foreach(GanttJob job in jobs)
            {
                _ganttChart.Plot.AddLine(job.Actual.StartX, yValue, job.Actual.EndX, yValue);
                labels[yValue] = job.Name;
                yValue++;
            }
            this.Plot.YTicks(labels);
            this.Plot.Legend();
            _ganttChart.Refresh();
        }

        private void OnJobStatusChanged(object sender, PropertyChangedEventArgs args)
        {

        }
    }
}

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
        private IDictionary<Job, GanttJob> _ganttJobs;

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
            Plot.YAxis.Line(false);
            Plot.Title("Tâches");
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void UpdateItems(IEnumerable<Job> jobs)
        {
            _ganttJobs = new Dictionary<Job, GanttJob>();
            foreach(var job in jobs)
            {

            }
        }

        private void OnJobStatusChanged(object sender, PropertyChangedEventArgs args)
        {

        }
    }

    public class GanttJob
    {
        private readonly string _name;
        private readonly GanttLine _expected;
        private readonly GanttLine _actual;

        public GanttJob(string name, GanttLine expected, GanttLine actual)
        {
            _name = name;
            _expected = expected;
            _actual = actual;
        }
    }

    public class GanttLine
    {
        private readonly DaySpan _daySpan;

        public GanttLine(DaySpan daySpan)
        {
            _daySpan = daySpan;
        }

        public DaySpan DaySpan => _daySpan;
    }
}

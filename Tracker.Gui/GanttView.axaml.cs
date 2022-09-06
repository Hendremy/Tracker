using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Hendricé.Rémy.Poo.Tracker.Domains;
using Hendricé.Rémy.Poo.Tracker.Presentations;
using ScottPlot;
using ScottPlot.Avalonia;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;

namespace Hendricé.Rémy.Poo.Tracker.Gui
{
    public partial class GanttView : UserControl, IGanttView
    {
        private AvaPlot _ganttChart;
        private Plot Plot => _ganttChart.Plot;
        private readonly Color _expectedColor = Color.Cyan;
        private readonly Color _actualColor = Color.Brown;

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
            Plot.Title("Tâches");
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void UpdateItems(IList<GanttJob> jobs)
        {
            if (jobs.Count == 0) return;
            _ganttChart.Reset();
            int jobCount = jobs.Count;
            string[] labels = new string[jobs.Count];
            SetLimits(jobs);

            double[] expStartX = new double[jobCount];
            double[] expEndX = new double[jobCount];
            double[] actStartX = new double[jobCount];
            double[] actEndX = new double[jobCount];
            double[] expYs = new double[jobCount];
            double[] actYs = new double[jobCount];

            int yValue = 0;

            foreach (GanttJob job in jobs)
            {
                expStartX[yValue] = job.Expected.StartX;
                expEndX[yValue] = job.Expected.EndX;
                actStartX[yValue] = job.Actual.StartX;
                actEndX[yValue] = job.Actual.EndX;
                expYs[yValue] = yValue;
                actYs[yValue] = yValue + 0.5;
                labels[yValue] = job.Name;

                yValue++;
            }

            DrawScatter(expStartX, expYs, "Expected Start", _expectedColor);
            DrawScatter(expEndX, expYs, "Expected End", _expectedColor);
            DrawScatter(actStartX, actYs, "Actual Start", _actualColor);
            DrawScatter(actEndX, actYs, "Actual End", _actualColor);
            DrawLines(expStartX, expEndX, expYs, _expectedColor);
            DrawLines(actStartX, actEndX, actYs, _actualColor);

            this.Plot.YTicks(labels);
            this.Plot.Legend();
            _ganttChart.Refresh();
        }


        private void DrawScatter(double[] xCoords, double[] yCoords, string label, Color color)
        {
            Plot.AddScatter(xCoords, yCoords, color, 0, 10, MarkerShape.filledCircle, LineStyle.None, label);
        }

        private void DrawLines(double[] startX, double[]endX, double[] Ys, Color color)
        {
            for(int i = 0; i < startX.Length; ++i)
            {
                Plot.AddLine(startX[i], Ys[i], endX[i], Ys[i], color, 1);
            }
        }

        private void SetLimits(IList<GanttJob> jobs)
        {
            var yMin = 0;
            var yMax = jobs.Count + 1;
            var xMin = FindXMin(jobs);
            var xMax = FindXMax(jobs);
            _ganttChart.Plot.SetAxisLimitsX(xMin, xMax);
            _ganttChart.Plot.SetAxisLimitsY(yMin, yMax);
        }

        private double FindXMin(IList<GanttJob> jobs)
        {
            double xMinValue= 0;
            foreach(var job in jobs)
            {
                var start = job.Expected.StartX;
                xMinValue = Math.Min(xMinValue, start);
            }
            return xMinValue;
        }

        private double FindXMax(IList<GanttJob> jobs)
        {
            double xMaxValue = 0;
            foreach (var job in jobs)
            {
                var start = job.Actual.EndX;
                xMaxValue = Math.Max(xMaxValue, start);
            }
            return xMaxValue;
        }
    }
}

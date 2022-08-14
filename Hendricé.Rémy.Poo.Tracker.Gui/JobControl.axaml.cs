using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Hendricé.Rémy.Poo.Tracker.Domains;
using Hendricé.Rémy.Poo.Tracker.Presentations;
using System;
using System.ComponentModel;

namespace Hendricé.Rémy.Poo.Tracker.Gui
{
    public partial class JobControl : UserControl, IJobView
    {
        private TextBlock _planning;
        private TextBlock _name;
        private TextBlock _expectedDates;
        private TextBlock _status;
        private Button _startBtn;
        private Button _endBtn;
        private TextBlock _delay;

        private Job _job;

        public event EventHandler StartJobRequested;
        public event EventHandler EndJobRequested;


        public JobControl()
        {
            InitializeComponent();
            LocateControls();
        }

        private void LocateControls()
        {
            _planning = this.FindControl<TextBlock>("Planning");
            _name = this.FindControl<TextBlock>("Name");
            _expectedDates = this.FindControl<TextBlock>("ExpectedDates");
            _status = this.FindControl<TextBlock>("Status");
            _startBtn = this.FindControl<Button>("StartBtn");
            _endBtn = this.FindControl<Button>("EndBtn");
            _delay = this.FindControl<TextBlock>("Delay");
        }
        
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void StartJob_Click(object sender, RoutedEventArgs args)
        {
            StartJobRequested?.Invoke(sender, EventArgs.Empty);
        }

        private void EndJob_Click(object sender, RoutedEventArgs args)
        {
            EndJobRequested?.Invoke(sender, EventArgs.Empty);
        }

        public void OnDatePropertyChanged(object? sender, PropertyChangedEventArgs args)
        {
            string propertyChanged = args.PropertyName != null ? args.PropertyName : "";
            UpdateStatus();
        }

        private void UpdateStartDate()
        {
            DateTime startDate = _job.TimeReport.ActualStartDate;
            _startBtn.Content = $"Commencée le {startDate.ToShortDateString()}";
            _startBtn.IsEnabled = false;
            _endBtn.IsEnabled = true;
        }

        private void UpdateEndDate()
        {
            DateTime endDate = _job.TimeReport.ActualEndDate;
            _endBtn.Content = $"Terminée le {endDate.ToShortDateString()}";
            _endBtn.IsEnabled = false;
        }

        public void InitView(Job job)
        {
            _job = job;
            SubscribeToJobEvents();
            UpdateView();
        }

        private void UpdateView()
        {
            _planning.Text = _job.Planning;
            _name.Text = _job.Name;
            DateTime expStart = _job.TimeReport.ExpectedStartDate;
            DateTime expEnd = _job.TimeReport.ExpectedEndDate;
            _expectedDates.Text =$" {expStart.ToShortDateString()} => {expEnd.ToShortDateString()}";
            UpdateStatus();
            _delay.Text = _job.GetDelay().ToString();
        }

        private void SubscribeToJobEvents()
        {
            _job.PropertyChanged += OnDatePropertyChanged;
        }

        private void UpdateStatus()
        {
            switch (_job.GetStatus())
            {
                case JobStatus.Todo:
                    SetStatusTodo();
                    break;
                case JobStatus.Doing:
                    SetStatusDoing();
                    break;
                case JobStatus.Done:
                    SetStatusDone();
                    break;
                default:
                    _status.Text = "Inconnu";
                    _status.Foreground = Brushes.LightGray;
                    break;
            };
        }

        private void SetStatusTodo()
        {
            _status.Text = "A faire";
            _status.Foreground = Brushes.Red;
        }

        private void SetStatusDoing()
        {
            _status.Text = "En cours";
            _status.Foreground = Brushes.Orange;
            UpdateStartDate();
        }

        private void SetStatusDone()
        {
            _status.Text = "Terminée";
            _status.Foreground = Brushes.Green;
            UpdateStartDate();
            UpdateEndDate();
        }
    }
}

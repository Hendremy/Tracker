using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Hendricé.Rémy.Poo.Tracker.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Hendricé.Rémy.Poo.Tracker.Gui
{
    public partial class FilterControls : UserControl
    {

        private ComboBox _filterParam;
        private Label _valueLabel;
        private TextBox _filterValue;
        private Label _dateLabel;
        private DatePicker _filterDate;

        public event EventHandler<FilterParams> FilterRequested;

        public FilterControls()
        {
            InitializeComponent();
            LocateControls();
            InitFilterOptions();
        }

        private void LocateControls()
        {
            _filterParam = this.FindControl<ComboBox>("FilterParam");
            _valueLabel = this.FindControl<Label>("ValueLabel");
            _filterValue = this.FindControl<TextBox>("FilterValue");
            _dateLabel = this.FindControl<Label>("DateLabel");
            _filterDate = this.FindControl<DatePicker>("FilterDate");
            _filterDate.SelectedDateChanged += FilterDate_Changed;
        }

        private void InitFilterOptions()
        {
            IList<ComboBoxItem> options = new List<ComboBoxItem>();
            foreach (FilterOption opt in Enum.GetValues<FilterOption>())
            {
                ComboBoxItem item = new ComboBoxItem() { Name = opt.ToString() };
                item.Content = new TextBlock() { Text = FilterOptionToString(opt) };
                options.Add(item);
            }
            _filterParam.Items = options;
        }

        private string FilterOptionToString(FilterOption opt) => opt switch
        {
            FilterOption.Planning => "Chantier",
            FilterOption.Status => "Status",
            FilterOption.Date => "Date",
            _ => "Aucun"
        };

        private void FireFilterEvent(FilterParams args)
        {
            FilterRequested?.Invoke(this, args);
        }

        private void FilterValue_Changed(object? sender, AvaloniaPropertyChangedEventArgs args)
        {
            string value = _filterValue.Text;
            if (!string.IsNullOrWhiteSpace(value))
            {
                FilterParams filterArgs = new FilterParams(GetSelectedFilterParam(), _filterValue.Text);
                FireFilterEvent(filterArgs);
            }
        }

        private void FilterDate_Changed(object? sender, DatePickerSelectedValueChangedEventArgs args)
        {
            FilterParams filterArgs = new FilterParams(GetSelectedFilterParam(), args.NewDate.ToString());
            FireFilterEvent(filterArgs);
        }

        private void FilterParam_Selected(object? sender, SelectionChangedEventArgs args)
        {
            FilterOption opt = GetSelectedFilterParam();
            ShowHideFilterVal(opt);
            if(GetSelectedFilterParam() == FilterOption.Date)
            {
                FilterParams filterArgs = new FilterParams(GetSelectedFilterParam(), _filterDate.SelectedDate.ToString());
                FireFilterEvent(filterArgs);
            }
            else
            {
                FilterParams filterArgs = new FilterParams(GetSelectedFilterParam(), _filterValue.Text);
                FireFilterEvent(filterArgs);
            }
        }

        private void ShowHideFilterVal(FilterOption opt)
        {
            if(opt == FilterOption.Date)
            {
                SwitchFilterIsDate(true);
            }
            else
            {
                if(opt == FilterOption.None)
                {
                    SetFilterDateVisible(false);
                    SetFilterValueVisible(false);
                }
                else
                {
                    SwitchFilterIsDate(false);
                }
            }
        }

        private FilterOption GetSelectedFilterParam()
        {
            FilterOption opt = FilterOption.None;
            if (_filterParam.SelectedItem != null)
            {
                ComboBoxItem selected = (ComboBoxItem)_filterParam.SelectedItem;
                if(Enum.TryParse(selected.Name, out opt))
                {
                    return opt;
                }
            }
            return FilterOption.None;
        }

        private void SwitchFilterIsDate(bool isDate)
        {
            SetFilterValueVisible(!isDate);
            SetFilterDateVisible(isDate);
        }

        private void SetFilterDateVisible(bool visible)
        {
            _dateLabel.IsVisible = visible;
            _filterDate.IsVisible = visible;
        }

        private void SetFilterValueVisible(bool visible)
        {
            _valueLabel.IsVisible = visible;
            _filterValue.IsVisible = visible;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

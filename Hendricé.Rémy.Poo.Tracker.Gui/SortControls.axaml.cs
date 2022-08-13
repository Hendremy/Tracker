using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Hendricé.Rémy.Poo.Tracker.Domains;
using System;
using System.Collections.Generic;

namespace Hendricé.Rémy.Poo.Tracker.Gui
{
    public partial class SortControls : UserControl
    {
        private ComboBox _sortParam;
        private ToggleSwitch _ascending;

        public event EventHandler<SortParams> SortRequested;

        public SortControls()
        {
            InitializeComponent();
            LocateControls();
            InitSortOptions();
        }

        private void LocateControls()
        {
            _sortParam = this.FindControl<ComboBox>("SortParam");
            _ascending = this.FindControl<ToggleSwitch>("Ascending");
        }

        private void InitSortOptions()
        {
            IList<ComboBoxItem> options = new List<ComboBoxItem>();
            foreach (SortOption opt in Enum.GetValues<SortOption>())
            {
                ComboBoxItem item = new ComboBoxItem() { Name = opt.ToString() };
                item.Content = new TextBlock() { Text = SortOptionToString(opt) };
                options.Add(item);
            }
            _sortParam.Items = options;
        }

        private string SortOptionToString(SortOption opt) => opt switch
        {
            SortOption.Planning => "Chantier",
            SortOption.Status => "Statut",
            _ => "Date de début"
        };

        private SortOption GetSelectedSortParam()
        {
            SortOption opt = SortOption.StartDate;
            if (_sortParam.SelectedItem != null)
            {
                ComboBoxItem selected = (ComboBoxItem)_sortParam.SelectedItem;
                if (Enum.TryParse(selected.Name, out opt))
                {
                    return opt;
                }
            }
            return SortOption.StartDate;
        }

        private void FireSortRequested()
        {
            bool ascending = _ascending.IsChecked != null ? (bool) _ascending.IsChecked : true;
            SortParams sortArgs = new SortParams(GetSelectedSortParam(), ascending);
        }

        private void SortParam_Selected(object? sender, SelectionChangedEventArgs args)
        {
            FireSortRequested();
        }

        private void Ascending_Changed(object? sender, RoutedEventArgs args)
        {
            FireSortRequested();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

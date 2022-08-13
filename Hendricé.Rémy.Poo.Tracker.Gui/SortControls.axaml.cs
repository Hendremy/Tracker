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

        private void SortParam_Selected(object? sender, SelectionChangedEventArgs args)
        {

        }

        private void Ascending_Changed(object? sender, RoutedEventArgs args)
        {

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

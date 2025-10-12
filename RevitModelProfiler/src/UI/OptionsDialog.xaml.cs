using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitModelProfiler.src.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RevitModelProfiler.src.UI
{
    public partial class OptionsDialog : Window
    {
        private readonly List<FamilyDataViewModel> _allFamilies = new List<FamilyDataViewModel>();
        private readonly AnalysisEventHandler _handler;
        private readonly ExternalEvent _externalEvent;

        public OptionsDialog(Document doc, AnalysisEventHandler handler, ExternalEvent externalEvent)
        {
            InitializeComponent();

            _handler = handler;
            _externalEvent = externalEvent;

            _handler.SetDocument(doc);
            _handler.SetUpdateAction(UpdateDataGrid);

            RefreshButton_Click(this, new RoutedEventArgs());
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            StatusTextBlock.Text = "Analyzing model...";
            _handler.Request = RequestType.Analyze;
            _externalEvent.Raise();
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            if (ResultsGrid.SelectedItem is FamilyDataViewModel selectedFamily)
            {
                _handler.Request = RequestType.Select;
                _handler.FamilyIdToSelect = selectedFamily.FamilyId;
                _externalEvent.Raise();
            }
        }

        private void ResultsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectButton.IsEnabled = ResultsGrid.SelectedItem != null;
        }

        public void UpdateDataGrid(List<FamilyReport> reports)
        {
            Dispatcher.Invoke(() =>
            {
                var viewModels = reports.Select(r => new FamilyDataViewModel
                {
                    FamilyId = r.FamilyId,
                    FamilyName = r.FamilyName,
                    InstanceCount = r.InstanceCount,
                    FaceCount = r.FaceCount,
                    MeshTriangleCount = r.MeshTriangleCount
                    // FamilyFileSizeMB conversion removed
                }).ToList();

                _allFamilies.Clear();
                _allFamilies.AddRange(viewModels);

                SearchTextBox_TextChanged(this, null);
                StatusTextBlock.Text = $"Analysis complete. Displayed {_allFamilies.Count} family types.";
            });
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs? e)
        {
            string searchText = SearchTextBox.Text.ToLowerInvariant();
            List<FamilyDataViewModel> displayList;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                displayList = _allFamilies;
            }
            else
            {
                displayList = _allFamilies
                    .Where(f => f.FamilyName.ToLowerInvariant().Contains(searchText))
                    .ToList();
            }

            ResultsGrid.ItemsSource = displayList.OrderByDescending(f => f.MeshTriangleCount + f.FaceCount).ToList();
        }
    }
}


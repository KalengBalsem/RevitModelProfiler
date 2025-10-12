using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace RevitModelProfiler.src.Commands
{
    public enum RequestType { Analyze, Select }

    public class AnalysisEventHandler : IExternalEventHandler
    {
        public RequestType Request { get; set; } = RequestType.Analyze;
        public long FamilyIdToSelect { get; set; } // Changed to long

        private Action<List<FamilyReport>>? _updateAction;
        private Document? _doc;

        public void SetUpdateAction(Action<List<FamilyReport>> updateAction) => _updateAction = updateAction;
        public void SetDocument(Document doc) => _doc = doc;

        public void Execute(UIApplication app)
        {
            if (_doc == null) return;

            switch (Request)
            {
                case RequestType.Analyze:
                    if (_updateAction == null) return;
                    List<FamilyReport> reports = AnalyzeFamilies.PerformAnalysis(_doc);
                    _updateAction(reports);
                    break;

                case RequestType.Select:
                    UIDocument uiDoc = app.ActiveUIDocument;
                    if (uiDoc == null) return;

                    try
                    {
                        var collector = new FilteredElementCollector(_doc);
                        var instances = collector.OfClass(typeof(FamilyInstance))
                            .Cast<FamilyInstance>()
                            // FIXED: Using .Value instead of the obsolete .IntegerValue
                            .Where(inst => inst.Symbol?.Family?.Id.Value == FamilyIdToSelect);

                        uiDoc.Selection.SetElementIds(instances.Select(inst => inst.Id).ToList());
                        SetForegroundWindow(app.MainWindowHandle);
                    }
                    catch { /* Fails silently */ }
                    break;
            }
        }

        public string GetName() => "Revit Model Profiler Event Handler";

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
    }
}


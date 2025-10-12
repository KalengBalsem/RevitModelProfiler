using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RevitModelProfiler.src.Utils;

namespace RevitModelProfiler.src.Commands
{
    [Transaction(TransactionMode.ReadOnly)]
    public class AnalyzeFamilies : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                var handler = new AnalysisEventHandler();
                var externalEvent = ExternalEvent.Create(handler);
                var dlg = new UI.OptionsDialog(commandData.Application.ActiveUIDocument.Document, handler, externalEvent);
                dlg.Show();
                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }
        }

        public static List<FamilyReport> PerformAnalysis(Document doc)
        {
            var collector = new FilteredElementCollector(doc)
                .OfClass(typeof(FamilyInstance))
                .WhereElementIsNotElementType();

            var instances = collector.Cast<FamilyInstance>();
            var reports = new Dictionary<long, FamilyReport>();

            foreach (var inst in instances)
            {
                var fam = inst.Symbol?.Family;
                if (fam == null) continue;
                long famId = fam.Id.Value;

                if (!reports.TryGetValue(famId, out var rpt))
                {
                    rpt = new FamilyReport
                    {
                        FamilyName = fam.Name,
                        FamilyId = famId,
                        InstanceCount = 0
                    };
                    reports[famId] = rpt;
                }

                reports[famId].InstanceCount++;
                try
                {
                    var opts = new Options { DetailLevel = ViewDetailLevel.Fine, ComputeReferences = false, IncludeNonVisibleObjects = false };
                    GeometryElement geom = inst.GetOriginalGeometry(opts) ?? inst.get_Geometry(opts);
                    var metrics = GeometryUtils.AnalyzeGeometryElement(geom);
                    // The line calculating SolidCount has been removed.
                    reports[famId].FaceCount = Math.Max(reports[famId].FaceCount, metrics.FaceCount);
                    reports[famId].MeshTriangleCount = Math.Max(reports[famId].MeshTriangleCount, metrics.MeshTriangleCount);
                }
                catch { /* ignore geometry errors */ }
            }

            return reports.Values.ToList();
        }
    }

    public class FamilyReport
    {
        public string FamilyName { get; set; } = "";
        public long FamilyId { get; set; }
        public int InstanceCount { get; set; }
        // SolidCount property removed
        public int FaceCount { get; set; }
        public int MeshTriangleCount { get; set; }
    }
}


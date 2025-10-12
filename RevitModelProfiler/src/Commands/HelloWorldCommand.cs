using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using System;

namespace RevitModelProfiler.src.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class HelloWorldCommand : IExternalCommand
    {
        public Result Execute(
            ExternalCommandData commandData,
            ref string message,
            ElementSet elements)
        {
            // Get the current Revit application and document
            UIApplication uiApp = commandData.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;

            // Simple Revit dialog box
            TaskDialog.Show("Hello Revit", "Hello World! This is your first Revit plugin.");

            return Result.Succeeded;
        }
    }
}

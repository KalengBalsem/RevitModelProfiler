using Autodesk.Revit.UI;
using System;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace RevitModelProfiler.src
{
    public class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication app)
        {
            RibbonPanel panel = app.CreateRibbonPanel("Model Profiler");
            string assemblyPath = Assembly.GetExecutingAssembly().Location;

            PushButtonData buttonData = new PushButtonData(
                "AnalyzeFamiliesButton",
                "Analyze\nFamilies",
                assemblyPath,
                "RevitModelProfiler.src.Commands.AnalyzeFamilies"
            );

            PushButton button = (PushButton)panel.AddItem(buttonData);

            button.ToolTip = "Analyzes Revit families to identify those with high geometric complexity.";

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication app)
        {
            return Result.Succeeded;
        }
    }
}


using Autodesk.Revit.UI;
using System;
using System.Windows.Media.Imaging;
using System.Reflection;

namespace RevitAddin
{
    public class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            // Create a custom ribbon tab
            string tabName = "My Add-in";
            application.CreateRibbonTab(tabName);

            // Create a ribbon panel
            RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName, "Tools");

            // Create button data
            PushButtonData buttonData = new PushButtonData(
                "cmdSample",           // Internal name
                "Sample Command",      // Display name
                Assembly.GetExecutingAssembly().Location,
                "RevitAddin.Command"  // Command class full name
            );

            // Add button to panel
            PushButton button = ribbonPanel.AddItem(buttonData) as PushButton;
            
            // Add tooltip
            button.ToolTip = "Execute sample command";

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
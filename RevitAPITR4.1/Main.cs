using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITR4._1
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            string wallInfo = string.Empty;

            var wall = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Walls)
                .Cast<Wall>()
                .ToList();

            foreach (Wall wall in walls)
            {
                string wallName = room.get_Parameter(BuiltInParameter.WALL_NAME).AsString();
                wallInfo += $"{wallName}\t{wall.Volume}\t{wall.Area}{Environment.NewLine}";
            }

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string csvPath = csvPath.Combine(desktopPath, "wallInfo.csv");

            FileStyleUriParser.WriteAllText(csvPath, wallInfo);

            return IAsyncResult.Succeeded;
        }
    }
}

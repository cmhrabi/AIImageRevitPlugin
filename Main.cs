using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.ApplicationServices;
using System.IO;

namespace AIImageRevitPlugin
{
    public class Main
    {
        [Transaction(TransactionMode.Manual)]
        public class WallFilter : IExternalCommand
        {
            public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
            {
                UIApplication uiApp = commandData.Application;
                UIDocument uiDoc = uiApp.ActiveUIDocument;
                Application app = uiApp.Application;
                Document doc = uiDoc.Document;

                var categories = new List<BuiltInCategory>()
            {
                BuiltInCategory.OST_Floors,
                BuiltInCategory.OST_Walls,
            };

                var multiCategoryFilter = new ElementMulticategoryFilter(categories);
                var collector = new FilteredElementCollector(doc)
                     .WherePasses(multiCategoryFilter);

                return Result.Succeeded;
            }
        }

        [Transaction(TransactionMode.Manual)]
        public class MainFunction : IExternalCommand
        {
            public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
            {
                UIApplication uiApp = commandData.Application;
                UIDocument uiDoc = uiApp.ActiveUIDocument;
                Application app = uiApp.Application;
                Document doc = uiDoc.Document;

                var element = uiDoc.Selection.GetElementIds().Select(
                    x => doc.GetElement(x)).First();

                var value = element.LookupParameter("Comments").AsString();
                TaskDialog.Show("Message", value);

                return Result.Succeeded;
            }
        }

        [Transaction(TransactionMode.Manual)]
        public class Export_JPG : IExternalCommand
        {
            public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
            {
                UIApplication uiApp = commandData.Application;
                UIDocument uiDoc = uiApp.ActiveUIDocument;
                Application app = uiApp.Application;
                Document doc = uiDoc.Document;

                string fileName = "exported_image.jpg";

                var BilledeExportOptions = new ImageExportOptions
                {
                    ZoomType = ZoomFitType.FitToPage,
                    PixelSize = 2024,
                    FitDirection = FitDirectionType.Horizontal,
                    FilePath = Environment.CurrentDirectory + @"\Data\" + fileName,
                    HLRandWFViewsFileType = ImageFileType.JPEGLossless,
                    ImageResolution = ImageResolution.DPI_600,
                };

                doc.ExportImage(BilledeExportOptions);

                return Result.Succeeded;
            }
        }
    }
}
}

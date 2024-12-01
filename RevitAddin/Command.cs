using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitAddin
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Get UIDocument and Document
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            try
            {
                // Start transaction
                using (Transaction trans = new Transaction(doc, "Sample Command"))
                {
                    trans.Start();

                    // Add your code here
                    // Example: Create a line
                    XYZ point1 = new XYZ(0, 0, 0);
                    XYZ point2 = new XYZ(10, 10, 0);
                    Line line = Line.CreateBound(point1, point2);

                    // Create a model line
                    using (Plane plane = Plane.CreateByNormalAndOrigin(XYZ.BasisZ, XYZ.Zero))
                    {
                        SketchPlane sketchPlane = SketchPlane.Create(doc, plane);
                        ModelCurve modelLine = doc.Create.NewModelCurve(line, sketchPlane);
                    }

                    trans.Commit();
                }

                return Result.Succeeded;
            }
            catch (System.Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }
        }
    }
}
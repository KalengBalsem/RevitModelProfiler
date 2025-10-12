using Autodesk.Revit.DB;

namespace RevitModelProfiler.src.Utils
{
    public static class GeometryUtils
    {
        public class Metrics
        {
            public int FaceCount;
            public int MeshTriangleCount;
        }

        public static Metrics AnalyzeGeometryElement(GeometryElement? geom)
        {
            var metrics = new Metrics();
            if (geom == null) return metrics;

            foreach (GeometryObject obj in geom)
            {
                if (obj is Solid solid)
                {
                    // Solids have faces (BRep geometry)
                    metrics.FaceCount += solid.Faces.Size;
                }
                else if (obj is Mesh mesh)
                {
                    // Meshes have triangles (faceted geometry)
                    metrics.MeshTriangleCount += mesh.NumTriangles;
                }
                else if (obj is GeometryInstance inst)
                {
                    // Recursively analyze geometry inside nested instances
                    var subMetrics = AnalyzeGeometryElement(inst.GetInstanceGeometry());
                    metrics.FaceCount += subMetrics.FaceCount;
                    metrics.MeshTriangleCount += subMetrics.MeshTriangleCount;
                }
            }

            return metrics;
        }
    }
}

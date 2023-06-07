using Common.Primitives;
using Common.Structures.Numerics;

namespace Common.Structures.Traceable;

public class Mesh : ITraceable
{
    public List<Triangle> Triangles;

    public Mesh()
    {
        Triangles = new List<Triangle>();
    }

    public Mesh(List<Triangle> triangles)
    {
        Triangles = triangles;
    }

    public TraceResult? Trace(Ray ray)
    {
        var minDist = float.MaxValue;
        TraceResult? closest = null;
        foreach (var triangle in Triangles)
        {
            var intersection = triangle.Trace(ray);
            if (intersection == null) continue;
            
            var distance = Point.GetDistance(ray.Origin, intersection.IntersectionPoint);
            if (distance < minDist)
            {
                closest = intersection;
                minDist = distance;
            }
        }

        return closest;
    }

    public (bool, ITraceable?) Intersects(Ray ray)
    {
        foreach (var triangle in Triangles)
        {
            (bool, ITraceable?) intersects = triangle.Intersects(ray);
            if (intersects.Item1 && intersects.Item2 != triangle) return intersects;
        }

        return (false, null);
    }
}
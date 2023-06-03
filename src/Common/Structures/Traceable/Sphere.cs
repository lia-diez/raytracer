using Common.Primitives;
using Common.Structures.Numerics;

namespace Common.Structures.Traceable;

public class Sphere : ITraceable
{
    public Point Center;
    public float Radius;

    public Sphere(Point center, float radius)
    {
        Center = center;
        Radius = radius;
    }

    public TraceResult? Trace(Ray ray)
    {
        var k = ray.Origin - Center;
        
        var a = Vector3.DotProduct(ray.Direction, ray.Direction);
        var b = 2 * Vector3.DotProduct(ray.Direction, k);
        var c = Vector3.DotProduct(k, k) - Radius * Radius;

        var result = SolveQuadraticEquation(a, b, c);
        if (result == null) return null;

        var valid = result.Where(t => t >= 0);
        if (!valid.Any()) return null;
        var closestDistance = valid.Min();

        var point = ray.Origin.Translate(ray.Direction * closestDistance);
        
        return new TraceResult(point - Center, point);
    }

    private IEnumerable<float>? SolveQuadraticEquation(float a, float b, float c)
    {
        var d = b * b - 4 * a * c;
        if (d < 0) return null;

        var x1 = (float)(Math.Sqrt(d) - b) / (2 * a);
        var x2 = (float)(-1 * Math.Sqrt(d) - b) / (2 * a);
        
        if (Math.Abs(x1 - x2) < 0.000001) return new[] { x1 };
        return new[] { x1, x2 };
    }
}
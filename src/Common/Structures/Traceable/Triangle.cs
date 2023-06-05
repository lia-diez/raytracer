using Common.Primitives;
using Common.Structures.Numerics;

namespace Common.Structures.Traceable;

public class Triangle : ITraceable
{
    public Point A => Points[0];
    public Point B => Points[1];
    public Point C => Points[2];

    public Point[] Points;
    public Vector3 Normal => Vector3.CrossProduct(B-A, C-A).Normalize();
    
    public Triangle(Point a, Point b, Point c)
    {
        Points = new[] { a, b, c };
    }

    public Triangle(Point a, Point b, Point c, Vector3 normal)
    {
        Points = new[] { a, b, c };
    }
    
    public TraceResult? Trace(Ray ray)
    {
        const float epsilon = 0.0000001f;
        var edge1 = B - A;
        var edge2 = C - A;
        var h = Vector3.CrossProduct(ray.Direction, edge2);
        var a = Vector3.DotProduct(edge1, h);

        if (a > -epsilon && a < epsilon)
            return null;  
        
        var f = 1.0f / a;
        var s = ray.Origin - A;
        var u = f * Vector3.DotProduct(s, h);

        if (u < 0.0 || u > 1.0)
            return null;

        var q = Vector3.CrossProduct(s,edge1);
        var v = f * Vector3.DotProduct(ray.Direction, q);

        if (v < 0.0 || u + v > 1.0)
            return null;

        float t = f * Vector3.DotProduct(edge2, q);

        if (t > epsilon)
        {
            var outIntersectionPoint = ray.Origin.Translate(ray.Direction * t);
            return new TraceResult(this, Normal, outIntersectionPoint);
        }

        return null;
    }

    public (bool, ITraceable) Intersects(Ray ray)
    {
        const float epsilon = 0.0000001f;
        var edge1 = B - A;
        var edge2 = C - A;
        var h = Vector3.CrossProduct(ray.Direction, edge2);
        var a = Vector3.DotProduct(edge1, h);

        if (a > -epsilon && a < epsilon)
           return (false, this);  
        
        var f = 1.0f / a;
        var s = ray.Origin - A;
        var u = f * Vector3.DotProduct(s, h);

        if (u < 0.0 || u > 1.0)
            return (false, this);  

        var q = Vector3.CrossProduct(s,edge1);
        var v = f * Vector3.DotProduct(ray.Direction, q);

        if (v < 0.0 || u + v > 1.0)
            return (false, this);  

        var t = f * Vector3.DotProduct(edge2, q);

        return (t > epsilon, this);
    }

    public Point GetCenter() => new Point((A.X + B.X + C.X) / 3, (A.Y + B.Y + C.Y) / 3, (A.Z + B.Z + C.Z) / 3);
}
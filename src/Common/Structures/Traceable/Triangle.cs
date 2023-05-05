using Common.Primitives;
using Common.Structures.Numerics;

namespace Common.Structures.Traceable;

public class Triangle : ITraceable
{
    public Point A;
    public Point B;
    public Point C;

    public Vector3 Normal;
    
    public Triangle(Point a, Point b, Point c)
    {
        A = a;
        B = b;
        C = c;
    }

    public Triangle(Point a, Point b, Point c, Vector3 normal)
    {
        A = a;
        B = b;
        C = c;
        Normal = normal;
    }
    
    public Point? FindIntersection(Ray ray)
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
            return outIntersectionPoint;
        }

        return null;
    }

    public Vector3 GetNormal(Point point) => Normal;
}
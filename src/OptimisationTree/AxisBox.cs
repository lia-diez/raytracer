using Common.Structures;
using Common.Structures.Traceable;

namespace OptimisationTree;

public class AxisBox
{
    public List<Triangle> Triangles;

    public (float X, float Y, float Z) Min => Bounds[0];
    public (float X, float Y, float Z) Max => Bounds[1];
    public (float X, float Y, float Z)[] Bounds;

    private float? _surface;

    public float Surface
    {
        get
        {
            _surface ??= CalculateSurface();
            return _surface.Value;
        }
    }

    public AxisBox((float X, float Y, float Z) min, (float X, float Y, float Z) max)
    {
        Bounds = new[] { min, max };
        Triangles = new List<Triangle>();
    }

    private float CalculateSurface()
    {
        var x = Math.Abs(Max.X - Min.X);
        var y = Math.Abs(Max.Y - Min.Y);
        var z = Math.Abs(Max.Z - Min.Z);

        return 2 * (x * y + y * z + z * x);
    }

    public bool IsInBox(Triangle triangle)
    {
        var center = triangle.GetCenter();
        if (center.X < Min.X || center.X > Max.X
                             || center.Y < Min.Y || center.Y > Max.Y
                             || center.Z < Min.Z || center.Z > Max.Z)
            return false;

        return true;
    }

    public bool Intersects(Ray ray)
    {
        float txmin, txmax, tymin, tymax, tzmin, tzmax;
        
        txmin = (Bounds[ray.Signs.X].X - ray.Origin.X) * ray.InversedDirection.X;
        txmax = (Bounds[1-ray.Signs.X].X - ray.Origin.X) * ray.InversedDirection.X;
        tymin = (Bounds[ray.Signs.Y].Y - ray.Origin.Y) * ray.InversedDirection.Y;
        tymax = (Bounds[1-ray.Signs.Y].Y - ray.Origin.Y) * ray.InversedDirection.Y;
    
        if (txmin > tymax || tymin > txmax)
            return false;

        if (tymin > txmin)
            txmin = tymin;
        if (tymax < txmax)
            txmax = tymax;
    
        tzmin = (Bounds[ray.Signs.Z].Z - ray.Origin.Z) * ray.InversedDirection.Z;
        tzmax = (Bounds[1-ray.Signs.Z].Z - ray.Origin.Z) * ray.InversedDirection.Z;
    
        if (txmin > tzmax || tzmin > txmax)
            return false;

        if (tzmin > txmin)
            txmin = tzmin;
        if (tzmax < txmax)
            txmax = tzmax;

        return true;
    }
}

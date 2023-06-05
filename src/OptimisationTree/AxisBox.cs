using Common.Structures;
using Common.Structures.Traceable;

namespace OptimisationTree;

public class AxisBox
{
    public List<Triangle> Triangles;

    public (float X, float Y, float Z) Min;
    public (float X, float Y, float Z) Max;

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
        Min = min;
        Max = max;
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
        float tmin, tmax, tymin, tymax, tzmin, tzmax;
        if (ray.Direction.X >= 0)
        {
            tmin = (Min.X - ray.Origin.X) / ray.Direction.X;
            tmax = (Max.X - ray.Origin.X) / ray.Direction.X;
        }
        else
        {
            tmin = (Max.X - ray.Origin.X) / ray.Direction.X;
            tmax = (Min.X - ray.Origin.X) / ray.Direction.X;
        }

        if (ray.Direction.Y >= 0)
        {
            tymin = (Min.Y - ray.Origin.Y) / ray.Direction.Y;
            tymax = (Max.Y - ray.Origin.Y) / ray.Direction.Y;
        }
        else
        {
            tymin = (Max.Y - ray.Origin.Y) / ray.Direction.Y;
            tymax = (Min.Y - ray.Origin.Y) / ray.Direction.Y;
        }

        if (tmin > tymax || tymin > tmax)
            return false;
        
        if (tymin > tmin)
            tmin = tymin;
        if (tymax < tmax)
            tmax = tymax;
        
        if (ray.Direction.Z >= 0)
        {
            tzmin = (Min.Z - ray.Origin.Z) / ray.Direction.Z;
            tzmax = (Max.Z - ray.Origin.Z) / ray.Direction.Z;
        }
        else
        {
            tzmin = (Max.Z - ray.Origin.Z) / ray.Direction.Z;
            tzmax = (Min.Z - ray.Origin.Z) / ray.Direction.Z;
        }

        if (tmin > tzmax || tzmin > tmax)
            return false;
        if (tzmin > tmin)
            tmin = tzmin;
        if (tzmax < tmax)
            tmax = tzmax;
        
        return ((tmin < t1) && (tmax > t0));
    }
}

}
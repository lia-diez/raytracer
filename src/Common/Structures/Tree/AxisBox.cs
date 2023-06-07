using Common.Structures;
using Common.Structures.Traceable;

namespace OptimisationTree;

public class AxisBox
{
    public float[] Min => Bounds[0];
    public float[] Max => Bounds[1];
    public readonly float[][] Bounds;

    public float Surface => CalculateSurface();

    public AxisBox((float x, float y, float z) min, (float x, float y, float z) max)
    {
        Bounds = new[]
        {
            new[] { min.x, min.y, min.z },
            new[] { max.x, max.y, max.z }
        };
    }

    public AxisBox(float[] min, float[] max)
    {
        Bounds = new float[2][];
        Bounds[0] = new float[3];
        Bounds[1] = new float[3];
        Array.Copy(min, Bounds[0], 3);
        Array.Copy(max, Bounds[1], 3);
    }
    
    public AxisBox(float[][] bounds)
    {
        Bounds = bounds;
    }

    private float CalculateSurface()
    {
        var x = Math.Abs(Max[0] - Min[0]);
        var y = Math.Abs(Max[1] - Min[1]);
        var z = Math.Abs(Max[2] - Min[2]);

        return 2 * (x * y + y * z + z * x);
    }

    public bool Contains(Triangle triangle)
    {
        var center = triangle.Center;
        return !(center.X < Min[0]) && !(center.X > Max[0])
            && !(center.Y < Min[1]) && !(center.Y > Max[1]) 
            && !(center.Z < Min[2]) && !(center.Z > Max[2]);
    }

    public bool Intersects(Ray ray)
    {
        float txmin, txmax, tymin, tymax, tzmin, tzmax;
        
        txmin = (Bounds[ray.Signs.X][0] - ray.Origin.X) * ray.InversedDirection.X;
        txmax = (Bounds[1-ray.Signs.X][0] - ray.Origin.X) * ray.InversedDirection.X;
        tymin = (Bounds[ray.Signs.Y][1] - ray.Origin.Y) * ray.InversedDirection.Y;
        tymax = (Bounds[1-ray.Signs.Y][1] - ray.Origin.Y) * ray.InversedDirection.Y;
    
        if (txmin > tymax || tymin > txmax)
            return false;

        if (tymin > txmin)
            txmin = tymin;
        if (tymax < txmax)
            txmax = tymax;
    
        tzmin = (Bounds[ray.Signs.Z][2] - ray.Origin.Z) * ray.InversedDirection.Z;
        tzmax = (Bounds[1-ray.Signs.Z][2] - ray.Origin.Z) * ray.InversedDirection.Z;
    
        if (txmin > tzmax || tzmin > txmax)
            return false;

        if (tzmin > txmin)
            txmin = tzmin;
        if (tzmax < txmax)
            txmax = tzmax;

        return txmax >= 0 && tymax >= 0 && tzmax >= 0;
    }
}

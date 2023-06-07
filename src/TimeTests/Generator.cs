using Common.Structures;
using Common.Structures.Numerics;
using Common.Structures.Traceable;
using OptimisationTree;

namespace TimeTests;

public class Generator
{
    public static Triangle CreateTriangle((int x, int y, int z) sceneSize, (int min, int max) triangleSize)
    {
        var random = new Random();
        Triangle? triangle = null;
        do
        {
            var p1 = new Point(random.Next(sceneSize.x), random.Next(sceneSize.y), random.Next(sceneSize.z));
            var p2 = new Point(p1.X + random.Next(triangleSize.min, triangleSize.max)/3f,
                p1.Y + random.Next(triangleSize.min, triangleSize.max)/3f,
                p1.Z + random.Next(triangleSize.min, triangleSize.max)/3f);
            var p3 = new Point(p1.X + random.Next(triangleSize.min, triangleSize.max)/3f,
                p1.Y + random.Next(triangleSize.min, triangleSize.max)/3f,
                p1.Z + random.Next(triangleSize.min, triangleSize.max)/3f);

            var a1 = Point.GetDistance(p1, p2);
            var a2 = Point.GetDistance(p3, p2);
            var a3 = Point.GetDistance(p1, p3);

            if (a1 + a2 > a3 && a2 + a3 > a1 && a1 + a3 > a2)
                triangle = new Triangle(p1, p2, p3);
        } while (triangle == null);

        return triangle;
    }
    
    public static Ray CreateRay((int x, int y, int z) sceneSize)
    {
        var random = new Random();
        var origin = new Point(random.Next(sceneSize.x), random.Next(sceneSize.y), random.Next(sceneSize.z));
        var direction = new Vector3((float)random.NextDouble() - 0.5f,
            (float)random.NextDouble() - 0.5f, 
            (float)random.NextDouble() - 0.5f).Normalize();
        return new Ray(origin, direction);
    }
    
    public static AxisBox CreateBox((int x, int y, int z) sceneSize, (int min, int max) boxSize)
    {
        var random = new Random();
        var p1 = new Point(random.Next(sceneSize.x), random.Next(sceneSize.y), random.Next(sceneSize.z));
        var p2 = new Point(p1.X + random.Next(boxSize.min, boxSize.max)/3f,
            p1.Y + random.Next(boxSize.min, boxSize.max)/3f,
            p1.Z + random.Next(boxSize.min, boxSize.max)/3f);

        return new AxisBox((Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), Math.Min(p1.Z, p2.Z)),
            (Math.Max(p1.X, p2.X), Math.Max(p1.Y, p2.Y), Math.Max(p1.Z, p2.Z)));
    }
}
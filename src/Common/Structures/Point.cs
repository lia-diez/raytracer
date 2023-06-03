using Common.Structures.Numerics;

namespace Common.Structures;

public class Point
{
    public Point(float X, float Y, float Z)
    {
        this.X = X;
        this.Y = Y;
        this.Z = Z;
    }

    public static Point Zero => new Point(0, 0, 0);
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }

    public Point Translate(Vector3 translation)
    {
        return new Point(X + translation.X, Y + translation.Y, Z + translation.Z);
    }
    
    public static Vector3 operator -(Point p1, Point p2)
    {
        return new Vector3(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
    }

    public static float GetDistance(Point p1, Point p2)
    {
        var p3 = p2 - p1;
        return (float)Math.Sqrt(p3.X * p3.X + p3.Y * p3.Y + p3.Z * p3.Z);
    }

    public void Deconstruct(out float X, out float Y, out float Z)
    {
        X = this.X;
        Y = this.Y;
        Z = this.Z;
    }
}
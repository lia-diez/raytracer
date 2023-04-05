namespace Common.Structures;

public record Point (float X, float Y, float Z)
{
    public static Point Zero => new Point(0, 0, 0);
    public Point Translate(Vector translation)
    {
        return new Point(X + translation.X, Y + translation.Y, Z + translation.Z);
    }
    
    public static Vector operator -(Point p1, Point p2)
    {
        return new Vector(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
    }
}
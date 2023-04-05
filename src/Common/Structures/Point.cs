namespace Common.Structures;

public record Point (float X, float Y, float Z)
{
    public Point Translate(Vector translation)
    {
        return new Point(X + translation.X, Y + translation.Y, Z + translation.Z);
    }
}
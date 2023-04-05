namespace Common.Structures;

public record Vector(float X, float Y, float Z)
{
    public static Vector Zero => new Vector(0, 0, 0);

    private float? _magnitude;
    public float Magnitude => _magnitude ??= MathF.Sqrt(X*X + Y*Y + Z*Z);

    public Vector Normalize()
    {
        return this / Magnitude;
    }

    public static Vector operator +(Vector v1, Vector v2)
    {
        return new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
    }

    public static Vector operator -(Vector v1, Vector v2)
    {
        return new Vector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
    }

    public static Vector operator *(Vector v, float multiplier)
    {
        return new Vector(v.X * multiplier, v.Y * multiplier, v.Z * multiplier);
    }

    public static Vector operator *(float multiplier, Vector v)
    {
        return v * multiplier;
    }

    public static Vector operator /(Vector v, float divider)
    {
        return new Vector(v.X / divider, v.Y / divider, v.Z / divider);
    }

    public static Vector operator -(Vector v)
    {
        return v * -1;
    }

    public static float DotProduct(Vector v1, Vector v2)
    {
        return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
    }

    public static Vector CrossProduct(Vector v1, Vector v2)
    {
        var x = (v1.Y * v2.Z) - (v1.Z * v2.Y);
        var y = (v1.Z * v2.X) - (v1.X * v2.Z);
        var z = (v1.X * v2.Y) - (v1.Y * v2.X);
        return new Vector(x, y, z);
    }
}
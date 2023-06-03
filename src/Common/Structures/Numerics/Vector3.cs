namespace Common.Structures.Numerics;

public record Vector3(float X, float Y, float Z)
{
    public static Vector3 Zero => new Vector3(0, 0, 0);

    private float? _magnitude;
    public float Magnitude => _magnitude ??= MathF.Sqrt(X*X + Y*Y + Z*Z);

    public Vector3 Normalize()
    {
        return this / Magnitude;
    }

    public static Vector3 operator +(Vector3 v1, Vector3 v2)
    {
        return new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
    }

    public static Vector3 operator -(Vector3 v1, Vector3 v2)
    {
        return new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
    }

    public static Vector3 operator *(Vector3 v, float multiplier)
    {
        return new Vector3(v.X * multiplier, v.Y * multiplier, v.Z * multiplier);
    }

    public static Vector3 operator *(float multiplier, Vector3 v)
    {
        return v * multiplier;
    }

    public static Vector3 operator /(Vector3 v, float divider)
    {
        return new Vector3(v.X / divider, v.Y / divider, v.Z / divider);
    }

    public static Vector3 operator -(Vector3 v)
    {
        return v * -1;
    }

    public static float DotProduct(Vector3 v1, Vector3 v2)
    {
        return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
    }

    public static Vector3 CrossProduct(Vector3 v1, Vector3 v2)
    {
        var x = (v1.Y * v2.Z) - (v1.Z * v2.Y);
        var y = (v1.Z * v2.X) - (v1.X * v2.Z);
        var z = (v1.X * v2.Y) - (v1.Y * v2.X);
        return new Vector3(x, y, z);
    }

    public override string ToString()
    {
        return $"{X} {Y} {Z}";
    }
}
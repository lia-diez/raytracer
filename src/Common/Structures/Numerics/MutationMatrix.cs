using Common.Extensions;

namespace Common.Structures.Numerics;

public static class MutationMatrix
{
    public static Matrix FromScale(float x, float y, float z)
    {
        if (x.Equalish(1) && y.Equalish(1) && z.Equalish(1))
            return Matrix.Identity(4);

        return new Matrix(new[,]
        {
            { x, 0, 0, 0 },
            { 0, y, 0, 0 },
            { 0, 0, z, 0 },
            { 0, 0, 0, 1 }
        });
    }
    
    public static Matrix FromTranslation(float x, float y, float z)
    {
        if (x == 0 && y == 0 && z == 0)
            return Matrix.Identity(4);

        return new Matrix(new[,]
        {
            { 1, 0, 0, x },
            { 0, 1, 0, y },
            { 0, 0, 1, z },
            { 0, 0, 0, 1 }
        });
    }
    
    public static Matrix FromRotation(float x, float y, float z)
    {
        var result = Matrix.Identity(4);

        if (x == 0 && y == 0 && z == 0)
            return result;

        if (x != 0) result *= FromRotationX(x);
        if (y != 0) result *= FromRotationY(y);
        if (z != 0) result *= FromRotationZ(z);

        return result;
    }

    public static Matrix FromRotationX(float degree)
    {
        if (degree == 0f) return Matrix.Identity(4);
        return new Matrix(new[,]
        {
            { 1, 0, 0, 0 },
            { 0, MathF.Cos(degree), -MathF.Sin(degree), 0 },
            { 0, MathF.Sin(degree), MathF.Cos(degree), 0 },
            { 0, 0, 0, 1 }
        });
    }

    public static Matrix FromRotationY(float degree)
    {
        if (degree == 0f) return Matrix.Identity(4);
        return new Matrix(new[,]
        {
            { MathF.Cos(degree), 0, MathF.Sin(degree), 0 },
            { 0, 1, 0, 0 },
            { -MathF.Sin(degree), 0, MathF.Cos(degree), 0 },
            { 0, 0, 0, 1 }
        });
    }

    public static Matrix FromRotationZ(float degree)
    {
        if (degree == 0f) return Matrix.Identity(4);
        return new Matrix(new[,]
        {
            { MathF.Cos(degree), -MathF.Sin(degree), 0, 0 },
            { MathF.Sin(degree), MathF.Cos(degree), 0, 0 },
            { 0, 0, 1, 0 },
            { 0, 0, 0, 1 }
        });
    }
}


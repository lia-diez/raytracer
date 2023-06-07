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

    public static Matrix RemoveTranslationScale(Matrix source)
    {
        var matrix = source.Copy();
        var s = new float[3];
        for (var i = 0; i < 3; i++)
        {
            matrix[i, 3] = 0;
            s[i] = new Vector3(matrix[0, i], matrix[1, i], matrix[2, i]).Magnitude;
        }

        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                matrix[j, i] /= s[i];
            }
        }

        return matrix;
    }
    
    // from System.Numerics Matrix4x4
    #region CalcRotationMatrix
    public static Matrix CalculateRotationMatrix(Vector3 first, Vector3 second)
    {
        first = first.Normalize();
        second = second.Normalize();

        var axis = Vector3.CrossProduct(first, second);
        var angle = (float)Math.Acos(Vector3.DotProduct(first, second));

        var rotationMatrix = CreateFromAxisAngle(axis, angle);

        return rotationMatrix;
    }

    private static Matrix CreateFromAxisAngle(Vector3 axis, float angle)
    {
        float x = axis.X, y = axis.Y, z = axis.Z;
        float sa = MathF.Sin(angle), ca = MathF.Cos(angle);
        float xx = x * x, yy = y * y, zz = z * z;
        float xy = x * y, xz = x * z, yz = y * z;

        var result = new Matrix(4, 4)
        {
            [3, 3] = 1,
            [0, 0] = xx + ca * (1.0f - xx),
            [0, 1] = xy - ca * xy + sa * z,
            [0, 2] = xz - ca * xz - sa * y,
            [1, 0] = xy - ca * xy - sa * z,
            [1, 1] = yy + ca * (1.0f - yy),
            [1, 2] = yz - ca * yz + sa * x,
            [2, 0] = xz - ca * xz + sa * y,
            [2, 1] = yz - ca * yz - sa * x,
            [2, 2] = zz + ca * (1.0f - zz)
        };

        return result;
    }
    #endregion

}


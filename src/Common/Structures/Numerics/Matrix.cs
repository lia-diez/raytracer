using System.Text;
using Common.Extensions;

namespace Common.Structures.Numerics;

public class Matrix
{
    private float[,] _rawMatrix;
    public Vector2Int Size;

    public Matrix(float[,] rawMatrix)
    {
        _rawMatrix = rawMatrix;
        Size = new Vector2Int(rawMatrix.GetLength(0), rawMatrix.GetLength(1));
    }

    public Matrix(Vector2Int size)
    {
        _rawMatrix = new float[size.X, size.Y];
        Size = size;
    }
    
    public Matrix(int size)
    {
        Size = new Vector2Int(size, size);
        _rawMatrix = IdentityArray(size);
    }

    public Matrix(int x, int y)
    {
        _rawMatrix = new float[x, y];
        Size = new Vector2Int(x, y);
    }

    public float this[int x, int y]
    {
        get => _rawMatrix[x, y];
        set => _rawMatrix[x, y] = value;
    }

    public static Matrix Identity(int n)
    {
        var result = new float[n, n];
        for (int i = 0; i < n; i++)
        {
            result[i, i] = 1;
        }

        return new Matrix(result);
    }
    
    public static float[,] IdentityArray(int n)
    {
        var result = new float[n, n];
        for (int i = 0; i < n; i++)
        {
            result[i, i] = 1;
        }

        return result;
    }

    public static Matrix operator *(Matrix first, Matrix second)
    {
        if (first.Size.Y != second.Size.X) 
            throw new ArgumentException("First matric o-jpsdjfosjf");

        var result = new Matrix(first.Size.X, second.Size.Y);

        for (int i = 0; i < first.Size.X; i++)
        {
            for (int j = 0; j < second.Size.Y; j++)
            {
                var current = 0f;
                for (int k = 0; k < second.Size.X; k++)
                {
                    current += first[i, k] * second[k, j];
                }

                result[i, j] = current;
            }
        }

        return result;
    }

    public Matrix Rotate(float x, float y, float z)
    {
        return this * MutationMatrix.FromRotation(x, y, z);
    }
    public Matrix Scale(float x, float y, float z)
    {
        return this * MutationMatrix.FromScale(x, y, z);
    }
    public Matrix Translate(float x, float y, float z)
    {
        return this * MutationMatrix.FromTranslation(x, y, z);
    }

    public Matrix Copy()
    {
        return new Matrix(TwoDimensionalArray<float>.Copy(_rawMatrix));
    }

    public override string ToString()
    {
        var strBuilder = new StringBuilder();
        for (int i = 0; i < Size.X; i++)
        {
            for (int j = 0; j < Size.Y; j++)
            {
                strBuilder.Append($"{this[i, j]} ");
            }

            strBuilder.Append('\n');
        }
        
        return strBuilder.ToString();
    }
}
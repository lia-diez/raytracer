namespace Common.Structures;

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

    public static Matrix? operator *(Matrix first, Matrix second)
    {
        if (first.Size.Y != second.Size.X)
            return null;

        var result = new Matrix(second.Size.Y, first.Size.X);

        for (int i = 0; i < first.Size.X; i++)
        {
            for (int j = 0; j < second.Size.Y; j++)
            {
                var current = 0f;
                for (int k = 0; k < second.Size.X; k++)
                {
                    current += first[i, k] * second[k, j];
                }

                result[j, i] = current;
            }
        }

        return result;
    }
}

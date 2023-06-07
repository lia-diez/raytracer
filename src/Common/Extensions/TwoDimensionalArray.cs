namespace Common.Extensions;

public static class TwoDimensionalArray<T> where T : struct
{
    public static T[,] Copy(T[,] source)
    {
        var matrix = new T[source.GetLength(0), source.GetLength(1)];
        for (var i = 0; i < matrix.GetLength(0); i++)
        {
            for (var j = 0; j < matrix.GetLength(1); j++)
            {
                matrix[i, j] = source[i, j];
            }
        }

        return matrix;
    }

    public static T[][] Copy(T[][] source)
    {
        var matrix = new T[source.GetLength(0)][];
        for (var i = 0; i < matrix.GetLength(0); i++)
        {
            matrix[i] = new T[source[i].Length];
            for (var j = 0; j < matrix[i].Length; j++)
            {
                matrix[i][j] = source[i][j];
            }
        }

        return matrix;
    }
}
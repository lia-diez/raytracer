namespace Common.Structures.Numerics;

public record Vector4(float X, float Y, float Z, float A)
{
    public Vector4 Transform(Matrix transformation)
    {
        var temp = transformation * new Matrix(new float[,] { { X}, {Y}, {Z}, {A} });
        return new Vector4(temp[0, 0], temp[0, 1], temp[0, 2], temp[0, 3]);
    }
}
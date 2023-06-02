namespace Common.Extensions;

public static class SingleExtension
{
    private const float Precision = 0.000001f;

    public static bool Equalish(this float f1, float f2)
    {
        return MathF.Abs(f1 - f2) < Precision;
    }
}
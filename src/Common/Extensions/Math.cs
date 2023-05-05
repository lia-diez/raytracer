namespace Core.Extensions;

public static class MathExtensions
{
    public static float DegreeToRad(int degree) => (float) (degree * Math.PI / 180);
}
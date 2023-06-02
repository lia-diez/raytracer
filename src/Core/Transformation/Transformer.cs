using Common.Structures;
using Common.Structures.Numerics;
using Common.Structures.Traceable;

namespace Core;

public static class Transformer
{
    public static Vector3 Transform(Vector3 vector, Matrix transformation)
    {
        // if (Math.Pow(transformation[0, 0], 2) + Math.Pow(transformation[0, 1], 2) == 1 &&
        //     Math.Pow(transformation[1, 0], 2) + Math.Pow(transformation[1, 1], 2) == 1 &&
        //     transformation[0, 2] == 0 && transformation[1, 2] == 0 &&
        //     transformation[2, 0] == 0 && transformation[2, 1] == 0)
        // {
            var temp = transformation * new Matrix(new[,] { { vector.X }, { vector.Y }, { vector.Z }, { 1 } });
            return new Vector3(temp[0, 0], temp[1, 0], temp[2, 0]).Normalize();
        // }

        // throw new Exception(
            // $"You cannot scale or translate vector. The problem is with ({vector.X} {vector.Y} {vector.Z})");
    }

    public static Point Transform(Point point, Matrix transformation)
    {
        var temp = transformation * new Matrix(new[,] { { point.X }, { point.Y }, { point.Z }, { 1 } });
        return new Point(temp[0, 0], temp[1, 0], temp[2, 0]);
    }
    
    public static Triangle Transform(Triangle triangle, Matrix transformation)
    {
        var points = new Point[3];
        for (var index = 0; index < triangle.GetPoints.Length; index++)
        {
            var point = triangle.GetPoints[index];
            var temp = transformation * new Matrix(new[,] { { point.X }, { point.Y }, { point.Z }, { 1 } });
            points[index] = new Point(temp[3, 0], temp[3, 1], temp[3, 2]);
        }

        return new Triangle(points[0], points[1], points[2]);
    }
}
using Common.Structures;
using Common.Structures.Numerics;
using Common.Structures.Traceable;
using OptimisationTree;

namespace Core.Transformation;

public static class Transformer
{
    public static Vector3 Transform(Vector3 vector, Matrix transformation)
    {
        var temp = transformation * new Matrix(new[,] { { vector.X }, { vector.Y }, { vector.Z }, { 1 } });
        return new Vector3(temp[0, 0], temp[1, 0], temp[2, 0]).Normalize();
    }

    public static Point Transform(Point point, Matrix transformation)
    {
        var temp = transformation * new Matrix(new[,] { { point.X }, { point.Y }, { point.Z }, { 1 } });
        return new Point(temp[0, 0], temp[1, 0], temp[2, 0]);
    }

    public static Triangle Transform(Triangle triangle, Matrix transformation)
    {
        var points = new Point[3];
        for (var index = 0; index < triangle.Points.Length; index++)
        {
            var point = triangle.Points[index];
            var temp = transformation * new Matrix(new[,] { { point.X }, { point.Y }, { point.Z }, { 1 } });
            points[index] = new Point(temp[0, 0], temp[1, 0], temp[2, 0]);
        }

        return new Triangle(points[0], points[1], points[2]);
    }

    public static Mesh Transform(Mesh mesh, Matrix transformation)
    {
        var temp = new HashSet<Point>();
        foreach (var triangle in mesh.Triangles)
        {
            temp.Add(triangle.A);
            temp.Add(triangle.B);
            temp.Add(triangle.C);
        }

        var points = temp.ToList();

        for (var i = 0; i < points.Count; i++)
        {
            var temp2 = transformation *
                        new Matrix(new[,] { { points[i].X }, { points[i].Y }, { points[i].Z }, { 1 } });
            points[i].X = temp2[0, 0];
            points[i].Y = temp2[1, 0];
            points[i].Z = temp2[2, 0];
        }

        return mesh;
    }
    
    public static TreeMesh Transform(TreeMesh mesh, Matrix transformation)
    {
        var temp = new HashSet<Point>();
        for (var i = 0; i < mesh.Triangles.Count; i++)
        {
            mesh.Triangles[i] = Transform(mesh.Triangles[i], transformation);
        }

        mesh.Tree = new Tree();
        mesh.Tree.Build(mesh.Triangles);
        return mesh;
    }
}
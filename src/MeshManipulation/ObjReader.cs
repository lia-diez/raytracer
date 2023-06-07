using Common.Structures;
using Common.Structures.Traceable;

namespace MeshManipulation;

public static class ObjReader
{
    public static Mesh ReadObj(string path)
    {
        using var sr = new StreamReader(File.OpenRead(path));
        var vertices = new List<Point>();
        var triangles = new List<Triangle>();
        while (sr.ReadLine() is { } line)
        {
            var tokens = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            switch (tokens[0])
            {
                case "v":
                    if (tokens.Length >= 4) 
                        vertices.Add(ParsePoint(tokens[1..4]));
                    break;
                case "f":
                    if (tokens.Length >= 4) 
                        triangles.Add(ParseTriangle(tokens[1..4], vertices));
                    break;
            }
        }
        return new Mesh(triangles);
    }

    private static Point ParsePoint(string[] coordinates)
    {
        if (coordinates.Length != 3) throw new ArgumentException();
        if (float.TryParse(coordinates[0], out var x) &&
            float.TryParse(coordinates[1], out var y) && 
            float.TryParse(coordinates[2], out var z))
        {
            return new Point(x, y, z);
        }

        throw new ArgumentException();
    }
    
    private static Triangle ParseTriangle(string[] coordinates, List<Point> points)
    {
        if (coordinates.Length != 3) throw new ArgumentException();
        for (var i = 0; i < coordinates.Length; i++)
        {
            var s = coordinates[i];
            coordinates[i] = s[..s.IndexOf('/')];
        }
        if (int.TryParse(coordinates[0], out var a) &&
            int.TryParse(coordinates[1], out var b) && 
            int.TryParse(coordinates[2], out var c))
        {
            return new Triangle(points[a - 1], points[b - 1], points[c - 1]);
        }
        throw new ArgumentException();
    }
}
using System.Globalization;
using Common.Extensions;
using Common.Structures.Traceable;

namespace OptimisationTree.Trees;

public class BoxNode
{
    public BoxNode? Parent;
    public BoxNode? LeftChild;
    public BoxNode? RightChild;

    public readonly AxisBox Box;
    public readonly int DivideAxis;
    public List<Triangle> Triangles;

    public BoxNode(BoxNode parent, AxisBox box, int divideAxis)
    {
        Box = box;
        DivideAxis = divideAxis;
        Triangles = new List<Triangle>();
    }

    public BoxNode(BoxNode parent, AxisBox box, int divideAxis, List<Triangle> triangles)
    {
        Box = box;
        DivideAxis = divideAxis;
        Triangles = triangles;
    }

    public List<(BoxNode left, BoxNode right)> Divide(int num)
    {
        var newAxis = DivideAxis == 2 ? 0 : DivideAxis + 1;
        (float min, float max) bounds = (Box.Bounds[0][newAxis], Box.Bounds[1][newAxis]);
        var result = new List<(BoxNode left, BoxNode right)>();

        var segmentLength = Math.Abs(bounds.max - bounds.min);
        var pieceLength = segmentLength / num;

        for (var i = 1; i < num; i++)
        {
            var delta = i * pieceLength;

            result.Add((
                new BoxNode(this, new AxisBox(ChangeBounds(newAxis, (bounds.min, bounds.min + delta))), newAxis),
                new BoxNode(this, new AxisBox(ChangeBounds(newAxis, (bounds.min + delta, bounds.max))), newAxis)));
        }

        return result;
    }

    private float[][] ChangeBounds(int axis, (float min, float max) bounds)
    {
        var newBounds = TwoDimensionalArray<float>.Copy(Box.Bounds);
        newBounds[0][axis] = bounds.min;
        newBounds[1][axis] = bounds.max;
        return newBounds;
    }

    public static float[][] GetBounds(List<Triangle> triangles)
    {
        var bounds = new[]
        {
            new[] { float.MaxValue, float.MaxValue, float.MaxValue },
            new[] { float.MinValue, float.MinValue, float.MinValue }
        };

        foreach (var triangle in triangles)
        {
            for (var i = 0; i < triangle.Coordinates.Length; i++)
            {
                var min = triangle.Coordinates[i].Min();
                var max = triangle.Coordinates[i].Max();

                if (min < bounds[0][i])
                    bounds[0][i] = min;

                if (max > bounds[1][i])
                    bounds[1][i] = max;
            }
        }

        return bounds;
    }

    public override string ToString() => $"{Box.Min[0]:0.00} {Box.Min[1]:0.00} {Box.Min[2]:0.00}   " +
                                         $"{Box.Max[0]:0.00} {Box.Max[1]:0.00} {Box.Max[2]:0.00}";

    public void PrintPretty(string indent, bool last)
    {
        Console.Write(indent);
        if (last)
        {
            Console.Write("\\-");
            indent += "  ";
        }
        else
        {
            Console.Write("|-");
            indent += "| ";
        }

        Console.WriteLine(LeftChild != null
            ? $"{Box.Min[0]:0.00} {Box.Min[1]:0.00} {Box.Min[2]:0.00}   " +
              $"{Box.Max[0]:0.00} {Box.Max[1]:0.00} {Box.Max[2]:0.00}"
            : $"triangles");
        if (LeftChild == null || RightChild == null)
            return;

        LeftChild.PrintPretty(indent, false);
        RightChild.PrintPretty(indent, true);
    }
}
using Common.Extensions;
using Common.Primitives;
using Common.Structures;
using Common.Structures.Traceable;
using Microsoft.VisualBasic;
using OptimisationTree.Trees;

namespace OptimisationTree;

public class Tree
{
    public static readonly float TriangleTime = 2.27f;
    public static readonly float BoxTime = 0.96f;
    public static readonly int DivideNum = 10;

    public BoxNode Root;
    
    public void Build(List<Triangle> triangles)
    {
        Root = new BoxNode(null, new AxisBox(BoxNode.GetBounds(triangles)), 2, triangles); // 0 is x

        var stack = new Stack<BoxNode>();
        stack.Push(Root);

        while (stack.TryPop(out var current))
        {
            var notLeaf = AddChildren(current);
            if (!notLeaf)
                continue;

            stack.Push(current.RightChild);
            stack.Push(current.LeftChild);
        }
    }

    private bool AddChildren(BoxNode parent)
    {
        var options = parent.Divide(DivideNum);
        var bestHeuristic = CalculateHeuristic(parent.Triangles);
        (BoxNode left, BoxNode right)? bestPair = null;

        foreach (var pair in options)
        {
            pair.left.Triangles = GetTrianglesResizeChild(pair.left, parent.Triangles);
            pair.right.Triangles = GetTrianglesResizeChild(pair.right, parent.Triangles);

            //TODO: добавить трикутники які не попали ні туди ні туди!

            var heuristic = CalculateHeuristic(pair, parent);

            if (heuristic < bestHeuristic)
            {
                bestHeuristic = heuristic;
                bestPair = pair;
            }
        }

        if (bestPair == null)
            return false;

        parent.LeftChild = bestPair.Value.left;
        parent.RightChild = bestPair.Value.right;

        return true;
    }

    private float CalculateHeuristic(List<Triangle> triangles)
        => BoxTime + triangles.Count * TriangleTime;

    private float CalculateHeuristic((BoxNode left, BoxNode right) pair, BoxNode parent)
        => 2 * BoxTime + pair.left.Triangles.Count * TriangleTime * pair.left.Box.Surface / parent.Box.Surface +
           pair.right.Triangles.Count * TriangleTime * pair.right.Box.Surface / parent.Box.Surface;

    private List<Triangle>
        GetTrianglesResizeChild(BoxNode node, List<Triangle> triangles)
    {
        var result = new List<Triangle>();
        var newMin = node.Box.Bounds[0][node.DivideAxis];
        var newMax = node.Box.Bounds[1][node.DivideAxis];

        foreach (var triangle in triangles)
        {
            if (node.Box.Contains(triangle))
            {
                var max = triangle.Coordinates[node.DivideAxis].Max();
                if (max > newMax)
                    newMax = max;

                var min = triangle.Coordinates[node.DivideAxis].Min();
                if (min < newMin)
                    newMin = min;

                result.Add(triangle);
            }
        }

        node.Box.Bounds[0][node.DivideAxis] = newMin;
        node.Box.Bounds[1][node.DivideAxis] = newMax;

        return result;
    }
    
    public void Search(Triangle triangle)
    {
        var stack = new Stack<BoxNode>();
        stack.Push(Root);

        while (stack.TryPop(out var current))
        {
            var yes = current.Box.Contains(triangle);
            // Console.WriteLine($"now at {current}");
            if (current.LeftChild == null)
            {
                if (current.Triangles.Contains(triangle))
                {
                    // Console.WriteLine($"exp: {triangle.Center.X} {triangle.Center.Y} {triangle.Center.Z}\n" +
                    // $"act: {current.Triangles[current.Triangles.IndexOf(triangle)].Center.X} " +
                    // $"{current.Triangles[current.Triangles.IndexOf(triangle)].Center.Y} " +
                    // $"{current.Triangles[current.Triangles.IndexOf(triangle)].Center.Z}");
                    return;
                }
                continue;
            }

            if (yes)
            {
                stack.Push(current.RightChild);
                stack.Push(current.LeftChild);
            }
        }
    }
}
using Common.Primitives;
using OptimisationTree;
using OptimisationTree.Trees;

namespace Common.Structures.Traceable;

public class TreeMesh : ITraceable
{
    public List<Triangle> Triangles;
    public Tree? Tree;

    public TreeMesh()
    {
        Triangles = new List<Triangle>();
    }

    public TreeMesh(List<Triangle> triangles)
    {
        Triangles = triangles;
        Tree = new Tree();
        Tree.Build(triangles);
    }

    public TraceResult? Trace(Ray ray)
    {
        var stack = new Stack<BoxNode>();
        stack.Push(Tree.Root);
        
        var minDist = float.MaxValue;
        TraceResult? closest = null;

        while (stack.TryPop(out var current))
        {
            var intersects = current.Box.Intersects(ray);
            if (!intersects) continue;
            if (current.LeftChild != null)
            {
                stack.Push(current.RightChild);
                stack.Push(current.LeftChild);
            }
            else
            {
                foreach (var triangle in current.Triangles)
                {
                    var intersection = triangle.Trace(ray);
                    if (intersection == null) continue;
            
                    var distance = Point.GetDistance(ray.Origin, intersection.IntersectionPoint);
                    if (distance < minDist)
                    {
                        closest = intersection;
                        minDist = distance;
                    }
                }
            }
        }
        
        return closest;
    }

    public (bool, ITraceable?) Intersects(Ray ray)
    {
        var stack = new Stack<BoxNode>();
        stack.Push(Tree.Root);
        
        while (stack.TryPop(out var current))
        {
            var intersectsBox = current.Box.Intersects(ray);
            if (!intersectsBox) continue;
            if (current.LeftChild != null)
            {
                stack.Push(current.RightChild);
                stack.Push(current.LeftChild);
            }
            else
            {
                foreach (var triangle in current.Triangles)
                {
                    (bool, ITraceable?) intersectsTriangle = triangle.Intersects(ray);
                    if (intersectsTriangle.Item1) 
                        return intersectsTriangle;
                }
            }
        }
        
        return (false, null);
    }
}
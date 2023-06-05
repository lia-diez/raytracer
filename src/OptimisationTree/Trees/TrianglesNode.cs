using Common.Structures.Traceable;

namespace OptimisationTree;

public class TrianglesNode : ReferenceNode
{
    public List<Triangle> Triangles;

    public TrianglesNode(ReferenceNode parent) : base(parent)
    {
        Triangles = new List<Triangle>();
    }
    
    public TrianglesNode(ReferenceNode parent, List<Triangle> triangles) : base(parent)
    {
        Triangles = triangles;
    }
}
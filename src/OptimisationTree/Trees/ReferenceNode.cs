namespace OptimisationTree;

public abstract class ReferenceNode
{
    public ReferenceNode? Parent;
    public ReferenceNode? LeftChild;
    public ReferenceNode? RightChild;

    public ReferenceNode(ReferenceNode parent)
    {
        Parent = parent;
    }

    public bool IsLeaf => LeftChild == null;
}
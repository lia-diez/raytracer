namespace OptimisationTree;

public class BoxNode : ReferenceNode
{
    public AxisBox Box;

    public BoxNode(ReferenceNode parent, AxisBox box) : base(parent)
    {
        Box = box;
    }

    public void AddChildren(AxisBox left, AxisBox right)
    {
        LeftChild = new BoxNode(this, left);
        RightChild = new BoxNode(this, right);
    }
}
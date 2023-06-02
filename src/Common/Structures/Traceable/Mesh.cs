namespace Common.Structures.Traceable;

public class Mesh
{
    public List<Triangle> Triangles;

    public Mesh()
    {
        Triangles = new List<Triangle>();
    }

    public Mesh(List<Triangle> triangles)
    {
        Triangles = triangles;
    }
}
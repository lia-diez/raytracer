using Common.Primitives;
using Common.Structures.Numerics;

namespace Common.Structures.Traceable;

public class Mesh
{
    public List<Triangle> Triangles;

    public Mesh()
    {
        Triangles = new List<Triangle>();
    }
}
using Common.Primitives;
using Common.Structures;

namespace Common.SceneObjects;

public class Camera : ICamera
{
    public Point Origin;
    public Vector Direction;
    public float Fov;
    public (int Width, int Height) Resolution;
    
    public Camera(Point origin, Vector direction, int fov, (int Width, int Height) resolution)
    {
        Origin = origin;
        Direction = direction;
        Resolution = resolution;
        Fov = DegreeToRad(fov);
    }

    public Camera()
    {
    }

    public Ray[,] Render()
    {
        var a = CreateRays();
        return a;
    }

    private Ray[,] CreateRays()
    {
        var pixelSize = (float)(2 * Math.Tan(Fov) / Resolution.Height);
        var edge = FindEdge(pixelSize);
        var rays = new Ray[Resolution.Width,Resolution.Height];
        for (int i = 0; i < Resolution.Width; i++)
        {
            for (int j = 0; j < Resolution.Height; j++)
            {
                rays[i, j] = new Ray
                (
                    Origin,
                    new Vector(edge.X + pixelSize * i, edge.Y + pixelSize * j, edge.Z).Normalize()
                );
                // yield return new Ray
                // (
                //     Origin,
                //     new Vector(edge.X + pixelSize * i, edge.Y + pixelSize * j, edge.Z).Normalize()
                // );
            }
        }

        return rays;
    }

    private Point FindEdge(float pixelSize)
    {
        var (x, y, z) = Origin.Translate(Direction);
        if (Resolution.Width % 2 == 0)
            x += pixelSize / 2;
        if (Resolution.Height % 2 == 0)
            y += pixelSize / 2;

        x -= pixelSize * Resolution.Width / 2;
        y -= pixelSize * Resolution.Height / 2;

        return new Point(x, y, z);
    }
    
    public static float DegreeToRad(int degree) => (float)(degree * Math.PI / 180);

}
using Common.Extensions;
using Common.Primitives;
using Common.Structures;

namespace Common.SceneObjects;

public class Camera : ICamera
{
    public Point Origin;
    public Vector Direction;
    public float Fov;
    public Rect Resolution;
    
    public Camera(CameraSettings settings)
    {
        Origin = settings.Origin;
        Direction = settings.Direction;
        Resolution = settings.Resolution;
        Fov = MathExtensions.DegreeToRad(settings.Fov);
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

}
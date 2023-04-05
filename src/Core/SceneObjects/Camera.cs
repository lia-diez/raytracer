using Common.Extensions;
using Common.Primitives;
using Common.Structures;
using Common.Structures.Traceable;

namespace Core.SceneObjects;

public class Camera : ICamera
{
    public Point Origin;
    public Vector Direction;
    public float Fov;
    public Vector2Int Resolution;
    
    //todo: remove
    ITraceable Sphere = new Sphere(new Point(0, 0, 5), 1);
    
    public Camera(CameraSettings settings)
    {
        Origin = settings.Origin;
        Direction = settings.Direction;
        Resolution = settings.Resolution;
        Fov = MathExtensions.DegreeToRad(settings.Fov);
    }

    public IBitmap Render()
    {
        var rays = CreateRays();
        var bitmap = new Bitmap(Resolution with { });
        for (int i = 0; i < rays.GetLength(0); i++)
        {
            for (int j = 0; j < rays.GetLength(1); j++)
            {
                var color = Sphere.FindIntersection(rays[i, j]) == null ? new Color(0) : new Color(1);
                bitmap.SetPixel(i, j, color);
            }
        }
        return bitmap;
    }

    private Ray[,] CreateRays()
    {
        var pixelSize = (float)(2 * Math.Tan(Fov) / Resolution.Y);
        var edge = FindEdge(pixelSize);
        var rays = new Ray[Resolution.X,Resolution.Y];
        for (int i = 0; i < Resolution.X; i++)
        {
            for (int j = 0; j < Resolution.Y; j++)
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
        if (Resolution.X % 2 == 0)
            x += pixelSize / 2;
        if (Resolution.Y % 2 == 0)
            y += pixelSize / 2;

        x -= pixelSize * Resolution.X / 2;
        y -= pixelSize * Resolution.Y / 2;

        return new Point(x, y, z);
    }

}
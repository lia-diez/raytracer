using Common.Extensions;
using Common.Primitives;
using Common.Structures;
using Common.Structures.Numerics;
using Common.Structures.Traceable;

namespace Core.SceneObjects;

public class Camera : ICamera
{
    public Point Origin;
    public Vector3 Direction;
    public float Fov;
    public Vector2Int Resolution;
    public Scene Scene;
    
    public Camera(CameraSettings settings, Scene scene)
    {
        Scene = scene;
        Origin = settings.Origin;
        Direction = settings.Direction;
        Resolution = settings.Resolution;
        Fov = MathExtensions.DegreeToRad(settings.Fov);
    }

    public IBitmap Render()
    {
        var bitmap = new Bitmap(Resolution with { });
        var pixelSize = (float)(2 * Math.Tan(Fov) / Resolution.Y);
        var edge = FindEdge(pixelSize);
        
        for (int i = 0; i < Resolution.X; i++)
        {
            for (int j = 0; j < Resolution.Y; j++)
            {
                var ray = new Ray
                (
                    Origin,
                    new Vector3(edge.X + pixelSize * i, edge.Y + pixelSize * j, edge.Z).Normalize()
                );

                var light = Scene.Lights.First();
                (ITraceable trace, Point intersect)? closest = null;
                float minDist = float.MaxValue;
                foreach (var iTraceable in Scene.Traceables)
                {
                    var intersection = iTraceable.FindIntersection(ray);
                    if (intersection != null)
                    {
                        var distance = Point.GetDistance(Origin, intersection);
                        if (distance < minDist)
                        {
                            closest = (iTraceable, intersection);
                            minDist = distance;
                        }
                    }
                }
                var color = closest == null ? 
                    new Color(0) : 
                    new Color(light.ComputeColor(closest.Value.trace.GetNormal(closest.Value.intersect)));
                bitmap.SetPixel(i, j, color);
            }
        }

        return bitmap;
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
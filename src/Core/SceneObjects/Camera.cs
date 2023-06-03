using Common.Extensions;
using Common.Primitives;
using Common.Structures;
using Common.Structures.Numerics;
using Common.Structures.Traceable;

namespace Core.SceneObjects;

public class Camera : ICamera
{
    private readonly Point _origin;
    private readonly Vector3 _direction;
    private readonly float _fov;
    private readonly Vector2Int _resolution;
    private readonly Matrix? _transformation;
    private readonly Matrix? _rotationOnlyTransform;
    private readonly Scene _scene;


    public Camera(Scene scene)
    {
        _scene = scene;
        _origin = Point.Zero;
        _direction = new Vector3(0, 0, 1);
        _fov = (float)Math.PI / 3;
        _resolution = new Vector2Int(128, 128);
        _transformation = null;
    }

    public Camera(CameraSettings settings, Scene scene)
    {
        _scene = scene;
        _resolution = settings.Resolution;
        _fov = MathExtensions.DegreeToRad(settings.Fov);
        _origin = new Point(0, 0,0);
        _direction = new Vector3(0, 0, 1);
        
        if (settings.Transformation != null)
        {
            _transformation = settings.Transformation;
            _origin = Transformer.Transform(_origin, _transformation);
            _rotationOnlyTransform = MutationMatrix.RemoveTranslationScale(_transformation);
            _direction = Transformer.Transform(_direction, _rotationOnlyTransform);
        }
    }

    public IBitmap Render()
    {
        var bitmap = new Bitmap(_resolution with { });
        var pixelSizeY = (float)(2 * Math.Tan(_fov / 2) / _resolution.Y);
        var pixelSizeX = pixelSizeY;
        var edge = FindEdge(pixelSizeX, pixelSizeY);

        var rays = new List<Ray>();

        for (int i = 0; i < _resolution.X; i++)
        {
            for (int j = 0; j < _resolution.Y; j++)
            {
                var direction = new Vector3(edge.X + pixelSizeX * i, edge.Y + pixelSizeY * j, edge.Z).Normalize();
                if (_transformation != null)
                    direction = Transformer.Transform(direction, _rotationOnlyTransform);

                var ray = new Ray
                (
                    _origin,
                    direction
                );
                rays.Add(ray);

                var color = GetPixelColor(ray);
                bitmap.SetPixel(i, j, color);
            }
        }

        return bitmap;
    }

    public Color GetPixelColor(Ray ray)
    {
        var light = _scene.Lights.First(); // TODO: do normal lights

        TraceResult? closest = null;
        float minDist = float.MaxValue;
        foreach (var iTraceable in _scene.Traceables)
        {
            var intersection = iTraceable.Trace(ray);
            if (intersection == null) continue;

            var distance = Point.GetDistance(ray.Origin, intersection.IntersectionPoint);
            if (distance < minDist)
            {
                closest = intersection;
                minDist = distance;
            }
        }

        var color = new Color(0);
        if (closest != null)
        {
            var shadowRay = new Ray(closest.IntersectionPoint, light.Direction);
            var intersects = false;
            foreach (var iTraceable in _scene.Traceables)
            {
                var intersection = iTraceable.Intersects(shadowRay);
                if (intersection.Item1 && closest.Traceable != iTraceable)
                {
                    intersects = true;
                    break;
                }
            }
            
            if (!intersects)
                color = new Color(light.ComputeColor(closest.Normal));
        }

        return color;
    }

    private Point FindEdge(float pixelSizeX, float pixelSizeY)
    {
        var (x, y, z) = _origin.Translate(new Vector3(0, 0, 1));
        x += pixelSizeX / 2;
        y += pixelSizeY / 2;

        x -= pixelSizeX * _resolution.X / 2;
        y -= pixelSizeY * _resolution.Y / 2;

        return new Point(x, y, z);
    }
}
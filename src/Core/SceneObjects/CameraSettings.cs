using Common.Structures;

namespace Core.SceneObjects;

public record CameraSettings
{

    public int Fov { get; init; } = 60;
    public Point Origin { get; init; } = new (0, 0, 0);
    public Vector Direction { get; init; } = new(0, 0, 1);
    public required Vector2Int Resolution { get; init; }
}

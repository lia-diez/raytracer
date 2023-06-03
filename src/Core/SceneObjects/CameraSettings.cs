using Common.Structures;
using Common.Structures.Numerics;

namespace Core.SceneObjects;

public record CameraSettings
{
    public int Fov { get; init; } = 60;
    public required Vector2Int Resolution { get; init; }
    public Matrix? Transformation { get; init; }
}

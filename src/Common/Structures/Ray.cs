using Common.Structures.Numerics;

namespace Common.Structures;

public record Ray(Point Origin, Vector3 Direction)
{
    public readonly Vector3 InversedDirection = 1 / Direction;

    private (int X, int Y, int Z)? _signs;
    public (int X, int Y, int Z) Signs
    {
        get
        {
            _signs ??= (Convert.ToInt32(InversedDirection.X < 0), 
                Convert.ToInt32(InversedDirection.Y < 0),
                Convert.ToInt32(InversedDirection.Z < 0));
            return _signs.Value;
        }
    }
}
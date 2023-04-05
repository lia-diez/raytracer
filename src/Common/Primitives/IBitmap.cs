using Common.Structures;

namespace Common.Primitives;

public interface IBitmap
{
    public Vector2Int Resolution { get; }
    public void SetPixel(Vector2Int position, Color color);
    public void SetPixel(int x, int y, Color color);
    public Color GetPixel(int x, int y);
    public Color GetPixel(Vector2Int position);
    public Color this[int x, int y] { get; set; }
}
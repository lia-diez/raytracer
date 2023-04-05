using Common.Primitives;

namespace Common.Structures;

public class Bitmap : IBitmap
{
    private Color[,] _pixels; 
    public Bitmap(Vector2Int resolution)
    {
        Resolution = resolution;
        _pixels = new Color[resolution.X, resolution.Y];
    }

    public Vector2Int Resolution { get; }
    public void SetPixel(Vector2Int position, Color color)
    {
        SetPixel(position.X, position.Y, color);
    }

    public void SetPixel(int x, int y, Color color)
    {
        _pixels[x, y] = color;
    }

    public Color GetPixel(int x, int y)
    {
        return _pixels[x, y];
    }

    public Color GetPixel(Vector2Int position)
    {
        return GetPixel(position.X, position.Y);
    }

    public Color this[int x, int y]
    {
        get => GetPixel(x,y);
        set => SetPixel(x, y, value);
    }
}
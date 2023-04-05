using System.Collections.Concurrent;

namespace Common.Structures;

public record Color
{
    private readonly float _r;
    private readonly float _g;
    private readonly float _b;

    public float R
    {
        get => _r;
        init => _r  = NormalizeFloat(value);
    }

    public float G
    {
        get => _g;
        init => _g = NormalizeFloat(value);
    }

    public float B
    {
        get => _b;
        init => _b = NormalizeFloat(value);
    }


    private float NormalizeFloat(float n) => MathF.Max(0, MathF.Min(n, 1f));
    public Color(float r, float g, float b)
    {
        R = r;
        G = g;
        B = b;
    }

    public Color(float gray)
    {
        R = gray;
        G = gray;
        B = gray;
    }

    public static Color FromHex(string hex)
    {
        var (r, g, b) = (0, 0, 0);
        hex = hex.Replace("#", "");
        if (hex.Length == 6)
        {
            r = Convert.ToInt32(hex[..2], 16);
            g = Convert.ToInt32(hex[2..4], 16);
            b = Convert.ToInt32(hex[4..6], 16);
        }
        else throw new ArgumentException();

        return FromRgb(r, g, b);
    }

    public static Color FromRgb(int r, int g, int b)
    {
        return new Color(r / 255f, g / 255f, b / 255f);
    }

    public static Color Gray(int gray)
    {
        return FromRgb(gray, gray, gray);
    }

    public static Color operator +(Color c1, Color c2)
    {
        return new Color(c1.R + c2.R, c1.G + c2.G, c1.B + c2.B);
    }
    
    public static Color operator *(Color c1, Color c2)
    {
        return new Color(c1.R * c2.R, c1.G * c2.G, c1.B * c2.B);
    }
}
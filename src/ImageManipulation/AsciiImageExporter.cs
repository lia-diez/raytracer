using Common.Primitives;

namespace raytracer;

public class AsciiImageExporter : IImageExporter
{
    public AsciiImageExporter(Stream destination, IBitmap bitmap)
    {
        Destination = destination;
        Bitmap = bitmap;
    }

    public Stream Destination { get; set; }
    public IBitmap Bitmap { get; set; }
    public void Export()
    {
        using (var sw = new StreamWriter(Destination))
        {
            sw.AutoFlush = true;
            for (int i = 0; i < Bitmap.Resolution.X; i++)
            {
                for (int j = 0; j < Bitmap.Resolution.Y; j++)
                {
                    sw.Write(Bitmap.GetPixel(i, j).R >= 0.8f ? "OO" : "  ");
                }
                sw.WriteLine();
            }
            
        }
    }
}
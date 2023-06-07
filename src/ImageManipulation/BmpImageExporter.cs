using Common.Primitives;

namespace raytracer;

public class BmpImageExporter : IImageExporter
{
    public BmpImageExporter(Stream destination, IBitmap bitmap)
    {
        Destination = destination;
        Bitmap = bitmap;
    }

    public Stream Destination { get; set; }
    public IBitmap Bitmap { get; set; }
    public void Export()
    {
        using var br = new BinaryWriter(Destination);
        br.Write("BM"u8);
        var picSize = Bitmap.Resolution.X * Bitmap.Resolution.Y * 3 + 55;
        br.Write(picSize);
        br.Write(0);
        br.Write(54);
        br.Write(40);
        br.Write(Bitmap.Resolution.Y);
        br.Write(Bitmap.Resolution.X);
        //[1, 0, 24, 0]
        br.Write(1572865);
        for (var i = 0; i < 6; i++)
        {
            br.Write(0);
        }

        for (var i = 0; i < Bitmap.Resolution.X; i++)
        {
            for (var j = 0; j < Bitmap.Resolution.Y; j++)
            {
                var color = Bitmap[i, j];
                br.Write((byte)(color.B*255));
                br.Write((byte)(color.G*255));
                br.Write((byte)(color.R*255));
            }
        }
        br.Write((byte) 0);
    }
}
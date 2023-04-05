using Common.Primitives;

namespace raytracer;

public interface IImageExporter
{
    public Stream Destination { get; set; }
    public IBitmap Bitmap { get; set; }
    public void Export();
}
using System;
using System.Diagnostics;
using System.Drawing;

namespace RayTracing
{
  static class Extensions
  {
    public static Color AsColor(this Vector3 v) => Color.FromArgb((int)(255.99 * v.X), (int)(255.99 * v.Y), (int)(255.99 * v.Z));
  }

  class Program
  {
    private static Vector3 Sky(Ray r) => Vector3.Lerp((1, 1, 1), (.5, .7, 1), (r.Direction.Normalize().Y + 1) / 2);

    private static Vector3 NormalToColor(Vector3 normal) => (normal + Vector3.One) / 2;

    static Vector3 Color(Ray r, IHitable world)
    {
      var t = world.Hit(r, 0, double.MaxValue);
      return t != null ? NormalToColor(t.Value.Normal) : Sky(r);
    }

    static void Main(string[] _)
    {
      const int nx = 200;
      const int ny = 100;
      const int ns = 100;

      var rand = new Random();
      var bmp = new Bitmap(nx, ny, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

      var world = new HitableList
      {
        new Sphere((0, 0, -1), .5),
        new Sphere((0, -100.5, -1), 100)
      };

      var cam = new Camera();

      for (var j = 0; j < ny; j++)
      {
        for (var i = 0; i < nx; i++)
        {
          var col = Vector3.Zero;
          for (var s = 0; s < ns; s++)
          {
            var u = (i + rand.NextDouble()) / nx;
            var v = (j + rand.NextDouble()) / ny;
            var r = cam.GetRay(u, v);
            col = col + Color(r, world);
          }

          bmp.SetPixel(i, ny - 1 - j, (col / ns).AsColor());
        }
      }

      bmp.Save("output.png");

      var fileopener = new Process();
      fileopener.StartInfo.FileName = "explorer";
      fileopener.StartInfo.Arguments = "\"output.png\"";
      fileopener.Start();
    }
  }
}

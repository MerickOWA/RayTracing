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
      var nx = 200;
      var ny = 100;

      var bmp = new Bitmap(nx, ny, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

      Vector3 lowerLeftCorner = (-2, -1, -1);
      Vector3 horizontal = (4, 0, 0);
      Vector3 vertical = (0, 2, 0);
      var origin = Vector3.Zero;
      var world = new HitableList
      {
        new Sphere((0, 0, -1), .5),
        new Sphere((0, -100.5, -1), 100)
      };

      for (var j = 0; j < ny; j++)
      {
        for (var i = 0; i < nx; i++)
        {
          var u = (double)i / nx;
          var v = (double)j / ny;
          var r = new Ray(origin, lowerLeftCorner + u * horizontal + v * vertical);
          var col = Color(r, world).AsColor();

          bmp.SetPixel(i, ny - 1 - j, col);
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

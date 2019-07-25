using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace RayTracing
{
  static class Extensions
  {
    public static Color AsColor(this Vector3 v) => Color.FromArgb((int)(255.99 * Math.Pow(v.X, 1/2.2)), (int)(255.99 * Math.Pow(v.Y, 1/2.2)), (int)(255.99 * Math.Pow(v.Z, 1/2.2)));

    public static Vector3 NextVector(this Random r) => (r.NextDouble(), r.NextDouble(), r.NextDouble());

    public static Vector3 NextUnitSphere(this Random r)
    {
      Vector3 p;
      do
      {
        p = 2*r.NextVector() - Vector3.One;
      } while (p.LengthSquared > 1);
      return p;
    }
  }

  class Program
  {
    static int seed = Environment.TickCount;
    static readonly ThreadLocal<Random> rand = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));    

    private static Vector3 Sky(in Ray r) => Vector3.Lerp((1, 1, 1), (.5, .7, 1), (r.Direction.Normalize().Y + 1) / 2);

    private static Vector3 NormalToColor(Vector3 normal) => (normal + Vector3.One) / 2;

    static Vector3 Color(in Ray r, IHitable world)
    {
      var hit = world.Hit(r, 0.001, double.MaxValue);
      if (hit == null)
      {
        return Sky(r);
      }

      var target = hit.Value.Position + hit.Value.Normal + rand.Value.NextUnitSphere();
      return 0.5*Color((hit.Value.Position, target - hit.Value.Position), world);
    }

    static void Main(string[] _)
    {
      const int nx = 200;
      const int ny = 100;
      const int ns = 100;


      var world = new HitableList
      {
        new Sphere((0, 0, -1), .5),
        new Sphere((0, -100.5, -1), 100)
      };

      var cam = new Camera();
      var pixelData = new byte[nx*ny*3];

      Parallel.For(0, ny*nx, pixel => 
      {
        var j = pixel/nx;
        var i = pixel % nx;

        var col = Vector3.Zero;
        for (var s = 0; s < ns; s++)
        {
          var u = (i + rand.Value.NextDouble()) / nx;
          var v = (j + rand.Value.NextDouble()) / ny;
          var r = cam.GetRay(u, v);
          col = col + Color(r, world);
        }

        var color = (col / ns).AsColor();
        pixelData[3*pixel + 2] = color.R;
        pixelData[3*pixel + 1] = color.G;
        pixelData[3*pixel + 0] = color.B;
      });

      var handle = GCHandle.Alloc(pixelData, GCHandleType.Pinned);
      var bmp = new Bitmap(nx, ny, nx*3, PixelFormat.Format24bppRgb, handle.AddrOfPinnedObject());
      bmp.Save("output.png");
      handle.Free();

      new Process { StartInfo = { FileName = "explorer", Arguments = "\"output.png\"" } }.Start();
    }
  }
}

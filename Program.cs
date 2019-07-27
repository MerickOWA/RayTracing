using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using RayTracing.Materials;

namespace RayTracing
{
  class Program
  {
    static int seed = Environment.TickCount;
    static readonly ThreadLocal<Random> rand = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));    

    private static Vector3 Sky(in Ray r) => Vector3.Lerp((1, 1, 1), (.5, .7, 1), (r.Direction.Y + 1) / 2);

    static Vector3 Color(in Ray ray, IHitable world, Random random, int depth)
    {
      var hit = world.Hit(ray, 0.001, double.MaxValue);
      if (hit == null)
      {
        return Sky(ray);
      }

      var scactter = hit.Value.Material.Scatter(ray, hit.Value, random);
      if (depth <= 0 || scactter == null)
      {
        return Vector3.Zero;
      }

      return scactter.Value.attenuation*Color(scactter.Value.scatterRay, world, random, depth-1);
    }

    static void Main(string[] _)
    {
      const int nx = 1000;
      const int ny = 500;
      const int ns = 100;
      const int maxDepth = 50;

      var world = new HitableList
      {
        new Sphere((0, 0, -1), .5, new Lambertian((0.1, 0.2, 0.5))),
        new Sphere((0, -100.5, -1), 100, new Lambertian((0.8, 0.8, 0.0))),
        new Sphere((1, 0, -1), .5, new Metal((0.8, 0.6, 0.2), 0.5)),
        new Sphere((-1, 0, -1), .5, new Dielectric(1.5)),
        new Sphere((-1, 0, -1), -.45, new Dielectric(1.5)),
      };

      var cam = new Camera();
      var pixelData = new byte[nx*ny*3];

      Parallel.For(0, ny*nx, pixel => 
      //for (var pixel = 0; pixel < ny * nx; pixel++)
      {
        var j = pixel / nx;
        var i = pixel % nx;
        var random = rand.Value;

        var col = Vector3.Zero;
        for (var s = 0; s < ns; s++)
        {
          var u = (i + random.NextDouble()) / nx;
          var v = (j + random.NextDouble()) / ny;
          var r = cam.GetRay(u, v);
          col += Color(r, world, random, maxDepth);
        }

        var color = (col / ns).AsColor();
        pixelData[3 * pixel + 2] = color.R;
        pixelData[3 * pixel + 1] = color.G;
        pixelData[3 * pixel + 0] = color.B;
      //}
      });

      var handle = GCHandle.Alloc(pixelData, GCHandleType.Pinned);
      var bmp = new Bitmap(nx, ny, nx*3, PixelFormat.Format24bppRgb, handle.AddrOfPinnedObject());
      bmp.Save("output.png");
      handle.Free();

      new Process { StartInfo = { FileName = "explorer", Arguments = "\"output.png\"" } }.Start();
    }
  }
}

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
    static readonly ThreadLocal<Random> randomFactory = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));    

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

    private static IHitable RandomScene(Random random)
    {
      var world = new HitableList
      {
        new Sphere((0, -1000, 0), 1000, new Lambertian((.5, .5, .5))),
        new Sphere((0, 1, 0), 1, new Dielectric(1.5)),
        new Sphere((-4, 1, 0), 1, new Lambertian((.4, .2, .1))),
        new Sphere((4, 1, 0), 1, new Metal((.7, .6, .5), 0))
      };

      for (var a = -11; a < 11; a++)
      {
        for (var b = -11; b < 11; b++)
        {
          Vector3 center = (a + .9 * random.NextDouble(), .2, b + .9 * random.NextDouble());
          if ((center - (4, .2, 0)).Length <= .9) continue;

          world.Add(new Sphere(center, .2, RandomMaterial(random)));
        }
      }

      return world;
    }

    private static IMaterial RandomMaterial(Random random)
    {
      var materialChoice = random.NextDouble();
      if (materialChoice < .8)
      {
        return new Lambertian(random.NextVector() * random.NextVector());
      }
      else if (materialChoice < .95)
      {
        return new Metal((random.NextVector() + Vector3.One) / 2, random.NextDouble() / 2);
      }
      else
      {
        return new Dielectric(1.5);
      }
    }

    static void Main(string[] _)
    {
      const int nx = 120*5;
      const int ny = 80*5;
      const int ns = 10;
      const int maxDepth = 50;

      //var world = new HitableList
      //{
      //  new Sphere((0, 0, -1), .5, new Lambertian((0.1, 0.2, 0.5))),
      //  new Sphere((0, -100.5, -1), 100, new Lambertian((0.8, 0.8, 0.0))),
      //  new Sphere((1, 0, -1), .5, new Metal((0.8, 0.6, 0.2), 0.5)),
      //  new Sphere((-1, 0, -1), .5, new Dielectric(1.5)),
      //  new Sphere((-1, 0, -1), -.45, new Dielectric(1.5)),
      //};
      var world = RandomScene(randomFactory.Value);

      Vector3 lookFrom = (13,2,3);
      Vector3 lookAt = (0, 0, 0);
      var distanceToFocus = 10;
      var aperture = 0.1;

      var cam = new Camera(lookFrom, lookAt, Vector3.Up, 20, (double)nx/ny, aperture, distanceToFocus);
      var pixelData = new byte[nx*ny*3];

      var stopwatch = new Stopwatch();
      for (var test = 0; test < 10; test++)
      {
        stopwatch.Restart();
        Parallel.For(0, ny * nx, pixel =>
        {
          var j = pixel / nx;
          var i = pixel % nx;
          var random = randomFactory.Value;

          var col = Vector3.Zero;
          for (var s = 0; s < ns; s++)
          {
            var u = (i + random.NextDouble()) / nx;
            var v = (j + random.NextDouble()) / ny;
            var r = cam.GetRay(u, v, random);
            col += Color(r, world, random, maxDepth);
          }

          var color = (col / ns).AsColor();
          pixelData[3 * pixel + 2] = color.R;
          pixelData[3 * pixel + 1] = color.G;
          pixelData[3 * pixel + 0] = color.B;
        });
        Console.WriteLine($"{test}: {stopwatch.Elapsed.TotalMilliseconds}ms");
      }

      var handle = GCHandle.Alloc(pixelData, GCHandleType.Pinned);
      var bmp = new Bitmap(nx, ny, nx*3, PixelFormat.Format24bppRgb, handle.AddrOfPinnedObject());
      bmp.Save("output.png");
      handle.Free();

      using (var process = new Process { StartInfo = { FileName = "explorer", Arguments = "\"output.png\"" } })
      {
        process.Start();
      }
    }
  }
}

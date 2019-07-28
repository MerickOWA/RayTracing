using System;
using System.Drawing;

namespace RayTracing
{
  internal static class Extensions
  {
    public static Color AsColor(this in Vector3 v) => Color.FromArgb((int)(255.99 * Math.Pow(v.X, 1 / 2.2)), (int)(255.99 * Math.Pow(v.Y, 1 / 2.2)), (int)(255.99 * Math.Pow(v.Z, 1 / 2.2)));

    public static Vector3 NextVector(this Random r) => (r.NextDouble(), r.NextDouble(), r.NextDouble());

    public static Vector3 NextUnitDisk(this Random r)
    {
      Vector3 p;
      do
      {
        p = 2 * (Vector3)(r.NextDouble(), r.NextDouble(), 0) - (1, 1, 0);
      } while (p.LengthSquared > 1);
      return p;
    }

    public static Vector3 NextUnitSphere(this Random r)
    {
      Vector3 p;
      do
      {
        p = 2 * r.NextVector() - Vector3.One;
      } while (p.LengthSquared > 1);
      return p;
    }
  }
}

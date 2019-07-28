using System;

namespace RayTracing
{
  public readonly struct Sphere : IHitable
  {
    public Sphere(Vector3 center, double radius, IMaterial material) => (Center, Radius, Material) = (center, radius, material);

    public readonly Vector3 Center;
    public readonly double Radius;
    public readonly IMaterial Material;

    public HitRecord? Hit(in Ray ray, double min, double max)
    {
      var oc = ray.Origin - Center;
      var b = -2 * oc.Dot(ray.Direction);
      var c = oc.LengthSquared - Radius * Radius;
      var discriminant = b * b - 4 * c;
      if (discriminant < 0)
      {
        return null;
      }

      var sqrtDiscriminant = Math.Sqrt(discriminant);
      var t = (b - sqrtDiscriminant) / 2;

      // Ensure t falls within min/max
      if (t > max)
      {
        // Way too far out, no point checking the other intersection
        // it would be even farther away
        return null;
      }
      else if (t < min)
      {
        // Try the second possible intersection
        t += sqrtDiscriminant;
        if (t < min)
        {
          // Can't get inside min
          return null;
        }
      }

      var position = t * ray;
      var normal = ((position - Center) / Radius).AssumeIsUnitVector();

      return (t, position, normal, Material);
    }
  }
}

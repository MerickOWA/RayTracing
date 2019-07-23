using System;

namespace RayTracing
{
  struct Sphere : IHitable
  {
    public Sphere(Vector3 center, double radius) => (Center, Radius) = (center, radius);

    Vector3 Center { get; }
    double Radius { get; }

    public HitRecord? Hit(Ray ray, double min, double max)
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
      var normal = (position - Center).Normalize();

      return (t, position, normal);
    }
  }
}

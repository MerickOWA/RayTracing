using System;

namespace RayTracing
{
  struct Sphere : Hitable
  {
    Sphere(Vector3 center, double radius) => (this.center, this.radius) = (center, radius);

    Vector3 center { get; }
    double radius { get; }

    public HitRecord? Hit(Ray ray, double min, double max)
    {
      var oc = ray.origin - center;
      var b = -2 * oc.Dot(ray.direction);
      var c = oc.lengthSquared - radius * radius;
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

      var position = t*ray;
      var normal = (position - center).Normalize();

      return (t, position, normal);
    }
  }    
}
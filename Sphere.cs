using System;

namespace RayTracing
{
  struct Sphere : Hitable
  {
    Sphere(Vector3 center, double radius) => (this.center, this.radius) = (center, radius);

    Vector3 center { get; }
    double radius { get; }

    (double t, Vector3 position, Vector3 normal)? hit(Ray ray, double min, double max)
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
      if (t > max)
      {
        return null;
      }

      //TODO finish min/max checks

      var position = t*ray;
      var normal = (position - center).Normalize();

      return (t, position, normal);
    }
  }    
}
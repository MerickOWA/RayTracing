using System;

namespace RayTracing
{
  public struct Ray
  {
    public Ray(Vector3 origin, Vector3 direction) => (this.origin, this.direction) = (origin, direction);

    public Vector3 origin { get; }
    public Vector3 direction { get; }

    public static Vector3 operator *(double a, Ray b)
    {
      return b.origin + a*b.direction;
    }

    public static Vector3 operator *(Ray a, double b)
    {
      return a.origin + a.direction*b;
    }
  }
}
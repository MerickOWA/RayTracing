using System;

namespace RayTracing
{
  public readonly struct Ray
  {
    public Ray(Vector3 origin, Vector3 direction) => (Origin, Direction) = (origin, direction);

    public readonly Vector3 Origin;
    public readonly UnitVector3 Direction;

    public static Vector3 operator *(double a, Ray b)
    {
      return b.Origin + a * b.Direction;
    }

    public static Vector3 operator *(Ray a, double b)
    {
      return a.Origin + a.Direction * b;
    }

    public static implicit operator Ray((Vector3 origin, Vector3 direction) tuple)
    {
      return new Ray(tuple.origin, tuple.direction);
    }
  }
}

using System;

namespace RayTracing
{
  public readonly struct Ray
  {
    public Ray(Vector3 origin, Vector3 direction) => (Origin, Direction) = (origin, direction);

    public Vector3 Origin { get; }
    public UnitVector3 Direction { get; }

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

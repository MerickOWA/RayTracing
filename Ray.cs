using System;

namespace RayTracing
{
  public struct Ray
  {
    public Ray(Vector3 origin, Vector3 direction) => (this.origin, this.direction) = (origin, direction);

    public Vector3 origin { get; }
    public Vector3 direction { get; }
  }
}
using System;

namespace RayTracing.Materials
{
  public readonly struct Metal : IMaterial
  {
    public Metal(in Vector3 albedo, double fuzz = 0) => (Albedo, Fuzz) = (albedo, fuzz);

    public Vector3 Albedo { get; }
    public double Fuzz { get; }

    public (Ray scatterRay, Vector3 attenuation)? Scatter(in Ray ray, in HitRecord hit, Random random)
    {
      var reflected = ray.Direction.Reflect(hit.Normal) + Fuzz*random.NextUnitSphere();
      if (reflected.Dot(hit.Normal) <= 0)
      {
        return null;
      }

      return ((hit.Position, reflected), Albedo);
    }
  }
}

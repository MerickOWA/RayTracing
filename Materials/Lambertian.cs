using System;

namespace RayTracing.Materials
{
  public readonly struct Lambertian : IMaterial
  {
    public Lambertian(in Vector3 albedo) => Albedo = albedo;

    public Vector3 Albedo { get; }

    public (Ray scatterRay, Vector3 attenuation)? Scatter(in Ray ray, in HitRecord hit, Random random)
    {
      var target = hit.Position + hit.Normal + random.NextUnitSphere();

      return ((hit.Position, target - hit.Position), Albedo);
    }
  }
}

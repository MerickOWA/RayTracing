using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracing.Materials
{
  public struct Dielectric : IMaterial
  {
    public Dielectric(double refractiveIndex) => RefractiveIndex = refractiveIndex;

    public double RefractiveIndex { get; }

    public (Ray scatterRay, Vector3 attenuation)? Scatter(in Ray ray, in HitRecord hit, Random random)
    {
      var angleOfIncidenceCosine = ray.Direction.Dot(hit.Normal);
      var isExiting = angleOfIncidenceCosine > 0;
      var outWardNormal = isExiting ? -hit.Normal : hit.Normal;
      var niOverNt = isExiting ? RefractiveIndex : 1/RefractiveIndex;
      var cosine = isExiting ? RefractiveIndex * angleOfIncidenceCosine : -angleOfIncidenceCosine;

      var refracted = ray.Direction.Refract(outWardNormal, niOverNt);
      if (refracted != null)
      {
        if (random.NextDouble() >= Schlick(cosine, RefractiveIndex))
        {
          return ((hit.Position, refracted.Value), Vector3.One);
        }
      }

      return ((hit.Position, ray.Direction.Reflect(hit.Normal)), Vector3.One);
    }

    private static double Schlick(double cosine, double refractiveIndex)
    {
      var r0 = (1 - refractiveIndex) / (1 + refractiveIndex);
      var r0Squared = r0 * r0;

      return r0Squared + (1 - r0Squared) * Math.Pow(1 - cosine, 5);
    }
  }
}

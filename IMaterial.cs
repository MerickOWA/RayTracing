using System;

namespace RayTracing
{
  public interface IMaterial
  {
    (Ray scatterRay, Vector3 attenuation)? Scatter(in Ray ray, in HitRecord hit, Random random);
  }
}

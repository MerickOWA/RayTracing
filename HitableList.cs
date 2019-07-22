using System.Collections.Generic;

namespace RayTracing
{
  public class HitableList : List<Hitable>, Hitable
  {
    public HitRecord? Hit(Ray ray, double min, double max)
    {
      foreach (var item in this)
      {
          var hit = item.Hit(ray, min, max);
      }
    }
  }
}
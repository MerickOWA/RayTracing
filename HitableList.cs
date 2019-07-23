using System.Linq;
using System.Collections.Generic;

namespace RayTracing
{
  public class HitableList : List<IHitable>, IHitable
  {
    public HitRecord? Hit(Ray ray, double min, double max)
    {
      HitRecord? closest = null;
      foreach (var item in this)
      {
        var hit = item.Hit(ray, min, max);
        if (hit != null && (closest == null || hit.Value.Distance < closest.Value.Distance))
        {
          closest = hit;
          max = hit.Value.Distance;
        }
      }
      return closest;
    }
  }
}

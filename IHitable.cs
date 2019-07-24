namespace RayTracing
{
  public interface IHitable
  {
    HitRecord? Hit(in Ray ray, double min, double max);
  }
}

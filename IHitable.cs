namespace RayTracing
{
  public interface IHitable
  {
    HitRecord? Hit(Ray ray, double min, double max);
  }
}

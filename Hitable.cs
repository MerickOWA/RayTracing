namespace RayTracing
{
  public interface Hitable
  {
    HitRecord? Hit(Ray ray, double min, double max);
  }
}
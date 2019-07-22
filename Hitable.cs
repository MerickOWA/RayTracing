namespace RayTracing
{
  interface Hitable
  {
    (double t, Vector3 position, Vector3 normal)? hit(Ray ray, double min, double max);
  }
}
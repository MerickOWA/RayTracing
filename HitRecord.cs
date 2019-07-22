namespace RayTracing
{
  public struct HitRecord
  {
    public HitRecord(double distance, Vector3 position, Vector3 normal) => (this.distance, this.position, this.normal) = (distance, position, normal);

    public double distance { get; }
    public Vector3 position { get; }
    public Vector3 normal { get; }

    public static implicit operator HitRecord((double distance, Vector3 position, Vector3 normal) tuple)
    {
      return new HitRecord(tuple.distance, tuple.position, tuple.normal);
    }
  }
}
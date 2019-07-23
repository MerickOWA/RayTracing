namespace RayTracing
{
  public struct HitRecord
  {
    public HitRecord(double distance, Vector3 position, Vector3 normal) => (Distance, Position, Normal) = (distance, position, normal);

    public double Distance { get; }
    public Vector3 Position { get; }
    public Vector3 Normal { get; }

    public static implicit operator HitRecord((double distance, Vector3 position, Vector3 normal) tuple)
    {
      return new HitRecord(tuple.distance, tuple.position, tuple.normal);
    }
  }
}

namespace RayTracing
{
  public struct HitRecord
  {
    public HitRecord(double distance, Vector3 position, UnitVector3 normal, IMaterial material) => (Distance, Position, Normal, Material) = (distance, position, normal, material);

    public double Distance { get; }
    public Vector3 Position { get; }
    public UnitVector3 Normal { get; }
    public IMaterial Material { get; }

    public static implicit operator HitRecord((double distance, Vector3 position, UnitVector3 normal, IMaterial material) tuple)
    {
      return new HitRecord(tuple.distance, tuple.position, tuple.normal, tuple.material);
    }
  }
}

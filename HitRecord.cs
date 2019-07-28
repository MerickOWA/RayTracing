namespace RayTracing
{
  public struct HitRecord
  {
    public HitRecord(double distance, Vector3 position, UnitVector3 normal, IMaterial material) => (Distance, Position, Normal, Material) = (distance, position, normal, material);

    public readonly Vector3 Position;
    public readonly UnitVector3 Normal;
    public readonly IMaterial Material;
    public readonly double Distance;

    public static implicit operator HitRecord((double distance, Vector3 position, UnitVector3 normal, IMaterial material) tuple)
    {
      return new HitRecord(tuple.distance, tuple.position, tuple.normal, tuple.material);
    }
  }
}

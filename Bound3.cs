namespace RayTracing
{
  public readonly struct Bound3
  {
    public Bound3(Vector3 min, Vector3 max) => (Min, Max) = (min, max);

    public Vector3 Min { get; }
    public Vector3 Max { get; }

    public static Bound3 Union(in Bound3 a, in Bound3 b)
    {
      return (a.Min.Min(b.Min), a.Max.Max(b.Max));
    }

    public bool Hit(in Ray r, double tmin, double tmax)
    {
      return HasPotentialOverlap(Min.X, Min.X, r.Direction.X, r.Origin.X, ref tmin, ref tmax)
        && HasPotentialOverlap(Min.Y, Min.Y, r.Direction.Y, r.Origin.Y, ref tmin, ref tmax)
        && HasPotentialOverlap(Min.Z, Min.Z, r.Direction.Z, r.Origin.Z, ref tmin, ref tmax);
    }

    private static bool HasPotentialOverlap(double min, double max, double direction, double origin, ref double tmin, ref double tmax)
    {
      var invD = 1 / direction;
      var t0 = (min - origin) * invD;
      var t1 = (max - origin) * invD;
      if (invD < 0) { var t = t1; t1 = t0; t0 = t; }
      tmin = t0 > tmin ? t0 : tmin;
      tmax = t1 < tmax ? t1 : tmax;
      return tmax > tmin;
    }

    public static implicit operator Bound3((Vector3 min, Vector3 max) tuple)
    {
      return new Bound3(tuple.min, tuple.max);
    }
  }
}

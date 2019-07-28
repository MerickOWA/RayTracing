using System;
using System.Diagnostics;

namespace RayTracing
{
  public readonly struct UnitVector3
  {
    public UnitVector3(in Vector3 v) => (X, Y, Z) = v / v.Length;
    private UnitVector3(double x, double y, double z) => (X, Y, Z) = (x, y, z);

    public double X { get; }
    public double Y { get; }
    public double Z { get; }

    public static UnitVector3 operator -(in UnitVector3 a) => new UnitVector3(-a.X, -a.Y, -a.Z);

    public static Vector3 operator +(in UnitVector3 a, in Vector3 b) => new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    public static Vector3 operator +(in Vector3 a, in UnitVector3 b) => new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

    public static Vector3 operator -(in UnitVector3 a, in Vector3 b) => new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    public static Vector3 operator -(in Vector3 a, in UnitVector3 b) => new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

    public static Vector3 operator *(in UnitVector3 a, in Vector3 b) => new Vector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
    public static Vector3 operator *(in Vector3 a, in UnitVector3 b) => new Vector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);

    public static Vector3 operator /(in UnitVector3 a, in Vector3 b) => new Vector3(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
    public static Vector3 operator /(in Vector3 a, in UnitVector3 b) => new Vector3(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

    public static Vector3 operator *(in UnitVector3 a, double b) => new Vector3(a.X * b, a.Y * b, a.Z * b);

    public static Vector3 operator *(double a, in UnitVector3 b) => new Vector3(a * b.X, a * b.Y, a * b.Z);

    public static Vector3 operator /(in UnitVector3 a, double b) => new Vector3(a.X / b, a.Y / b, a.Z / b);

    public static Vector3 operator /(double a, in UnitVector3 b) => new Vector3(a / b.X, a / b.Y, a / b.Z);

    public static double Dot(in UnitVector3 a, in UnitVector3 b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;
    public static double Dot(in UnitVector3 a, in Vector3 b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;
    public static double Dot(in Vector3 a, in UnitVector3 b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;

    public double Dot(in Vector3 v) => Dot(this, v);
    public double Dot(in UnitVector3 v) => Dot(this, v);

    public static UnitVector3 Cross(in UnitVector3 a, in UnitVector3 b) => new Vector3(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);
    public static Vector3 Cross(in UnitVector3 a, in Vector3 b) => new Vector3(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);
    public static Vector3 Cross(in Vector3 a, in UnitVector3 b) => new Vector3(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);

    public Vector3 Cross(in Vector3 v) => Cross(this, v);
    public UnitVector3 Cross(in UnitVector3 v) => Cross(this, v);

    public static Vector3 Lerp(in UnitVector3 a, in UnitVector3 b, double t) => a * (1 - t) + b * t;

    public Vector3 Reflect(in UnitVector3 normal) => this - 2 * Dot(normal) * normal;

    public Vector3? Refract(in UnitVector3 normal, double niOverNt)
    {
      var dt = Dot(normal);
      var discriminant = 1 - niOverNt * niOverNt * (1 - dt * dt);
      if (discriminant <= 0)
      {
        return null;
      }

      return niOverNt * (this - normal * dt) - normal * Math.Sqrt(discriminant);
    }

    public static UnitVector3 AssumeIsUnitVector(Vector3 v)
    {
      Debug.Assert(Math.Abs(v.LengthSquared - 1) < 0.0000001);
      return new UnitVector3(v.X, v.Y, v.Z);
    }

    public static bool operator ==(in UnitVector3 a, in UnitVector3 b) => a.X == b.X && a.Y == b.Y && a.Z == b.Z;

    public static bool operator !=(in UnitVector3 a, in UnitVector3 b) => a.X != b.X || a.Y != b.Y || a.Z != b.Z;

    public override bool Equals(object obj) => obj != null && obj is UnitVector3 v && this == v;

    public override int GetHashCode() => CombineHashCodes(CombineHashCodes(X.GetHashCode(), Y.GetHashCode()), Z.GetHashCode());

    private static int CombineHashCodes(int a, int b) => (a << 5 + a) ^ b;

    public override string ToString() => $"{X}, {Y}, {Z}";

    public void Deconstruct(out double x, out double y, out double z) => (x, y, z) = (X, Y, Z);

    public static implicit operator UnitVector3(in Vector3 v) => new UnitVector3(v);
  }
}

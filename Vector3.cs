using System;

namespace RayTracing
{
  public readonly struct Vector3
  {
    public Vector3(double x, double y, double z) => (X, Y, Z) = (x, y, z);

    public readonly double X;
    public readonly double Y;
    public readonly double Z;
    public double LengthSquared => Dot(this);
    public double Length => Math.Sqrt(LengthSquared);

    public static Vector3 operator -(in Vector3 a) => new Vector3(-a.X, -a.Y, -a.Z);
    public static Vector3 operator +(in Vector3 a, in Vector3 b) => new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    public static Vector3 operator -(in Vector3 a, in Vector3 b) => new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    public static Vector3 operator *(in Vector3 a, in Vector3 b) => new Vector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
    public static Vector3 operator /(in Vector3 a, Vector3 b) => new Vector3(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
    public static Vector3 operator *(in Vector3 a, double b) => new Vector3(a.X * b, a.Y * b, a.Z * b);
    public static Vector3 operator *(double a, in Vector3 b) => new Vector3(a * b.X, a * b.Y, a * b.Z);
    public static Vector3 operator /(in Vector3 a, double b) => new Vector3(a.X / b, a.Y / b, a.Z / b);
    public static Vector3 operator /(double a, in Vector3 b) => new Vector3(a / b.X, a / b.Y, a / b.Z);

    public static double Dot(in Vector3 a, in Vector3 b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;
    public static double Dot(in Vector3 a, in UnitVector3 b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;
    public static double Dot(in UnitVector3 a, in Vector3 b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;
    public double Dot(in Vector3 v) => Dot(this, v);
    public double Dot(in UnitVector3 v) => Dot(this, v);

    public static Vector3 Cross(in Vector3 a, in Vector3 b) => new Vector3(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);
    public static Vector3 Cross(in Vector3 a, in UnitVector3 b) => new Vector3(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);
    public static Vector3 Cross(in UnitVector3 a, in Vector3 b) => new Vector3(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);

    public Vector3 Cross(in Vector3 v) => Cross(this, v);
    public Vector3 Cross(in UnitVector3 v) => Cross(this, v);

    public static Vector3 Lerp(in Vector3 a, in Vector3 b, double t) => a * (1 - t) + b * t;

    public static Vector3 Min(in Vector3 a, in Vector3 b) => (Math.Min(a.X, b.X), Math.Min(a.Y, b.Y), Math.Min(a.Z, b.Z));
    public static Vector3 Max(in Vector3 a, in Vector3 b) => (Math.Max(a.X, b.X), Math.Max(a.Y, b.Y), Math.Max(a.Z, b.Z));

    public Vector3 Min(in Vector3 v) => Min(this, v);
    public Vector3 Max(in Vector3 v) => Max(this, v);

    public UnitVector3 Normalize() => this;

    public UnitVector3 AssumeIsUnitVector() => UnitVector3.AssumeIsUnitVector(this);

    public static bool operator ==(in Vector3 a, in Vector3 b) => a.X == b.X && a.Y == b.Y && a.Z == b.Z;

    public static bool operator !=(in Vector3 a, in Vector3 b) => a.X != b.X || a.Y != b.Y || a.Z != b.Z;

    public override bool Equals(object obj) => obj != null && obj is Vector3 v && this == v;

    public override int GetHashCode() => CombineHashCodes(CombineHashCodes(X.GetHashCode(), Y.GetHashCode()), Z.GetHashCode());

    private static int CombineHashCodes(int a, int b) => (a << 5 + a) ^ b;

    public override string ToString() => $"{X}, {Y}, {Z}";

    public void Deconstruct(out double x, out double y, out double z) => (x, y, z) = (X, Y, Z);

    public static implicit operator Vector3((double x, double y, double z) v)
    {
      return new Vector3(v.x, v.y, v.z);
    }

    public static readonly Vector3 Zero = (0, 0, 0);
    public static readonly Vector3 One = (1, 1, 1);
    public static readonly Vector3 Up = (0, 1, 0);
  }
}

using System;

namespace RayTracing
{
  public readonly struct Vector3
  {

    public Vector3(double x, double y, double z) => (X, Y, Z) = (x, y, z);
    public Vector3((double, double, double) v) => (X, Y, Z) = v;

    public double X { get; }
    public double Y { get; }
    public double Z { get; }
    public double LengthSquared => Dot(this);
    public double Length => Math.Sqrt(LengthSquared);

    public static Vector3 operator +(Vector3 a, Vector3 b) => new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

    public static Vector3 operator -(Vector3 a, Vector3 b) => new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

    public static Vector3 operator *(Vector3 a, Vector3 b) => new Vector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);

    public static Vector3 operator /(Vector3 a, Vector3 b) => new Vector3(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

    public static Vector3 operator *(Vector3 a, double b) => new Vector3(a.X * b, a.Y * b, a.Z * b);

    public static Vector3 operator *(double a, Vector3 b) => new Vector3(a * b.X, a * b.Y, a * b.Z);

    public static Vector3 operator /(Vector3 a, double b) => new Vector3(a.X / b, a.Y / b, a.Z / b);

    public static Vector3 operator /(double a, Vector3 b) => new Vector3(a / b.X, a / b.Y, a / b.Z);

    public static double Dot(Vector3 a, Vector3 b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;

    public double Dot(Vector3 v) => Dot(this, v);

    public static Vector3 Cross(Vector3 a, Vector3 b) => new Vector3(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);

    public Vector3 Cross(Vector3 v) => Cross(this, v);

    public static Vector3 Lerp(Vector3 a, Vector3 b, double t) => a * (1 - t) + b * t;

    public Vector3 Normalize() => this / Length;

    public static bool operator ==(Vector3 a, Vector3 b) => a.X == b.X && a.Y == b.Y && a.Z == b.Z;

    public static bool operator !=(Vector3 a, Vector3 b) => a.X != b.X || a.Y != b.Y || a.Z != b.Z;

    public override bool Equals(object obj) => obj != null && obj is Vector3 v && this == v;

    public override int GetHashCode() => CombineHashCodes(CombineHashCodes(X.GetHashCode(), Y.GetHashCode()), Z.GetHashCode());

    private static int CombineHashCodes(int a, int b) => (a << 5 + a) ^ b;

    public override string ToString() => $"{X}, {Y}, {Z}";

    public void Deconstruct(out double x, out double y, out double z) => (x, y, z) = (X, Y, Z);

    public static implicit operator Vector3((double, double, double) v)
    {
      return new Vector3(v);
    }

    public static readonly Vector3 Zero = new Vector3(0, 0, 0);
    public static readonly Vector3 One = new Vector3(1, 1, 1);
  }
}

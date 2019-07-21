using System;

namespace RayTracing
{
  public struct Vector3
  {

    public Vector3(double x, double y, double z) => (this.x, this.y, this.z) = (x, y, z);

    public double x { get; }
    public double y { get; }
    public double z { get; }

    public double LengthSquared => x*x + y*y + z*z;
    public double Length => Math.Sqrt(LengthSquared);

    public static Vector3 operator + (Vector3 a, Vector3 b) => new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);

    public static Vector3 operator - (Vector3 a, Vector3 b) => new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);

    public static Vector3 operator * (Vector3 a, Vector3 b) => new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);

    public static Vector3 operator / (Vector3 a, Vector3 b) => new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);

    public static Vector3 operator * (Vector3 a, double b) => new Vector3(a.x*b, a.y*b, a.z*b);

    public static Vector3 operator * (double a, Vector3 b) => new Vector3(a*b.x, a*b.y, a*b.z);

    public static Vector3 operator / (Vector3 a, double b) => new Vector3(a.x/b, a.y/b, a.z/b);

    public static Vector3 operator / (double a, Vector3 b) => new Vector3(a/b.x, a/b.y, a/b.z);

    public static double dot(Vector3 a, Vector3 b) => a.x*b.x + a.y*b.y + a.z*b.z;

    public static Vector3 cross(Vector3 a, Vector3 b) => new Vector3(a.y*b.z - a.z*b.y, a.z*b.x-a.x*b.z, a.x*b.y - a.y*b.x);

    public Vector3 normalize() => this / Length;

    public static bool operator ==(Vector3 a, Vector3 b) => a.x == b.x && a.y == b.y && a.z == b.z;

    public static bool operator !=(Vector3 a, Vector3 b) => a.x != b.x || a.y != b.y || a.z != b.z;

    public override bool Equals(object obj) => obj != null && obj is Vector3 v && this == v;

    public override int GetHashCode() => CombineHashCodes(CombineHashCodes(x.GetHashCode(), y.GetHashCode()), z.GetHashCode());

    private static int CombineHashCodes(int a, int b) => (a << 5 + a) ^ b;

    public override string ToString() => $"{x}, {y}, {z}";

    public void Deconstruct(out double x, out double y, out double z) => (x, y, z) = (this.x, this.y, this.z);
  }
} 
namespace RayTracing
{
  public readonly struct Camera
  {
    public Vector3 Origin => (0, 0, 0);
    public Vector3 UpperLeft => (-2, 1, -1);
    public Vector3 Horizontal => (4, 0, 0);
    public Vector3 Vertical => (0, 2, 0);

    public Ray GetRay(double u, double v) => (Origin, UpperLeft + u*Horizontal - v*Vertical - Origin);
  }
}

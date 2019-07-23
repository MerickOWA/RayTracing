namespace RayTracing
{
  public struct Camera
  {
    public Vector3 Origin => (0, 0, 0);
    public Vector3 LowerLeft => (-2, -1, -1);
    public Vector3 Horizontal => (4, 0, 0);
    public Vector3 Vertical => (0, 2, 0);

    public Ray GetRay(double u, double v) => (Origin, LowerLeft + u*Horizontal + v*Vertical - Origin);
  }
}
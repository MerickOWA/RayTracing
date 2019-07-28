using System;

namespace RayTracing
{
  public readonly struct Camera
  {
    public Camera(Vector3 lookFrom, Vector3 lookAt, UnitVector3 up, double degreesVerticalFieldOfView, double aspectRatio, double aperture, double focusDistance)
    {
      var theta = degreesVerticalFieldOfView * Math.PI / 180;
      var halfHeight = Math.Tan(theta / 2);
      var halfWidth = aspectRatio * halfHeight;

      LensRadius = aperture / 2;
      Origin = lookFrom;
      W = (lookAt - lookFrom).Normalize();
      U = W.Cross(up);
      V = U.Cross(W);
      UpperLeft = Origin - halfWidth * focusDistance * U + halfHeight * focusDistance * V + focusDistance * W;
      Horizontal = 2 * halfWidth * focusDistance * U;
      Vertical = 2 * halfHeight * focusDistance * V;
    }

    public Vector3 Origin { get; }
    public Vector3 UpperLeft { get; }
    public Vector3 Horizontal { get; }
    public Vector3 Vertical { get; }
    public double LensRadius { get; }
    public UnitVector3 U { get; }
    public UnitVector3 V { get; }
    public UnitVector3 W { get; }

    public Ray GetRay(double s, double t, Random random)
    {
      var rd = LensRadius * random.NextUnitDisk();
      var offset = U * rd.X + V * rd.Y;
      return (Origin + offset, UpperLeft + s * Horizontal - t * Vertical - Origin - offset);
    }
  }
}

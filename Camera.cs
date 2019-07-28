using System;

namespace RayTracing
{
  public readonly struct Camera
  {
    public Camera(Vector3 lookFrom, Vector3 lookAt, Vector3 up, double degreesVerticalFieldOfView, double aspectRatio)
    {
      var theta = degreesVerticalFieldOfView * Math.PI / 180;
      var halfHeight = Math.Tan(theta / 2);
      var halfWidth = aspectRatio * halfHeight;

      Origin = lookFrom;
      var w = (lookAt - lookFrom).Normalize();
      var u = w.Cross(up);
      var v = u.Cross(w);
      UpperLeft = Origin + -halfWidth*u + halfHeight*v + w;
      Horizontal = 2 * halfWidth * u;
      Vertical = 2 * halfHeight * v;
    }

    public Vector3 Origin { get; }
    public Vector3 UpperLeft { get; }
    public Vector3 Horizontal { get; }
    public Vector3 Vertical { get; }

    public Ray GetRay(double u, double v) => (Origin, UpperLeft + u*Horizontal - v*Vertical - Origin);
  }
}

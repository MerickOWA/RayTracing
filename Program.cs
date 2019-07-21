﻿using System;
using System.Drawing;

namespace RayTracing
{
  static class Extensions
  {
    public static Color AsColor(this Vector3 v) => Color.FromArgb((int)(255.99 * v.x), (int)(255.99 * v.y), (int)(255.99 * v.z));
  }

  class Program
  {
    static double? hitSphere(Vector3 center, double radius, Ray r)
    {
      var oc = r.origin - center;
      var a = r.direction.lengthSquared;
      var b = 2 * oc.Dot(r.direction);
      var c = oc.lengthSquared - radius * radius;
      var discriminant = b * b - 4 * a * c;
      if (discriminant < 0)
      {
        return null;
      }

      return (-b - Math.Sqrt(discriminant)) / 2*a;
    }

    private static Vector3 sky(Ray r) => Vector3.Lerp((1, 1, 1), (.5, .7, 1), (r.direction.Normalize().y + 1) / 2);

    static Vector3 color(Ray r) 
    {
      Vector3 circleCenter = (0,0,-1);
      var circleRadius = .5;

      var t = hitSphere(circleCenter, circleRadius, r);
      if (t == null)
      {
        return sky(r);
      }

      var normal = (t.Value*r - circleCenter).Normalize();

      return normal/2 + (.5,.5,.5);
    }

    static void Main(string[] args)
    {
      var nx = 200;
      var ny = 100;

      var bmp = new Bitmap(nx, ny, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

      var lowerLeftCorner = new Vector3(-2, -1, -1);
      var horizontal = new Vector3(4, 0, 0);
      var vertical = new Vector3(0, 2, 0);
      var origin = Vector3.zero;

      for (int j = 0; j < ny; j++)
      {
        for (int i = 0; i < nx; i++)
        {
          var u = (double)i / nx;
          var v = (double)j / ny;
          var r = new Ray(origin, lowerLeftCorner + u * horizontal + v * vertical);
          var col = color(r).AsColor();

          bmp.SetPixel(i, ny - 1 - j, col);
        }
      }

      bmp.Save("output.png");
    }
  }
}

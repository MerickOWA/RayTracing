using System.Drawing;

namespace RayTracing
{
  static class Extensions
  {
    public static Color AsColor(this Vector3 v) => Color.FromArgb((int)(255.99 * v.x), (int)(255.99 * v.y), (int)(255.99 * v.z));
  }

  class Program
  {
    static Vector3 color(Ray r) => Vector3.Lerp((1,1,1), (.5, .7, 1), (r.direction.Normalize().y + 1)/2);

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

          bmp.SetPixel(i, ny-1-j, col);
        }
      }

      bmp.Save("ouput.png");
    }
  }
}

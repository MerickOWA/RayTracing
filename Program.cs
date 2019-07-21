using System.Drawing;

namespace RayTracing
{
  class Program
    {
        static void Main(string[] args)
        {
            var nx = 200;
            var ny = 100;

            var bmp = new Bitmap(nx, ny, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

            for (int j = 0; j < ny ; j++)
            {
                for (int i = 0; i < nx; i++)
                {
                    var r = (float)i / nx;
                    var g = (float)j / ny;
                    var b = 0.2f;

                    var ir = (int)(255.99*r);
                    var ig = (int)(255.99*g);
                    var ib = (int)(255.99*b);

                    bmp.SetPixel(i, j, Color.FromArgb(ir, ig, ib));
                }
            }

            bmp.Save("ouput.png");
        }
    }
}

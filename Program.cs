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
                    var col = new Vector3((double)i / nx, (double)j / ny, 0.2);
                    var icol = 255.99*col;

                    bmp.SetPixel(i, j, Color.FromArgb((int)icol.x, (int)icol.y, (int)icol.z));
                }
            }

            bmp.Save("ouput.png");
        }
    }
}

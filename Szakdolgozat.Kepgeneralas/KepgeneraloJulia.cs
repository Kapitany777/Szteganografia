using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Szakdolgozat.Kepgeneralas
{
    public class KepgeneraloJulia : AbstractKepgeneralo
    {
        public Complex Pont1 { get; set; }
        public Complex Pont2 { get; set; }

        public KepgeneraloJulia(int pixelWidth, int pixelHeight)
            : base(pixelWidth, pixelHeight)
        {
            Pont1 = new Complex(-2.0, -2.0);
            Pont2 = new Complex(2.0, 2.0);
        }

        private int Julia(Complex z, Complex c)
        {
            int count = 0;
            
            for (int i = 0; i < 40; i++)
            {
                z = z * z + c;
                count++;

                if (z.Magnitude > 4)
                {
                    break;
                }
            }

            return count;
        }

        public override void Generalas()
        {
            double re = Pont1.Real;
            double rp = (Pont2.Real - Pont1.Real) / Bitmap.PixelWidth;
            double im = Pont1.Imaginary;
            double ip = (Pont2.Imaginary - Pont1.Imaginary) / Bitmap.PixelHeight;

            byte[] pixels = new byte[Bitmap.BackBufferStride * Bitmap.PixelHeight];

            byte redTmp;
            byte greenTmp;
            byte blueTmp;

            /*
               c = -0.35 + 0.65i
             * c = -0.12375 + 0.56508i
               c = -0.12 + .74 i
               c = -0.11 + 0.6557i
               c = -0.194 + 0.6557i
               c = -0.125 + 0i
             */

            Random rnd = new Random();
            Complex c = new Complex(-rnd.NextDouble(), rnd.NextDouble());

            for (int i = 0; i < Bitmap.PixelWidth; i++)
            {
                for (int j = 0; j < Bitmap.PixelHeight; j++)
                {
                    int count = Julia(new Complex(re, im), c);

                    if (count == 40)
                    {
                        redTmp = greenTmp = blueTmp = 0;
                    }
                    else
                    {
                        redTmp = (byte)(count * 6.0 * Red);
                        greenTmp = (byte)(count * 6.0 * Green);
                        blueTmp = (byte)(count * 6.0 * Blue);
                    }

                    int pos = (Bitmap.PixelWidth * j + i) * 3;
                    pixels[pos] = redTmp;
                    pixels[pos + 1] = greenTmp;
                    pixels[pos + 2] = blueTmp;

                    im += ip;
                }

                im = Pont1.Imaginary;
                re += rp;
            }

            Bitmap.WritePixels(new Int32Rect(0, 0, Bitmap.PixelWidth, Bitmap.PixelHeight), pixels, Bitmap.BackBufferStride, 0);
        }
    }
}

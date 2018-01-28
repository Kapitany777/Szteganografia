using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Szakdolgozat.Kepgeneralas
{
    public class KepgeneraloZaj : AbstractKepgeneralo
    {
        public KepgeneraloZaj(int pixelWidth, int pixelHeight)
            : base(pixelWidth, pixelHeight)
        {
        }

        public override void Generalas()
        {
            Random rnd = new Random();

            byte[] pixels = new byte[Bitmap.BackBufferStride * Bitmap.PixelHeight];
            byte pixel;

            int pos = 0;

            for (int i = 0; i < Bitmap.PixelWidth; i++)
            {
                for (int j = 0; j < Bitmap.PixelHeight; j++)
                {
                    pixel = (byte)rnd.Next(256);
                    
                    pixels[pos] = (byte)(pixel * Red * 255);
                    pixels[pos + 1] = (byte)(pixel * Green * 255);
                    pixels[pos + 2] = (byte)(pixel * Blue * 255);

                    pos += 3;
                }
            }

            Bitmap.WritePixels(new Int32Rect(0, 0, Bitmap.PixelWidth, Bitmap.PixelHeight), pixels, Bitmap.BackBufferStride, 0);
        }
    }
}

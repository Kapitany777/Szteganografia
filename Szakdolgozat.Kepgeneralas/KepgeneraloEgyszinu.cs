using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Szakdolgozat.Kepgeneralas
{
    public class KepgeneraloEgyszinu : AbstractKepgeneralo
    {
        public KepgeneraloEgyszinu(int pixelWidth, int pixelHeight)
            : base(pixelWidth, pixelHeight)
        {
        }
        
        public override void Generalas()
        {
            byte[] pixels = new byte[Bitmap.BackBufferStride * Bitmap.PixelHeight];

            int pos = 0;

            for (int i = 0; i < Bitmap.PixelWidth; i++)
            {
                for (int j = 0; j < Bitmap.PixelHeight; j++)
                {
                    pixels[pos] = (byte)(Red * 255);
                    pixels[pos + 1] = (byte)(Green * 255);
                    pixels[pos + 2] = (byte)(Blue * 255);

                    pos += 3;
                }
            }

            Bitmap.WritePixels(new Int32Rect(0, 0, Bitmap.PixelWidth, Bitmap.PixelHeight), pixels, Bitmap.BackBufferStride, 0);
        }
    }
}

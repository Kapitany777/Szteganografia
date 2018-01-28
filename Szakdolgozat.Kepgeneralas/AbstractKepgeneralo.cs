using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Szakdolgozat.Kepgeneralas
{
    public abstract class AbstractKepgeneralo
    {
        public WriteableBitmap Bitmap { get; protected set; }

        public double Red { get; set; }
        public double Green { get; set; }
        public double Blue { get; set; }

        public AbstractKepgeneralo(int pixelWidth, int pixelHeight)
        {
            Bitmap = new WriteableBitmap(pixelWidth, pixelHeight, 96, 96, PixelFormats.Rgb24, null);

            Red = 1.0;
            Green = 1.0;
            Blue = 1.0;
        }

        public abstract void Generalas();
    }
}

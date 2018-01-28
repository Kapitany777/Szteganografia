using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szakdolgozat.Kepgeneralas
{
    public static class FactoryKepgeneralo
    {
        public static AbstractKepgeneralo GetKepgeneralo(KepgeneraloAlgoritmus tipus, int pixelWidth, int pixelHeight)
        {
            AbstractKepgeneralo gen = null;

            switch (tipus)
            {
                case KepgeneraloAlgoritmus.Mandelbrot:
                    gen = new KepgeneraloMandelbrot(pixelWidth, pixelHeight);
                    break;

                case KepgeneraloAlgoritmus.Julia:
                    gen = new KepgeneraloJulia(pixelWidth, pixelHeight);
                    break;

                case KepgeneraloAlgoritmus.Egyszinu:
                    gen = new KepgeneraloEgyszinu(pixelWidth, pixelHeight);
                    break;

                case KepgeneraloAlgoritmus.Zaj:
                    gen = new KepgeneraloZaj(pixelWidth, pixelHeight);
                    break;
            }

            return gen;
        }
    }
}

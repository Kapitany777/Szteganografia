using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szteganografia
{
    public static class Konyvtarak
    {
        public static string AdatKonyvtar
        {
            get
            {
                return String.Format("{0}\\Szteganografia", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            }
        }

        public static void AdatKonyvtarLetrehozasa()
        {
            if (!Directory.Exists(Konyvtarak.AdatKonyvtar))
            {
                Directory.CreateDirectory(Konyvtarak.AdatKonyvtar);
            }
        }
    }
}

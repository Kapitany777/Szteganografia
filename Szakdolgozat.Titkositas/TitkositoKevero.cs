using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szakdolgozat.Titkositas
{
    public class TitkositoKevero : ITitkosito
    {
        public int Oszlopszam { get; private set; }
        
        public TitkositoKevero(int oszlopszam)
        {
            Oszlopszam = oszlopszam;
        }
        
        public byte[] Titkositas(byte[] titkositando)
        {
            Random rnd = new Random();
            
            int sorszam = titkositando.Length / Oszlopszam;

            if (titkositando.Length % Oszlopszam != 0)
            {
                sorszam++;
            }

            int meret = sorszam * Oszlopszam;

            byte[] titkositott = new byte[meret];

            int pos = 0;

            for (int i = 0; i < Oszlopszam && pos < meret; i++)
            {
                for (int j = 0; j < sorszam && pos < meret; j++)
                {
                    try
                    {
                        titkositott[pos] = titkositando[j * Oszlopszam + i];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        titkositott[pos] = (byte)rnd.Next(255);
                    }

                    pos++;
                }
            }

            return titkositott;
        }

        public byte[] Visszafejtes(byte[] visszafejtendo)
        {
            int sorszam = visszafejtendo.Length / Oszlopszam;

            if (visszafejtendo.Length % Oszlopszam != 0)
            {
                sorszam++;
            }

            int meret = sorszam * Oszlopszam;

            byte[] visszafejtett = new byte[meret];

            int pos = 0;

            for (int i = 0; i < sorszam && pos < meret; i++)
            {
                for (int j = 0; j < Oszlopszam && pos < meret; j++)
                {
                    visszafejtett[pos] = visszafejtendo[j * sorszam + i];
                    pos++;
                }
            }

            return visszafejtett;
        }
    }
}

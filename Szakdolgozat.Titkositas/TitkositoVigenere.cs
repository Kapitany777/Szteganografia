using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szakdolgozat.Titkositas
{
    public class TitkositoVigenere : ITitkosito
    {
        public byte[] Kulcs { get; private set; }

        public TitkositoVigenere(byte[] kulcs)
        {
            Kulcs = kulcs;
        }

        public byte[] Titkositas(byte[] titkositando)
        {
            byte[] titkositott = new byte[titkositando.Length];
            
            int pos = 0;

            for (int i = 0; i < titkositando.Length; i++)
            {
                titkositott[i] = (byte)(titkositando[i] + Kulcs[pos]);
                pos = (pos + 1) % Kulcs.Length;
            }

            return titkositott;
        }

        public byte[] Visszafejtes(byte[] visszafejtendo)
        {
            byte[] visszafejtett = new byte[visszafejtendo.Length];

            int pos = 0;

            for (int i = 0; i < visszafejtendo.Length; i++)
            {
                visszafejtett[i] = (byte)(visszafejtendo[i] - Kulcs[pos]);
                pos = (pos + 1) % Kulcs.Length;
            }

            return visszafejtett;
        }
    }
}

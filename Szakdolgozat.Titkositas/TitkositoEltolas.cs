using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szakdolgozat.Titkositas
{
    public class TitkositoEltolas : ITitkosito
    {
        public int Eltolas { get; private set; }
        
        public TitkositoEltolas(int eltolas)
        {
            Eltolas = eltolas;
        }
        
        public byte[] Titkositas(byte[] titkositando)
        {
            byte[] titkositott = new byte[titkositando.Length];

            for (int i = 0; i < titkositando.Length; i++)
            {
                titkositott[i] = (byte)(titkositando[i] + Eltolas);
            }

            return titkositott;
        }

        public byte[] Visszafejtes(byte[] visszafejtendo)
        {
            byte[] visszafejtett = new byte[visszafejtendo.Length];

            for (int i = 0; i < visszafejtendo.Length; i++)
            {
                visszafejtett[i] = (byte)(visszafejtendo[i] - Eltolas);
            }

            return visszafejtett;
        }
    }
}

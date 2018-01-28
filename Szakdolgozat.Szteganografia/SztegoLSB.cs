using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szakdolgozat.Titkositas;

namespace Szakdolgozat.Szteganografia
{
    public class SztegoLSB : AbstractSztego
    {
        public SztegoLSB()
        {
            Titkosito = TitkositoAlgoritmus.NincsTitkositas;
        }

        public SztegoLSB(TitkositoAlgoritmus titkosito)
        {
            Titkosito = titkosito;
        }

        public override void Elrejtes(byte[] elrejtendo, byte[] adat)
        {
            int pos = 0;

            // Az elrejtendő szöveg hosszának kódolása
            int hossz = elrejtendo.Length;

            for (int i = 0; i < sizeof(int) * 8; i++)
            {
                if ((hossz & 1) == 0)
                {
                    adat[pos++] &= 254;
                }
                else
                {
                    adat[pos++] |= 1;
                }

                hossz >>= 1;
            }

            // A titkosító algoritmus azonosítójának kódolása
            byte titk = (byte)Titkosito;

            for (int i = 0; i < sizeof(byte) * 8; i++)
            {
                if ((titk & 1) == 0)
                {
                    adat[pos++] &= 254;
                }
                else
                {
                    adat[pos++] |= 1;
                }

                titk >>= 1;
            }

            // A szöveg titkosítása
            if (Titkosito != TitkositoAlgoritmus.NincsTitkositas)
            {
                ITitkosito titkosito = FactoryTitkosito.GetTitkosito(Titkosito);

                elrejtendo = titkosito.Titkositas(elrejtendo);
            }

            // Az elrejtendő szöveg kódolása
            for (int i = 0; i < elrejtendo.Length; i++)
            {
                byte a = elrejtendo[i];

                for (int j = 0; j < 8; j++)
                {
                    if ((a & 1) == 0)
                    {
                        adat[pos++] &= 254;
                    }
                    else
                    {
                        adat[pos++] |= 1;
                    }

                    a >>= 1;
                }
            }
        }

        public override string Visszafejtes(byte[] adat)
        {
            int pos = 0;
            
            // Az elrejtett szöveg hosszának dekódolása
            int hossz = 0;

            for (int i = 0; i < sizeof(int) * 8; i++)
            {
                hossz |= ((adat[pos++] & 1) << i);
            }

            // A titkosító algoritmus azonosítójának dekódolása
            int titk = 0;

            for (int i = 0; i < sizeof(byte) * 8; i++)
            {
                titk |= ((adat[pos++] & 1) << i);
            }

            Titkosito = (TitkositoAlgoritmus)titk;

            // Az elrejtett szöveg dekódolása
            byte[] dekod = new byte[hossz];

            for (int i = 0; i < hossz; i++)
            {
                dekod[i] = 0;

                for (int j = 0; j < 8; j++)
                {
                    dekod[i] |= (byte)((adat[pos] & 1) << j);

                    pos++;
                }
            }

            if (Titkosito != TitkositoAlgoritmus.NincsTitkositas)
            {
                ITitkosito titkosito = FactoryTitkosito.GetTitkosito(Titkosito);

                dekod = titkosito.Visszafejtes(dekod);
            }

            return Encoding.UTF8.GetString(dekod, 0, dekod.Length);
        }
    }
}

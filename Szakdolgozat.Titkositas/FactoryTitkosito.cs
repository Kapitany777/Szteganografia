using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szakdolgozat.Titkositas
{
    public static class FactoryTitkosito
    {
        public static ITitkosito GetTitkosito(TitkositoAlgoritmus tipus)
        {
            ITitkosito titkosito = null;

            switch (tipus)
            {
                case TitkositoAlgoritmus.Caesar:
                    titkosito = new TitkositoCaesar();
                    break;

                case TitkositoAlgoritmus.Eltolas10:
                    titkosito = new TitkositoEltolas(10);
                    break;

                case TitkositoAlgoritmus.Vigenere:
                    titkosito = new TitkositoVigenere(new byte[] { 1, 2, 3, 4 });
                    break;
            }

            return titkosito;
        }
    }
}

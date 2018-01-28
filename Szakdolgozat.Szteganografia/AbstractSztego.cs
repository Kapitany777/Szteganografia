using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szakdolgozat.Titkositas;

namespace Szakdolgozat.Szteganografia
{
    public abstract class AbstractSztego
    {
        public TitkositoAlgoritmus Titkosito { get; set; }
        
        public abstract void Elrejtes(byte[] elrejtendo, byte[] adat);
        
        public void Elrejtes(string elrejtendoSzoveg, byte[] adat)
        {
            Elrejtes(Encoding.UTF8.GetBytes(elrejtendoSzoveg), adat);
        }

        public abstract string Visszafejtes(byte[] adat);
    }
}

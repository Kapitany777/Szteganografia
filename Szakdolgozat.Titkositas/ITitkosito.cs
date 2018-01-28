using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szakdolgozat.Titkositas
{
    /// <summary>
    /// A titkosító osztályok ős interfésze
    /// </summary>
    public interface ITitkosito
    {
        byte[] Titkositas(byte[] titkositando);
        byte[] Visszafejtes(byte[] visszafejtendo);
    }
}

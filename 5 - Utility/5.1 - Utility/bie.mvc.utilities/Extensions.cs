using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




public static class Extensions
{
    public static string ToStringSeparada<T>(this ICollection<T> Seq, string Separador)
    {
        if (Seq.Count == 0) return string.Empty;
        

        string strSaida = "";

        foreach (var item in Seq)
        {
            strSaida += Separador +  item.ToString()  ;
        }

        //remove o primeiro separador
        return strSaida.Remove(0, Separador.Length);

    }
}




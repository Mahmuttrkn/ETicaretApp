using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Infsrastructure.Operations
{
    public static class NameOperation
    {
        public static string CharacterRegulatory(string name)
           => name.Replace("\"","")
                .Replace("!", "")
                .Replace("'", "")
                .Replace("^", "")
                .Replace("+", "")
                .Replace("%", "")
                .Replace("&", "")
                .Replace("$", "")
                .Replace("/", "")
                .Replace("?", "")
                .Replace(".", "")
                .Replace("£", "")
                .Replace("_", "")
                .Replace("<", "")
                .Replace(">", "")
                .Replace(";", "")
                .Replace("=", "")
                .Replace("é", "")
                .Replace(".", "")
                .Replace("İ", "I")
                .Replace("Ş", "S")
                .Replace("Ö", "O")
                .Replace("ş", "s")
                .Replace("ö", "o")
                .Replace("Ğ", "G")
                .Replace("ğ", "g")
                .Replace("Ü", "U")
                .Replace("ü", "u")
                .Replace(":", "")
                .Replace("ı", "i")
                .Replace("Ç", "c")
                .Replace("ç", "c")
                .Replace("|", "");
        
    }
}

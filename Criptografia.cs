using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTcc
{
    public static class Criptografia
    {
        public static string criptografar(string pString)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(pString);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            return System.Text.Encoding.ASCII.GetString(data);
        }
    }
}

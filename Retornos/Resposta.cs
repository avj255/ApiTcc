using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTcc
{
    public class Resposta
    {

        public int codigoRetorno { get; set; }
        public string descricao { get; set; }

        public Resposta() { }

        public Resposta(int codigoRetorno, string descricao)
        {
            this.codigoRetorno = codigoRetorno;
            this.descricao = descricao;
        }
    }
}

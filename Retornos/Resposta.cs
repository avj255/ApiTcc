using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTcc
{
    public class Resposta
    {
        public enum CodigoResposta
        {
            sucesso = 1,
            erro = 2,
        }

        public CodigoResposta codigo { get; set; }
        public string descricao { get; set; }

        public Resposta() { }

        public Resposta(bool sucesso, string descricao) {
            if (sucesso)
                codigo = CodigoResposta.sucesso;
            else
                codigo = CodigoResposta.erro;

            this.descricao = descricao;
        }

        public Resposta(bool sucesso)
        {
            if (sucesso)
            {
                codigo = CodigoResposta.sucesso;
                descricao = "Sucesso";
            }
            else
            {
                codigo = CodigoResposta.erro;
                descricao = "Ocorreu um erro";
            }
        }
    }
}

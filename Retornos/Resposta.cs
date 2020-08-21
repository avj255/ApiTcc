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

        public CodigoResposta codigoProcessamento { get; set; }
        public int codigoRetorno { get; set; }
        public string descricao { get; set; }

        public Resposta() { }

        public Resposta(bool sucesso, int codigoRetorno, string descricao) {
            if (sucesso)
            {
                codigoProcessamento = CodigoResposta.sucesso;
                this.codigoRetorno = codigoRetorno; 
            }
            else
                codigoProcessamento = CodigoResposta.erro;

            this.descricao = descricao;
        }

        public Resposta(bool sucesso, int codigoRetorno)
        {
            if (sucesso)
            {
                codigoProcessamento = CodigoResposta.sucesso;
                this.codigoRetorno = codigoRetorno;
                descricao = "Sucesso";
            }
            else
            {
                codigoProcessamento = CodigoResposta.erro;
                descricao = "Ocorreu um erro";
            }
        }

        public Resposta(bool sucesso)
        {
            if (sucesso)
            {
                codigoProcessamento = CodigoResposta.sucesso;
                descricao = "Sucesso";
            }
            else
            {
                codigoProcessamento = CodigoResposta.erro;
                descricao = "Ocorreu um erro";
            }
        }
    }
}

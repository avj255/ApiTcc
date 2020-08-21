using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTcc.Retornos
{
    public class RespostaLogin : Resposta
    {
        public enum CodigoLogin
        {
            valido = 1,
            invalido = 2,
        }

        public string nome { get; set; }
        public int administrador { get; set; }

        public RespostaLogin(){}

        public RespostaLogin(bool sucessoProcessamento, bool sucessoLogin, string nome = null, int administrador = 0) : base (sucessoProcessamento)
        {
            if (sucessoLogin)
            {
                codigoRetorno = (int)CodigoLogin.valido;
                this.nome = nome;
                this.administrador = administrador;
            }
            else
            {
                codigoRetorno = (int)CodigoLogin.invalido;

                if (sucessoProcessamento)
                    descricao = "Usuário ou Senha Inválidos";
            }
        }
    }
}

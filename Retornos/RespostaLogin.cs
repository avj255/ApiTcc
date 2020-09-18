﻿using System;
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

        public string usuario { get; set; }
        public string nome { get; set; }
        public int administrador { get; set; }

        public RespostaLogin(){}

        public RespostaLogin(bool sucessoLogin, string usuario = null, string nome = null, int administrador = 0)
        {
            if (sucessoLogin)
            {
                codigoRetorno = (int)CodigoLogin.valido;
                this.usuario = usuario;
                this.nome = nome;
                this.administrador = administrador;
                descricao = "Sucesso";
            }
            else
            {
                codigoRetorno = (int)CodigoLogin.invalido;
                descricao = "Usuário ou Senha Inválidos";
            }
        }
    }
}

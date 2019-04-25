using System.Collections.Generic;

namespace bie.focuscrm.domain.Entities
{
    /// <summary>
    ///     Representa uma entidade de empresa do sistema
    /// </summary>
    public class Empresa
    {
        public int id_empresa { get; set; }

        public string Grupo { get; set; } //colocar obrigatório
    

        public string Nome { get; set; }

        public string CNPJ { get; set; }

        public int? QtdFiliais { get; set; }

        public int? QtdPrestadores { get; set; }

        public int? MetrosQuadrados { get; set; }

        public string Socio { get; set; }

        public string TelefoneSocio { get; set; }

        public string EmailSocio { get; set; }

        public string Site { get; set; }

        public int? NumeroCheckout { get; set; }

        public string Atividade { get; set; }

        public string InscEstadual { get; set; }

        public int? QtdFuncionarios { get; set; }

        public string Bancos { get; set; }

        public string Endividamento { get; set; }

        public string AcoesFiscais { get; set; }

        public string RegimeTributacao { get; set; }

        public string SistemaRetaguarda { get; set; }

        public string DiaRetiradaDocumentos { get; set; }

        public string RazaoSocial { get; set; }
        public string OBS { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string CEP { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }

        public string Bairro { get; set; }

        public string Telefone { get; set; } 

        /// <summary>
        ///     Armazenará a resposta da consulta ao webservice receita serializada. ( para auditoria )
        /// </summary>
        public string RespostaWS { get; set; }

        public int? QtdNFS { get; set; }


        public virtual ICollection<Filial> Filiais { get; set; }
        public virtual ICollection<ParametroAgendamento> ParametrosAgendamento { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
        //public virtual ICollection<AlertaAlcance> AlertasAlcances { get; set; }
    }
}
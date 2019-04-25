using System.Collections.Generic;

namespace bie.focuscrm.domain.Entities
{
    public class Filial
    {
        public int id_filial { get; set; }

        public int id_empresa { get; set; }
        public virtual Empresa Empresa { get; set; }


        public string CNPJ { get; set; }
        public string Nome { get; set; }
        public string RazaoSocial { get; set; }
        public string OBS { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string CEP { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string Telefone { get; set; }

        public string Bairro { get; set; }


        /// <summary>
        ///     Armazenará a resposta da consulta ao webservice receita serializada. ( para auditoria )
        /// </summary>
        public string RespostaWS { get; set; }


        public virtual ICollection<Atendimento> Atendimentos { get; set; }
    }
}
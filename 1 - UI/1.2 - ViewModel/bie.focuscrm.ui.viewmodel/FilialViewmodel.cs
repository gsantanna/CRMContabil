using System.ComponentModel.DataAnnotations;

namespace bie.focuscrm.ui.viewmodel
{
    public class FilialViewmodel
    {
        public int id_filial { get; set; }

        public int id_empresa { get; set; }
        //public Empresa Empresa { get; set; }

        [Required(ErrorMessage = "Informe o CNPJ")]
        [MaxLength(40, ErrorMessage = "O tamanho máximo do campo {0} é de {1} caracteres")]
        public string CNPJ { get; set; }

        [Required(ErrorMessage = "Informe o Nome")]
        [MaxLength(200, ErrorMessage = "O tamanho máximo do campo {0} é de {1} caracteres")]
        public string Nome { get; set; }

        [Display(Name ="Razão Social")]
        [MaxLength(200, ErrorMessage = "O tamanho máximo do campo {0} é de {1} caracteres")]
        public string RazaoSocial { get; set; }

        [Display(Name = "Observações")]
        [MaxLength(300, ErrorMessage = "O tamanho máximo do campo {0} é de {1} caracteres")]
        public string OBS { get; set; }


        [MaxLength(300, ErrorMessage = "O tamanho máximo do campo {0} é de {1} caracteres")]
        public string Logradouro { get; set; }

        [MaxLength(20, ErrorMessage = "O tamanho máximo do campo {0} é de {1} caracteres")]
        public string Numero { get; set; }

        [MaxLength(100, ErrorMessage = "O tamanho máximo do campo {0} é de {1} caracteres")]
        public string Complemento { get; set; }

        [MaxLength(12, ErrorMessage = "O tamanho máximo do campo {0} é de {1} caracteres")]
        public string CEP { get; set; }

        [MaxLength(100, ErrorMessage = "O tamanho máximo do campo {0} é de {1} caracteres")]
        public string Cidade { get; set; }

        [MaxLength(2, ErrorMessage = "O tamanho máximo do campo {0} é de {1} caracteres")]
        public string UF { get; set; }

        [MaxLength(100, ErrorMessage = "O tamanho máximo do campo {0} é de {1} caracteres")]
        public string Bairro { get; set; }

        [MaxLength(25, ErrorMessage = "O tamanho máximo do campo {0} é de {1} caracteres")]
        public string Telefone { get; set; }


        /// <summary>
        ///     Armazenará a resposta da consulta ao webservice receita serializada. ( para auditoria )
        /// </summary>
        public string RespostaWS { get; set; }
    }
}

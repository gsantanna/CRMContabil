using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using bie.focuscrm.domain.Entities;

namespace bie.focuscrm.ui.viewmodel
{
    public class EmpresaViewmodel
    {
        public int id_empresa { get; set; }

        [Required(ErrorMessage = "Informe o Grupo")]
        [MaxLength(200, ErrorMessage = "O tamanho máximo do campo {0} é de {1} caracteres")]
        public string Grupo { get; set; }

        [Required(ErrorMessage ="Informe o Nome")]
        [MaxLength(255,ErrorMessage = "O tamanho máximo do campo {0} é de {1} caracteres")]
        public string Nome { get; set; }

        [MaxLength(255, ErrorMessage = "O tamanho máximo do campo {0} é de {1} caracteres")]
        [Required(ErrorMessage = "Informe a Razão Social")]
        public string RazaoSocial { get; set; }

        [Required(ErrorMessage = "Informe o CNPJ")]
        [MaxLength(40, ErrorMessage = "O tamanho máximo do campo {0} é de {1} caracteres")]
        public string CNPJ { get; set; }

        [Display(Name ="Quantidade de Filiais")]
        public int QtdFiliais { get; set; }

        [Display(Name = "Quantidade de Prestadores")]
        public int QtdPrestadores { get; set; }

        [Display(Name = "Metros Quadrados")]
        public int MetrosQuadrados { get; set; }


        [Display(Name = "Sócio")]
        public string Socio { get; set; }

        [Display(Name = "Telefone do Sócio")]
        public string TelefoneSocio { get; set; }

        [MaxLength(255, ErrorMessage = "O tamanho máximo do campo {0} é de {1} caracteres")]
        [Display(Name = "e-mail do Sócio")]
        public string EmailSocio { get; set; }

        [MaxLength(255, ErrorMessage = "O tamanho máximo do campo {0} é de {1} caracteres")]
        public string Site { get; set; }

        
        [Display(Name = "Número de Checkout")]
        public int NumeroCheckout { get; set; }

        [MaxLength(400, ErrorMessage = "O tamanho máximo do campo {0} é de {1} caracteres")]
        public string Atividade { get; set; }

        [MaxLength(10, ErrorMessage = "O tamanho máximo do campo {0} é de {1} caracteres")]
        [Display(Name = "Inscrição Estadual")]
        public string InscEstadual { get; set; }

        [Display(Name = "Quantidade de Funcionários")]
        public int QtdFuncionarios { get; set; }

        [MaxLength(40, ErrorMessage = "O tamanho máximo do campo {0} é de {1} caracteres")]
        public string Bancos { get; set; }

        [MaxLength(40, ErrorMessage = "O tamanho máximo do campo {0} é de {1} caracteres")]
        public string Endividamento { get; set; }

        [Display(Name = "Ações Fiscais")]
        [MaxLength(255, ErrorMessage = "O tamanho máximo do campo {0} é de {1} caracteres")]
        public string AcoesFiscais { get; set; }

        [Display(Name = "Regime de Tributação")]
        public string RegimeTributacao { get; set; }

        [Display(Name = "Sistema Retaguarda")]
        [MaxLength(100, ErrorMessage = "O tamanho máximo do campo {0} é de {1} caracteres")]
        public string SistemaRetaguarda { get; set; }

        [Display(Name = "Dia de Retirada dos Documentos")]
        [MaxLength(30, ErrorMessage = "O tamanho máximo do campo {0} é de {1} caracteres")]
        public string DiaRetiradaDocumentos { get; set; }

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

        [Display(Name = "Observações")]
        public string OBS { get; set; }

        
        [Display(Name = "Quantidade de NFS")]
        public int QtdNFS { get; set; }


        /// <summary>
        ///     Armazenará a resposta da consulta ao webservice receita serializada. ( para auditoria )
        /// </summary>
        public string RespostaWS { get; set; }


        public ICollection<ParametroAgendamento> ParametrosAgendamento { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }
        public string[] FuncionariosEmpresa { get; set; }


    }
}
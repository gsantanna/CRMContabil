using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using bie.focuscrm.domain.Enums;

namespace bie.focuscrm.ui.viewmodel
{
    public class UsuarioViewmodel
    {
        public UsuarioViewmodel()
        {
            Roles = new List<string>();
        }
        //string para sincronizar com o login 
        public string id_usuario { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "O limite para o campo {0} é  de {1} caracteres")]
        [MinLength(5, ErrorMessage = "Favor preencher no mínimo {1} caracteres")]
        public string Nome { get; set; }

        public bool Ativo { get; set; }



        [Phone(ErrorMessage = "Favor preencher um telefone válido")]
        public string Telefone { get; set; }

        [Display(Name = "Celular")]
        [Phone(ErrorMessage = "Favor preencher um telefone válido")]
        public string Telefone2 { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Favor preencher um e-mail válido")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }



        [Display(Name = "Empresa")]
        public int? id_empresa { get; set; }

        public string EmpresaDesc => !id_empresa.HasValue ? "Não associado" : NomeEmpresa;
        public string NomeEmpresa { get; set; }




        public List<string> Roles { get; set; }

        public TipoUsuario Tipo
        {
            get
            {
                if (Roles.Count == 0) return TipoUsuario.Cliente;

                if (Roles.Contains("Superadmin"))
                {
                    return TipoUsuario.Superadmin;
                }
                else if (Roles.Contains("Administrador"))
                {
                    return TipoUsuario.Administrador;
                }
                else if (Roles.Contains("Focus"))
                {
                    return TipoUsuario.Focus;
                }
                else if (Roles.Contains("Setor"))
                {
                    return TipoUsuario.Setor;
                }
                else
                {
                    return TipoUsuario.Cliente;
                }
            }
        }

        public string TipoDesc => Tipo.ToString();


        #region "CRIAR E EDITAR"

        [Required]
        [Display(Name = "Tipo de Usuário")]
        public TipoUsuario Funcao { get; set; }

        [Display(Name ="Senha")]
        [StringLength(100, ErrorMessage = "A {0} deve ter no mínimo {2} caracteres.", MinimumLength = 6)]
        public string Senha { get; set; }

        [Display(Name = "Confirmar Senha")]
        [Compare("Senha", ErrorMessage ="As senhas não são iguais")]
        public string ConfirmarSenha { get; set; }
        

        #endregion











    }
}
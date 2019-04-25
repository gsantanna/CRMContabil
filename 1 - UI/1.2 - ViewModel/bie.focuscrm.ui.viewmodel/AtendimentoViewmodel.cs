using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using bie.focuscrm.domain.Enums;


namespace bie.focuscrm.ui.viewmodel
{
    public class AtendimentoViewmodel
    {
        public AtendimentoViewmodel()
        {
            UsuariosFocusSelecionados = new List<string>();
            UsuariosClienteSelecionados = new List<string>();
        }
        public int id_atendimento { get; set; }

        [Display(Name = "Filial")]
        [Required(ErrorMessage = "Favor selecionar uma filial válida")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Favor selecionar uma filial válida")]
        public int id_filial { get; set; }


        public List<string> UsuariosFocusSelecionados { get; set; }

        public List<string> UsuariosClienteSelecionados { get; set; }



        [Required(ErrorMessage = "Favor preencher o assunto da reunião")]
        public string Assunto { get; set; }


        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "Favor inserir uma data válida")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data e hora")]
        public DateTime DataAgendada { get; set; }

        [Required(ErrorMessage = "Favor selecionar um setor")]
        [Display(Name = "Setor")]
        public int id_setor { get; set; }

        //Propriedades materializadas
        //Estas propriedades são mapeadas lá no Automapper/AutomapConfig.cs

        [Display(Name = "Empresa")]
        [Required]
        public int id_empresa { get; set; }

        public string NomeEmpresa { get; set; }
        public string NomeSetor { get; set; }



        public string FuncionariosFocus { get; set; }
        public string FuncionariosCliente { get; set; }


        public ATAViewmodel Ata { get; set; }


        


    }
}



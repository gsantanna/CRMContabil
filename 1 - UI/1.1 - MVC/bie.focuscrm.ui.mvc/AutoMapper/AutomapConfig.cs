using System.Collections.Generic;
using AutoMapper;
using bie.focuscrm.domain.Entities;
using bie.focuscrm.ui.viewmodel;
using System.Linq;
using bie.mvc.utilities;

namespace bie.focuscrm.ui.mvc.AutoMapper
{
    public static class AutomapConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {


                #region ALERTA 


                cfg.CreateMap<Alerta, AlertaViewmodel>();
                cfg.CreateMap<AlertaViewmodel, Alerta>();

                cfg.CreateMap<AlertaAlcance, AlertaAlcanceViewmodel>();
                cfg.CreateMap<AlertaAlcanceViewmodel, AlertaAlcance>();

                #endregion


                #region EMPRESA 

                cfg.CreateMap<Empresa, EmpresaViewmodel>();
                cfg.CreateMap<EmpresaViewmodel, Empresa>();


                cfg.CreateMap<Empresa, EmpresaGridViewmodel>();
                cfg.CreateMap<EmpresaGridViewmodel, Empresa>();

                cfg.CreateMap<Filial, FilialViewmodel>();
                cfg.CreateMap<FilialViewmodel, Filial>();


                #endregion

                #region USUARIO

                cfg.CreateMap<Usuario, UsuarioViewmodel>()
                .ForMember(dest => dest.NomeEmpresa, map => map.MapFrom(orig => orig.Empresa.Nome));

                cfg.CreateMap<UsuarioViewmodel, Usuario>();

                #endregion

                #region PESQUISA

                cfg.CreateMap<Pesquisa, PesquisaViewmodel>();
                cfg.CreateMap<PesquisaViewmodel, Pesquisa>();

                cfg.CreateMap<QuestaoPesquisa, QuestaoPesquisaViewmodel>();
                cfg.CreateMap<QuestaoPesquisaViewmodel, QuestaoPesquisa>();

                cfg.CreateMap<RespostaPesquisa, RespostaPesquisaViewmodel>();
                cfg.CreateMap<RespostaPesquisaViewmodel, RespostaPesquisa>();

                cfg.CreateMap<Pesquisa, RelatorioPesquisaSatisfacaoViewmodel>();
                cfg.CreateMap<RelatorioPesquisaSatisfacaoViewmodel, Pesquisa>();


                #endregion

                #region Atendimento



                cfg.CreateMap<Atendimento, AtendimentoViewmodel>()
                    .ForMember(dest => dest.NomeEmpresa, map => map.MapFrom(orig => orig.Filial.Empresa.Nome))
                    .ForMember(dest => dest.id_empresa, map => map.MapFrom(orig => orig.Filial.id_empresa))
                    .ForMember(dest => dest.FuncionariosCliente, map => map.MapFrom(orign => orign.UsuariosCliente.Select(x => x.Nome).ToArray().ToStringSeparada(",")))
                    .ForMember(dest => dest.NomeSetor, map => map.MapFrom(orign => orign.Setor.Nome))
                    .ForMember(dest => dest.FuncionariosFocus, map => map.MapFrom(orign => orign.UsuariosFocus.Select(x => x.Nome).ToArray().ToStringSeparada(",")))
                    .ForMember(dest => dest.UsuariosClienteSelecionados, map => map.MapFrom(orign => orign.UsuariosCliente.Select(x => x.id_usuario)))
                    .ForMember(dest => dest.UsuariosFocusSelecionados, map => map.MapFrom(orign => orign.UsuariosFocus.Select(x => x.id_usuario)))
                    ;

                cfg.CreateMap<AtendimentoViewmodel, Atendimento>()
                .ForMember(dest=> dest.UsuariosCliente, map => map.MapFrom( orign =>   orign.UsuariosClienteSelecionados.Select(x=> new Usuario { id_usuario = x })))
                .ForMember(dest => dest.UsuariosFocus, map => map.MapFrom(orign => orign.UsuariosFocusSelecionados.Select(x => new Usuario { id_usuario = x })));




                cfg.CreateMap<ATAViewmodel, Atendimento>();
                cfg.CreateMap<Atendimento, ATAViewmodel>()
                    .ForMember(dest => dest.NomeCliente,
                        map => map.MapFrom(orig => orig.Filial.Empresa.Nome))
                    .ForMember(dest => dest.AssuntoVisita,
                        map => map.MapFrom(orig => orig.Assunto));

                cfg.CreateMap<Anexo, AnexoViewmodel>();
                cfg.CreateMap<AnexoViewmodel, Anexo>();







                #endregion

                #region SETOR


                cfg.CreateMap<Setor, SetorViewmodel>()
                    .ForMember(dest => dest.NomeResponsavel, map => map.MapFrom(orig => orig.UsuarioResponsavel.Nome));

                cfg.CreateMap<SetorViewmodel, Setor>();

                cfg.CreateMap<Setor, SetorGridViewmodel>()
                    .ForMember(dest => dest.NomeResponsavel, map => map.MapFrom(orig => orig.UsuarioResponsavel.Nome));

                cfg.CreateMap<SetorGridViewmodel, Setor>();


                cfg.CreateMap<ParametroAgendamento, ParametroAgendamentoViewmodel>();
                cfg.CreateMap<ParametroAgendamentoViewmodel, ParametroAgendamento>();


                #endregion

                #region Pendencia 

                cfg.CreateMap<Pendencia, PendenciaViewmodel>()
                    .ForMember(dest => dest.NomeResponsavel, map => map.MapFrom(orig => orig.Responsavel.Nome))
                    .ForMember(dest => dest.NomeEmpresa, map=> map.MapFrom(orign => orign.Atendimento.Filial.Empresa.Nome))
                    .ForMember(dest => dest.AssuntoAtendimento, map => map.MapFrom(orig => orig.Atendimento.DataAgendada.ToString("dd/MM") + " - " + orig.Atendimento.Assunto));


                cfg.CreateMap<PendenciaViewmodel, Pendencia>();
                cfg.CreateMap<ComentarioViewmodel, Comentario>();
                cfg.CreateMap<Comentario, ComentarioViewmodel>()
                    .ForMember(dest => dest.NomeAutor, map => map.MapFrom(orig => orig.Usuario.Nome));

                #endregion


            });
        }
    }
}
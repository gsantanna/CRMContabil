using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using bie.focuscrm.application.Interfaces;
using bie.focuscrm.domain.Entities;
using bie.focuscrm.domain.Enums;
using bie.focuscrm.ui.viewmodel;
using Microsoft.AspNet.Identity;

namespace bie.focuscrm.ui.mvc.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly IAlertaAppService _appSvcAlerta;
        private readonly IUsuarioAppService _appSvcUsuario;
        private readonly IAtendimentoAppService _appSvcAtendimento;
        private readonly IPendenciaAppService _appSvcPendencias;
        private readonly ISetorAppService _appSvcSetor;

        public HomeController(IAlertaAppService svc1, IUsuarioAppService svc2, IAtendimentoAppService svc3, IPendenciaAppService svc4, ISetorAppService svc5)
        {
            _appSvcAlerta = svc1;
            _appSvcUsuario = svc2;
            _appSvcAtendimento = svc3;
            _appSvcPendencias = svc4;
            _appSvcSetor = svc5;

        }

        public ActionResult Index()
        {
            var model = new HomeViewmodel();
            var usuario = _appSvcUsuario.GetById(User.Identity.GetUserId());



            #region Alertas

            


            var alertas = (from a in _appSvcAlerta.GetAll()
                           where
                           (a.Alcances.Any(f => f.id_usuario == usuario.id_usuario ) || a.Alcances.Count == 0) &&   //Alcance
                           (DateTime.Now >= a.Inicio && DateTime.Now <= a.Fim) &&  //Período                          
                           a.Visualizacoes.Count(f => f.id_usuario ==  usuario.id_usuario ) < a.max_exibicoes  //Max exibições
                           select a).ToList();


            //Cadastra a exibição do alerta para o usuário 
            foreach (var alerta in alertas)
            {
                alerta.Visualizacoes.Add(new AlertaVisualizacao
                {
                    id_alerta = alerta.id_alerta,
                    data_visualizacao = DateTime.Now,
                    id_usuario = User.Identity.GetUserId()
                });

                _appSvcAlerta.Update(alerta);
            }

            model.Alertas = Mapper.Map<IEnumerable<Alerta>, IEnumerable<AlertaViewmodel>>(alertas).ToList();

            #endregion

            #region MeusAtendimentos
            //se for administrador mostra tudo 
            if (TipoUsuarioAtual == TipoUsuario.Setor || TipoUsuarioAtual == TipoUsuario.Cliente)
            {


                model.MeusAtendimentos =
                    Mapper.Map<IEnumerable<Atendimento>, IEnumerable<AtendimentoViewmodel>>(
                        _appSvcAtendimento.GetAll().Where(x => 
                        x.UsuariosCliente.Any(u=>u.id_usuario == usuario.id_usuario) || 
                        x.UsuariosFocus.Any(u=> u.id_usuario == usuario.id_usuario)
                        )).ToList();


            }
            else
            {
                model.MeusAtendimentos =
                    Mapper.Map<IEnumerable<Atendimento>, IEnumerable<AtendimentoViewmodel>>(
                        _appSvcAtendimento.GetAll()).ToList();
            }

            #endregion


            #region MinhasPendencias
            
            if (TipoUsuarioAtual == TipoUsuario.Setor || TipoUsuarioAtual == TipoUsuario.Cliente)
            {
                ViewBag.Admin = false;

                model.MinhasPendencias =
                    Mapper.Map<IEnumerable<Pendencia>, IEnumerable<PendenciaViewmodel>>(
                        _appSvcPendencias.GetAll().Where(x => x.id_responsavel == usuario.id_usuario)).ToList();
            }
            else
            {
                ViewBag.Admin = true;



                List<HomeViewmodel.ConsolidadoPendenciasViewmodel> lstPend = new List<HomeViewmodel.ConsolidadoPendenciasViewmodel>();

                foreach(var setor in _appSvcSetor.GetAll())
                {
                    lstPend.Add(new HomeViewmodel.ConsolidadoPendenciasViewmodel
                    {
                        id_setor = setor.id_setor,
                        nomesetor = setor.Nome,
                        qtd = setor.Funcionarios.Select(x => x.Pendencias.Count(y=> y.Status != "Concluído")).Sum()
                    });
                }



                //var pendenciasTotalAgrupada = _appSvcPendencias.GetAll().GroupBy(s => s.Atendimento.Setor)
                //    .Select(grupo => new HomeViewmodel.ConsolidadoPendenciasViewmodel
                //    {
                //        nomesetor = grupo.Key.Nome,
                //        id_setor = grupo.Key.id_setor,
                //        qtd = grupo.Count()
                //    }).OrderByDescending(x=> x.qtd).ToList();

                model.PendenciasConsolidadas = lstPend.OrderByDescending(x=> x.qtd).ToList();




            }


            #endregion



            return View(model);

        }


    }
}
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bie.focuscrm.application.Interfaces;
using bie.focuscrm.domain.Entities;
using bie.focuscrm.ui.viewmodel;
using AutoMapper;
using System;
using bie.focuscrm.domain.Enums;
using bie.focuscrm.ui.mvc.Helpers;

namespace bie.focuscrm.ui.mvc.Controllers
{
    public class ResponderPesquisaController : BaseController
    {
        public IPesquisaAppService _appPesquisa { get; private set; }
        public IUsuarioAppService _appUsuario { get; private set; }
        public IAtendimentoAppService _appAtendimento { get; private set; }

        public IRespostaPesquisaAppService _appResposta { get; private set; }

        //public ResponderPesquisaController(IPesquisaAppService AppSvc, IUsuarioAppService AppSvc2, IAtendimentoAppService AppSvc3)
        public ResponderPesquisaController(IRespostaPesquisaAppService AppSvc, IPesquisaAppService AppSvc2)
        {
            _appResposta = AppSvc;
            _appPesquisa = AppSvc2;
        }

        // GET: RespostaPesquisa
        [HttpGet]
        public ActionResult Index(int id)
        {
            //encontra a entidade
            var entidade = _appResposta.GetById(id);
            if (entidade == null) throw new HttpException(404, "Item não encontrado");

            // pesquisa ja respondida nao deixa responder de novo - TODO melhorar o erro
            if (entidade.Valores.Count() > 0) throw new HttpException(404, "Item não encontrado");

            // verifica se o usuario é o da pesquisa realmente - TODO melhorar o erro
            if( TipoUsuarioAtual != TipoUsuario.Superadmin && TipoUsuarioAtual != TipoUsuario.Administrador &&  IdentityHelper.GetCurrentUser().Id != entidade.id_usuario) throw new HttpException(404, "Item não encontrado");

            var model = Mapper.Map<RespostaPesquisa, RespostaPesquisaViewmodel>(entidade);

            //model.Pesq = _appPesquisa.GetById(entidade.id_pesquisa);

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Salvar(RespostaPesquisaViewmodel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            else
            {
                //encontra a entidade
                var entidade = _appResposta.GetById(vm.id_respostapesquisa);
                if (entidade == null) throw new HttpException(404, "Item não encontrado");

                string[] keys = Request.Form.AllKeys;

                for (int i = 0; i < keys.Length; i++)
                {
                    if (keys[i].StartsWith("val_") == true)
                    {
                        string[] partes = keys[i].Split('_');

                        entidade.Valores.Add(new RespostaPesquisaValor { id_questao = int.Parse(partes[2]), id_respostapesquisa = int.Parse(partes[1]), ValorResposta = Request.Form[keys[i]], });
                    }
                }

                entidade.DataResposta = DateTime.Now;

                _appResposta.Update(entidade);

                // TODO redirecionar para pagina de agradecimento(?)
                // Joga para a Ver apenas para visualizar o resultado em teste dev
                return RedirectToAction("Fim");
            }
        }

        public ActionResult Fim()
        {
            return View();
        }

        // TODO Autorizacao
        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public ActionResult Ver(int id)
        {
            //encontra a entidade
            var entidade = _appResposta.GetById(id);
            if (entidade == null) throw new HttpException(404, "Item não encontrado");
            
            var model = Mapper.Map<RespostaPesquisa, RespostaPesquisaViewmodel>(entidade);

            //model.Pesq = _appPesquisa.GetById(entidade.id_pesquisa);

            return View(model);
        }
    }
}
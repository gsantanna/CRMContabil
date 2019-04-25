using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bie.focuscrm.application.Interfaces;
using bie.focuscrm.domain.Entities;
using bie.focuscrm.ui.viewmodel;
using AutoMapper;
using System.Data.Entity;

namespace bie.focuscrm.ui.mvc.Controllers
{
    [Authorize(Roles = "Administrador, Focus, Setor")]
    public class QuestaoPesquisaController : Controller
    {
        public IPesquisaAppService _appPesquisa { get; private set; }

        public QuestaoPesquisaController(IPesquisaAppService AppSvc)
        {
            _appPesquisa = AppSvc;
        }

        
        public ActionResult Index(int id)
        {
            var entidade = _appPesquisa.GetById(id);
            var model = Mapper.Map<Pesquisa, PesquisaViewmodel>(entidade);
            return View(model);
        }

        public JsonResult GetJson(int id)
        {
            var entidade = _appPesquisa.GetById(id).Questoes;
            var model = Mapper.Map<IEnumerable<QuestaoPesquisa>, IEnumerable<QuestaoPesquisaViewmodel>>(entidade);

            return new JsonResult2 { Data = new { data = model } };
        }

        [HttpGet]
        public ActionResult Criar(int id_pesquisa)
        {
            var entidade = new QuestaoPesquisa(); //_appPesquisa.GetById(id_pesquisa).Questoes.FirstOrDefault();
            var model = Mapper.Map<QuestaoPesquisa, QuestaoPesquisaViewmodel>(entidade);

            model.id_pesquisa = id_pesquisa;

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Criar(QuestaoPesquisaViewmodel vm)
        {
            //encontra a entidade
            var entidade = _appPesquisa.GetById(vm.id_pesquisa);
            if (entidade == null) throw new HttpException(404, "Item não encontrado");

            //mapeia as propriedades comuns
            var objEntidade = Mapper.Map<QuestaoPesquisaViewmodel, QuestaoPesquisa>(vm);

            objEntidade.Ativo = true;

            entidade.Questoes.Add(objEntidade);

            _appPesquisa.Update(entidade);

            return RedirectToAction("index", new { id = objEntidade.id_pesquisa });
        }

        [HttpGet]
        public ActionResult Editar(int id_pesquisa, int id)
        {
            //encontra a entidade
            var entidade = _appPesquisa.GetById(id_pesquisa).Questoes;
            if (entidade == null) throw new HttpException(404, "Item não encontrado");

            var questao = entidade.FirstOrDefault(x => x.id_questaopesquisa == id);

            var model = Mapper.Map<QuestaoPesquisa, QuestaoPesquisaViewmodel>(questao);
            
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Editar(QuestaoPesquisaViewmodel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            else
            {
                //encontra a entidade
                var entidade = _appPesquisa.GetById(vm.id_pesquisa);
                if (entidade == null) throw new HttpException(404, "Item não encontrado");

                // encontra a questao
                var questao = entidade.Questoes.FirstOrDefault(x => x.id_questaopesquisa == vm.id_questaopesquisa);

                questao.Descricao = vm.Descricao;
                questao.Valores = vm.Valores;
                //questao.Ativo = vm.Ativo;
                questao.Ordem = vm.Ordem;

                _appPesquisa.Update(entidade);

                return RedirectToAction("index", new { id = vm.id_pesquisa });
            }
        }
        
        [HttpPost]
        public JsonResult Deletar(int id_pesquisa, int id)
        {
            //encontra a entidade 
            var model = _appPesquisa.GetById(id_pesquisa);
            if (model == null) throw new HttpException(404, "Pesquisa não encontrada");
            
           _appPesquisa.RemoveQuestao(model, id);

            return Json("OK");
        }

        public JsonResult Ativacao(int id_pesquisa, int id)
        {
            var model = _appPesquisa.GetById(id_pesquisa);
            if (model == null) throw new HttpException(404, "Pesquisa não encontrada");

            // encontra a questao
            var questao = model.Questoes.FirstOrDefault(x => x.id_questaopesquisa == id);

            questao.Ativo = !questao.Ativo;

            _appPesquisa.Update(model);

            return Json("OK");
        }
    }
}
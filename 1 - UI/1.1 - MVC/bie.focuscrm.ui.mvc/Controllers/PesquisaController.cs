using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bie.focuscrm.application.Interfaces;
using bie.focuscrm.domain.Entities;
using bie.focuscrm.ui.viewmodel;
using AutoMapper;
using System.Threading.Tasks;
using bie.focuscrm.ui.mvc.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using bie.focuscrm.domain.Enums;

namespace bie.focuscrm.ui.mvc.Controllers
{
    [Authorize(Roles = "Administrador, Superadmin")]
    public class PesquisaController : Controller
    {
        public IPesquisaAppService _appPesquisas { get; private set; }
        public UserManager<ApplicationUser> UserManager { get; private set; }
        public DbContext context { get; private set; }

        public PesquisaController(IPesquisaAppService AppSvc)
        {
            _appPesquisas = AppSvc;
            context = new ApplicationDbContext();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
        }

        // GET: Pesquisa
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetJson()
        {
            var entidade = _appPesquisas.GetAll();
            var model = Mapper.Map<IEnumerable<Pesquisa>, IEnumerable<PesquisaViewmodel>>(entidade);

            return new JsonResult2 { Data = new { data = model } };
        }

        [HttpGet]
        public ActionResult Criar()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Criar(PesquisaViewmodel vm)
        {
            //mapeia as propriedades comuns
            var objEntidade = Mapper.Map<PesquisaViewmodel, Pesquisa>(vm);

            objEntidade.Ativo = true;
            objEntidade.Criado = DateTime.Now;
            objEntidade.Modificado = DateTime.Now;

            _appPesquisas.Add(objEntidade);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            //encontra a entidade
            var entidade = _appPesquisas.GetById(id);

            if (entidade == null) throw new HttpException(404, "Item não encontrado");

            var vm = Mapper.Map<Pesquisa, PesquisaViewmodel>(entidade);

            return View(vm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Editar(PesquisaViewmodel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            else
            {
                //encontra a entidade
                var entidade = _appPesquisas.GetById(vm.id_pesquisa);
                if (entidade == null) throw new HttpException(404, "Item não encontrado");

                entidade.tp_pesquisa = vm.tp_pesquisa;
                //entidade.Ativo = vm.Ativo;
                //entidade.Criado = vm.Criado;
                entidade.Modificado = DateTime.Now;
                entidade.Titulo = vm.Titulo;
                entidade.Descricao = vm.Descricao;

                _appPesquisas.Update(entidade);
                
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        public JsonResult Deletar(int id)
        {
            //encontra a entidade 
            var model = _appPesquisas.GetById(id);
            if (model == null) throw new HttpException(404, "Pesquisa não encontrada");
            //deleta as exibicoes
            _appPesquisas.Remove(model);
            return Json("OK");
        }

        public JsonResult Ativacao(int id)
        {
            var model = _appPesquisas.GetById(id);
            if (model == null) throw new HttpException(404, "Pesquisa não encontrada");

            model.Ativo = !model.Ativo;

            _appPesquisas.Update(model);

            return Json("OK");
        }

        [HttpGet]
        public ActionResult Resposta()
        {
            ViewBag.di = DateTime.Now.Date.ToString("dd/MM/yy HH:mm:ss");
            ViewBag.df = DateTime.Now.Date.AddSeconds(86399).ToString("dd/MM/yy HH:mm:ss");
            
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Resposta(DateTime? di, DateTime? df)
        {

            if (!di.HasValue) di = DateTime.Now.Date;
            if (!df.HasValue) df = DateTime.Now.Date.AddSeconds(86399);

            ViewBag.di = di.Value.ToString("dd/MM/yy HH:mm:ss");
            ViewBag.df = df.Value.ToString("dd/MM/yy HH:mm:ss");

            //faz a pesquisa  com join de usuários 
            var usuarios = await UserManager.Users.ToListAsync();
            
            var pesquisas = _appPesquisas.GetAll().Where(p => p.tp_pesquisa == TipoPesquisa.Satisfacao && p.Respostas.Count() > 0 && p.Respostas.FirstOrDefault().DataResposta >= di && p.Respostas.FirstOrDefault().DataResposta <= df);

            var model = Mapper.Map<IEnumerable<Pesquisa>, IEnumerable<RelatorioPesquisaSatisfacaoViewmodel>>(pesquisas);
            
            return View(model);

        }
    }
}
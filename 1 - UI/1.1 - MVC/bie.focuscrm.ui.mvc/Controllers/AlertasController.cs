using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using bie.focuscrm.application.Interfaces;
using bie.focuscrm.domain.Entities;
using bie.focuscrm.ui.viewmodel;

namespace bie.focuscrm.ui.mvc.Controllers
{
    [Authorize(Roles = "Administrador, Superadmin")]
    public class AlertasController : Controller
    {

        public IAlertaAppService _appAlertas { get; private set; }
        public IUsuarioAppService _appUsuario { get; private set; }

        public AlertasController(IAlertaAppService AppSvc, IUsuarioAppService AppSvc2)
        {
            _appAlertas = AppSvc;
            _appUsuario = AppSvc2;
        }

        // GET: Admin/Alertas
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetJson()
        {
            var entidade = _appAlertas.GetAll();
            var model = Mapper.Map<IEnumerable<Alerta>, IEnumerable<AlertaViewmodel>>(entidade);

            return new JsonResult2 { Data = new { data = model } };
        }
        
        [HttpGet]
        public ActionResult Criar()
        {
            ViewBag.Usuarios = new SelectList(_appUsuario.GetAll().ToList(), "id_usuario", "Nome");
            ViewBag.Selecionadas = new SelectList(new List<UsuarioViewmodel>(), "id_usuario", "Nome");

            return View();

        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Criar(AlertaViewmodel vm)
        {
            //todos os Usuários
            var emp = _appUsuario.GetAll().ToList();

            //selecionadas
            var selecionadas = from sel in vm.UsuariosSelecionados
                               join e in emp on sel equals e.id_usuario
                               select e;

            //disponiveis
            var disponiveis = emp.Except(selecionadas);


            ViewBag.Usuarios = new SelectList(disponiveis, "id_usuario", "Nome");
            ViewBag.Selecionadas = new SelectList(selecionadas, "id_usuario", "Nome");


            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            //mapeia as propriedades comuns
            var objEntidade = Mapper.Map<AlertaViewmodel, Alerta>(vm);
            _appAlertas.Add(objEntidade);


            //monta a lista de Alcance
            foreach (var item in selecionadas)
            {
                objEntidade.Alcances.Add(new AlertaAlcance { id_usuario = item.id_usuario, id_alerta = objEntidade.id_alerta });
            }

            _appAlertas.Update(objEntidade);



            return RedirectToAction("index");
             

        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            //encontra a entidade
            var entidade = _appAlertas.GetById(id);
            if (entidade == null) throw new HttpException(404, "Item não encontrado");


            var vm = Mapper.Map<Alerta, AlertaViewmodel>(entidade);
            vm.UsuariosSelecionados = vm.Alcances.Select(f => f.id_usuario.ToString()).ToArray();

            var usuarios = _appUsuario.GetAll();


            //selecionadas
            var selecionadas = from sel in vm.UsuariosSelecionados
                               join e in usuarios on sel equals e.id_usuario
                               select e;

            var disponiveis = usuarios.Except(selecionadas);

            ViewBag.Usuarios = new SelectList(disponiveis, "id_usuario", "Nome");
            ViewBag.Selecionadas = new SelectList(selecionadas, "id_usuario", "Nome");

            return View(vm);



        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Editar(AlertaViewmodel vm)
        {

            var usuarios = _appUsuario.GetAll();

            //selecionadas
            var selecionadas = from sel in vm.UsuariosSelecionados
                               join e in usuarios on sel equals e.id_usuario.ToString()
                               select e;

            var disponiveis = usuarios.Except(selecionadas);

            ViewBag.Usuarios = new SelectList(disponiveis, "id_usuario", "Nome");
            ViewBag.Selecionadas = new SelectList(selecionadas, "id_usuario", "Nome");

            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            else
            {

                //encontra a entidade
                var entidade = _appAlertas.GetById(vm.id_alerta);
                if (entidade == null) throw new HttpException(404, "Item não encontrado");

                entidade.Titulo = vm.Titulo;
                entidade.Classificacao = vm.Classificacao;
                entidade.Inicio = vm.Inicio;
                entidade.Fim = vm.Fim;
                entidade.Conteudo = vm.Conteudo;
                entidade.max_exibicoes = vm.max_exibicoes;


                //reseta o alcance da entidade
                entidade.Alcances.Clear();
                _appAlertas.Update(entidade);

                //monta a lista de Alcance
                foreach (var item in selecionadas)
                {
                    entidade.Alcances.Add(new AlertaAlcance { id_usuario = item.id_usuario, id_alerta = entidade.id_alerta });
                }

                _appAlertas.Update(entidade);





                return RedirectToAction("index");





            }


        }






        [HttpPost]
        public JsonResult Deletar(int id)
        {

            //encontra a entidade 
            var model = _appAlertas.GetById(id);
            if (model == null) throw new HttpException(404, "Alerta não encontrado");
            //deleta as exibicoes
            _appAlertas.Remove(model);
            return Json("OK");



        }











    }
}
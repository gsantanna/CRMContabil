using System;
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
    public class SetorController : Controller
    {
        public ISetorAppService _appSetor { get; private set; }
        public IUsuarioAppService _appUsuario { get; private set; }

        public SetorController(ISetorAppService AppSvc, IUsuarioAppService AppSvc2)
        {
            _appSetor = AppSvc;
            _appUsuario = AppSvc2;
        }

        // GET: Setor
        public ActionResult Index()
        {
            return View();
        }


        #region API 

        [AllowAnonymous]
        public JsonResult GetUsuarios(int id)
        {
            Response.ClearHeaders();
            Response.ClearContent();
            Response.Clear();
            var setor = _appSetor.GetById(id);
            if (setor == null) throw new HttpException(404, "Setor não encontrado");

            //usuários
            var model = setor.Funcionarios.Where(x => x.Ativo).Select(x => new { x.id_usuario, x.Nome }).ToList();

            model.Add(new { setor.UsuarioResponsavel.id_usuario, setor.UsuarioResponsavel.Nome });


            return Json(model.Distinct(), JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetJson()
        {
            var entidade = _appSetor.GetAll();

            var model = Mapper.Map<IEnumerable<Setor>, IEnumerable<SetorGridViewmodel>>(entidade);

            foreach (var m in model)
            {
                var ent = _appSetor.GetById(m.id_setor);

                m.FuncionariosSetor_desc = ent.Funcionarios.Count() == 0 ? "Todos" : ent.Funcionarios.Count().ToString();
            }

            return new JsonResult2 { Data = new { data = model } };
        }

        #endregion




        [HttpGet]
        public ActionResult Criar()
        {
            ViewBag.Usuarios = new SelectList(_appUsuario.GetAll().ToList(), "id_usuario", "Nome");
            ViewBag.Funcionarios = new SelectList(new List<Usuario>(), "id_usuario", "Nome");

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Criar(SetorViewmodel vm)
        {
            //todos os usuarios
            var usu = _appUsuario.GetAll().ToList();

            //funcionarios
            var funcionarios = (from sel in vm.FuncionariosSetor
                                join u in usu on sel equals u.id_usuario
                                select new Usuario { Nome = u.Nome, id_usuario = u.id_usuario }).ToList();

            //disponiveis
            var disponiveis = usu.Except(funcionarios);


            ViewBag.Usuarios = new SelectList(disponiveis, "id_usuario", "Nome");
            ViewBag.Funcionarios = new SelectList(funcionarios, "id_usuario", "Nome");


            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            //mapeia as propriedades comuns
            var objEntidade = Mapper.Map<SetorViewmodel, Setor>(vm);

            //monta a lista de funcionarios
            foreach (var item in funcionarios)
            {
                objEntidade.Funcionarios.Add(item);
            }

            objEntidade.Ativo = true;

            //_appSetor.Update(objEntidade);
            _appSetor.Add(objEntidade);

            return RedirectToAction("index");
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            //encontra a entidade
            var entidade = _appSetor.GetById(id);
            if (entidade == null) throw new HttpException(404, "Item não encontrado");


            var vm = Mapper.Map<Setor, SetorViewmodel>(entidade);
            vm.FuncionariosSetor = vm.Funcionarios.Select(f => f.id_usuario.ToString()).ToArray();

            //todas os usuarios
            var usu = _appUsuario.GetAll().ToList();

            //funcionarios
            var funcionarios = (from sel in vm.FuncionariosSetor
                                join u in usu on sel equals u.id_usuario.ToString()
                                select new Usuario { Nome = u.Nome, id_usuario = u.id_usuario }).ToList();

            //disponiveis
            //var disponiveis = usu.Except(funcionarios);

            foreach (var f in vm.Funcionarios.ToList())
            {
                foreach (var u in usu.ToList())
                {
                    if (u.id_usuario == f.id_usuario)
                        usu.Remove(u);
                }
            }

            ViewBag.TodosUsuarios = new SelectList(_appUsuario.GetAll().ToList(), "id_usuario", "Nome");
            ViewBag.Usuarios = new SelectList(usu, "id_usuario", "Nome");
            ViewBag.Funcionarios = new SelectList(funcionarios, "id_usuario", "Nome");

            return View(vm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Editar(SetorViewmodel vm)
        {
            //todas os usuarios
            var usu = _appUsuario.GetAll().ToList();

            //funcionarios
            var funcionarios = (from sel in vm.FuncionariosSetor
                                join u in usu on sel equals u.id_usuario
                                select new Usuario { Nome = u.Nome, id_usuario = u.id_usuario }).ToList();

            //disponiveis
            var disponiveis = usu.Except(funcionarios);


            ViewBag.Usuarios = new SelectList(disponiveis, "id_usuario", "Nome");
            ViewBag.Funcionarios = new SelectList(funcionarios, "id_usuario", "Nome");

            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            else
            {
                // TODO validar se o registro existe - a validação abaixo cria um conflito de entidades no mapper
                //encontra a entidade
                //var entidade = _appSetor.GetById(vm.id_setor);
                //if (entidade == null) throw new HttpException(404, "Item não encontrado");

                //mapeia as propriedades comuns
                var entidade = Mapper.Map<SetorViewmodel, Setor>(vm);

                //reseta o alcance da entidade
                entidade.Funcionarios.Clear();

                //_appSetor.Update(entidade);

                //monta a lista de funcionarios
                foreach (var item in funcionarios)
                {
                    entidade.Funcionarios.Add(item);
                }

                _appSetor.Update(entidade);


                return RedirectToAction("index");
            }
        }

        [HttpPost]
        public JsonResult Deletar(int id)
        {
            //encontra a entidade 
            var model = _appSetor.GetById(id);

            if (model == null) throw new HttpException(404, "Alerta não encontrado");

            //deleta as exibicoes
            _appSetor.Remove(model);

            return Json("OK");
        }

        public JsonResult Ativacao(int id)
        {
            var model = _appSetor.GetById(id);
            if (model == null) throw new HttpException(404, "Pesquisa não encontrada");

            model.Ativo = !model.Ativo;

            _appSetor.Update(model);

            return Json("OK");
        }
    }
}
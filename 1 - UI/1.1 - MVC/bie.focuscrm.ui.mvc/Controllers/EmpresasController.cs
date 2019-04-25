using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AutoMapper;
using bie.focuscrm.application.Interfaces;
using bie.focuscrm.domain.Entities;
using bie.focuscrm.ui.viewmodel;
using Newtonsoft.Json.Linq;
using System.Collections;

namespace bie.focuscrm.ui.mvc.Controllers
{
    [Authorize(Roles = "Administrador, Focus, Setor")]
    public class EmpresasController : Controller
    {
        public IEmpresaAppService _appEmpresas { get; private set; }
        public ISetorAppService _appSetores { get; private set; }
        public IUsuarioAppService _appUsuarios { get; private set; }

        public EmpresasController(IEmpresaAppService AppSvc, ISetorAppService AppSvc2, IUsuarioAppService AppSvc3)
        {
            _appEmpresas = AppSvc;
            _appSetores = AppSvc2;
            _appUsuarios = AppSvc3;
        }

        // GET: Empresa
        [Authorize(Roles = "Administrador, Focus")]
        public ActionResult Index()
        {
            return View();
        }

        #region API

        [Authorize(Roles = "Administrador, Focus")]
        public JsonResult GetJson()
        {
            var entidade = _appEmpresas.GetAll();
            var model = Mapper.Map<IEnumerable<Empresa>, IEnumerable<EmpresaGridViewmodel>>(entidade);

            foreach (var m in model)
            {
                m.QtdFiliais = _appEmpresas.GetById(m.id_empresa).Filiais.Count().ToString();
            }

            return new JsonResult2 { Data = new { data = model } };
        }


        [Authorize(Roles ="Administrador, Focus, Setor")]
        public JsonResult GetUsuariosEmpresa(int id)
        {
            Response.ClearHeaders();
            Response.ClearContent();
            Response.Clear();
            var empresa = _appEmpresas.GetById(id);
            if (empresa == null) throw new HttpException(404, "Empresa não encontrado");
            //usuários
            var model = empresa.Usuarios.Where(x => x.Ativo).Select(x => new { x.id_usuario, x.Nome }).ToList();
            return Json(model.Distinct(), JsonRequestBehavior.AllowGet);
        }


        [Authorize(Roles = "Administrador, Focus, Setor")]
        public JsonResult GetFiliais(int id)
        {
            Response.ClearHeaders();
            Response.ClearContent();
            Response.Clear();
            var empresa = _appEmpresas.GetById(id);
            if (empresa == null) throw new HttpException(404, "Empresa não encontrada");
            var model = empresa.Filiais.Select(x => new { x.id_filial, x.Nome, x.RazaoSocial });
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        #endregion










        [HttpGet]
        [Authorize(Roles = "Administrador, Focus")]
        public ActionResult Criar()
        {
            ViewBag.Usuarios = new SelectList(_appUsuarios.GetAll().ToList(), "id_usuario", "Nome");
            ViewBag.Funcionarios = new SelectList(new List<Usuario>(), "id_usuario", "Nome");

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize(Roles = "Administrador, Focus")]
        public ActionResult Criar(EmpresaViewmodel vm)
        {
            //todos os usuarios
            var usu = _appUsuarios.GetAll().ToList();

            //funcionarios
            var funcionarios = new List<Usuario>();

            if (vm.FuncionariosEmpresa != null)
                foreach (var f in (from sel in vm.FuncionariosEmpresa
                                   join u in usu on sel equals u.id_usuario
                                   select new Usuario { Nome = u.Nome, id_usuario = u.id_usuario }).ToList())
                    funcionarios.Add(f);

            //disponiveis
            var disponiveis = usu.Except(funcionarios);


            ViewBag.Usuarios = new SelectList(disponiveis, "id_usuario", "Nome");
            ViewBag.Funcionarios = new SelectList(funcionarios, "id_usuario", "Nome");

            //mapeia as propriedades comuns
            var objEntidade = Mapper.Map<EmpresaViewmodel, Empresa>(vm);

            objEntidade.Usuarios = new List<Usuario>();
            
            //monta a lista de funcionarios
            foreach (var item in funcionarios)
            {
                objEntidade.Usuarios.Add(item);
            }

            _appEmpresas.Add(objEntidade);

            return RedirectToAction("Index");
            //return RedirectToAction("Editar", new { id = objEntidade.id_empresa });
        }

        [HttpGet]
        [Authorize(Roles = "Administrador, Focus")]
        public ActionResult Editar(int id)
        {
            //encontra a entidade
            var entidade = _appEmpresas.GetById(id);

            if (entidade == null) throw new HttpException(404, "Item não encontrado");

            var vm = Mapper.Map<Empresa, EmpresaViewmodel>(entidade);

            vm.FuncionariosEmpresa = vm.Usuarios.Select(f => f.id_usuario.ToString()).ToArray();

            //todas os usuarios
            var usu = _appUsuarios.GetAll().ToList();

            //funcionarios
            var funcionarios = (from sel in vm.FuncionariosEmpresa
                                join u in usu on sel equals u.id_usuario.ToString()
                                select new Usuario { Nome = u.Nome, id_usuario = u.id_usuario }).ToList();

            foreach (var f in vm.Usuarios.ToList())
            {
                foreach (var u in usu.ToList())
                {
                    if (u.id_usuario == f.id_usuario)
                        usu.Remove(u);
                }
            }

            ViewBag.TodosUsuarios = new SelectList(_appUsuarios.GetAll().ToList(), "id_usuario", "Nome");
            ViewBag.Usuarios = new SelectList(usu, "id_usuario", "Nome");
            ViewBag.Funcionarios = new SelectList(funcionarios, "id_usuario", "Nome");

            return View(vm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize(Roles = "Administrador, Focus")]
        public ActionResult Editar(EmpresaViewmodel vm)
        {
            //todas os usuarios
            var usu = _appUsuarios.GetAll().ToList();

            //funcionarios
            var funcionarios = new List<Usuario>();

            if (vm.FuncionariosEmpresa != null)
                foreach (var f in (from sel in vm.FuncionariosEmpresa
                                   join u in usu on sel equals u.id_usuario
                                   select new Usuario { Nome = u.Nome, id_usuario = u.id_usuario }).ToList())
                    funcionarios.Add(f);

            //disponiveis
            var disponiveis = usu.Except(funcionarios);

            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            else
            {
                // TODO validar se o registro existe - a validação abaixo cria um conflito de entidades no mapper
                //encontra a entidade
                //var entidade = _appEmpresas.GetById(vm.id_empresa);
                //if (entidade == null) throw new HttpException(404, "Item não encontrado");

                var model = Mapper.Map<EmpresaViewmodel, Empresa>(vm);

                model.Usuarios.Clear();

                //monta a lista de funcionarios
                foreach (var item in funcionarios)
                {
                    model.Usuarios.Add(item);
                }

                _appEmpresas.Update(model);

                return RedirectToAction("index");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrador, Focus")]
        public JsonResult Deletar(int id)
        {
            //encontra a entidade 
            var model = _appEmpresas.GetById(id);
            if (model == null) throw new HttpException(404, "Pesquisa não encontrada");

            //deleta as exibicoes
            _appEmpresas.Remove(model);

            return Json("OK");
        }

        public JsonResult PesquisarCNPJ(string cnpjsujo)
        {
            var CNPJ = Regex.Replace(cnpjsujo, @"[^\d]", "");

            using (var client = new WebClient())
            {
                client.Encoding = System.Text.Encoding.UTF8;

                var json = client.DownloadString("https://www.receitaws.com.br/v1/cnpj/" + CNPJ);

                var serializer = new JavaScriptSerializer();

                return new JsonResult2 { Data = new { data = serializer.Deserialize(json, typeof(object)) } };
            }
        }


        public ActionResult ParametroAgendamento(int id_empresa)
        {
            return View();
        }

        public JsonResult GetJsonParametroAgendamento(int id_empresa)
        {
            var parametros = new List<ParametroAgendamentoViewmodel>();

            var setores = _appSetores.GetAll().Where(x => x.Ativo == true).OrderBy(x => x.Nome).ToList();

            foreach(var s in setores)
            {
                var param = _appEmpresas.GetById(id_empresa).ParametrosAgendamento.Where(x => x.id_setor == s.id_setor);

                var np = new ParametroAgendamentoViewmodel();

                np.id_empresa = id_empresa;
                np.id_setor = s.id_setor;
                np.SetorNome = s.Nome;
                np.Intervalo = param.Count() > 0 ? param.FirstOrDefault().Intervalo : 0;

                parametros.Add(np);
            }

            return new JsonResult2 { Data = new { data = parametros } };
        }

        [HttpPost]
        [Authorize(Roles = "Administrador, Focus")]
        public JsonResult SalvarParametroAgendamento(int id_empresa, int id_setor, int intervalo)
        {
            var param = new ParametroAgendamento();

            param.id_empresa = id_empresa;
            param.id_setor = id_setor;
            param.Intervalo = intervalo;

            var entidade = _appEmpresas.GetById(id_empresa);

            if (entidade.ParametrosAgendamento.Where(x => x.id_setor == id_setor).Count() > 0)
                entidade.ParametrosAgendamento.Where(x => x.id_setor == id_setor).FirstOrDefault().Intervalo = intervalo; 
            else
                entidade.ParametrosAgendamento.Add(param);

            _appEmpresas.Update(entidade);

            return Json("OK");
        }
    }
}
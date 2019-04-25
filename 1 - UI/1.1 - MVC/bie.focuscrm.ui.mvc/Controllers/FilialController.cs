using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bie.focuscrm.application.Interfaces;
using bie.focuscrm.domain.Entities;
using bie.focuscrm.ui.viewmodel;
using AutoMapper;
using System.Text.RegularExpressions;
using System.Net;
using System.Web.Script.Serialization;

namespace bie.focuscrm.ui.mvc.Controllers
{
    [Authorize(Roles = "Administrador, Focus")]
    public class FilialController : Controller
    {
        public IEmpresaAppService _appEmpresas { get; private set; }

        public FilialController(IEmpresaAppService AppSvc)
        {
            _appEmpresas = AppSvc;
        }


        public ActionResult Index(int id)
        {
            var entidade = _appEmpresas.GetById(id);
            var model = Mapper.Map<Empresa, EmpresaViewmodel>(entidade);
            return View(model);
        }

        public JsonResult GetJson(int id)
        {
            var entidade = _appEmpresas.GetById(id).Filiais;
            var model = Mapper.Map<IEnumerable<Filial>, IEnumerable<FilialViewmodel>>(entidade);

            return new JsonResult2 { Data = new { data = model } };
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

        [HttpGet]
        public ActionResult Criar(int id_empresa)
        {
            var entidade = new Filial();

            entidade.id_empresa = id_empresa;

            var model = Mapper.Map<Filial, FilialViewmodel>(entidade);
            
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Criar(FilialViewmodel vm)
        {
            //encontra a entidade
            var entidade = _appEmpresas.GetById(vm.id_empresa);
            if (entidade == null) throw new HttpException(404, "Item não encontrado");

            //mapeia as propriedades comuns
            var objEntidade = Mapper.Map<FilialViewmodel, Filial>(vm);

            entidade.Filiais.Add(objEntidade);

            _appEmpresas.Update(entidade);

            return RedirectToAction("index", new { id = objEntidade.id_empresa });
        }

        [HttpGet]
        public ActionResult Editar(int id_empresa, int id)
        {
            //encontra a entidade
            var entidade = _appEmpresas.GetById(id_empresa);
            if (entidade == null) throw new HttpException(404, "Item não encontrado");

            var filial = entidade.Filiais.FirstOrDefault(x => x.id_filial == id);

            var model = Mapper.Map<Filial, FilialViewmodel>(filial);

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Editar(FilialViewmodel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            else
            {
                //encontra a entidade
                var entidade = _appEmpresas.GetById(vm.id_empresa);
                if (entidade == null) throw new HttpException(404, "Item não encontrado");

                // encontra a questao
                var filial = entidade.Filiais.FirstOrDefault(x => x.id_filial == vm.id_filial);

                filial.CNPJ = vm.CNPJ;
                filial.Nome = vm.Nome;
                filial.RazaoSocial = vm.RazaoSocial;
                filial.OBS = vm.OBS;
                filial.Logradouro = vm.Logradouro;
                filial.Numero = vm.Numero;
                filial.Complemento = vm.Complemento;
                filial.Bairro = vm.Bairro;
                filial.Cidade = vm.Cidade;
                filial.UF = vm.UF;
                filial.CEP = vm.CEP;
                filial.Telefone = vm.Telefone;


                _appEmpresas.Update(entidade);

                return RedirectToAction("index", new { id = vm.id_empresa });
            }
        }

        [HttpPost]
        public JsonResult Deletar(int id_empresa, int id)
        {
            //encontra a entidade 
            var model = _appEmpresas.GetById(id_empresa);
            if (model == null) throw new HttpException(404, "Empresa não encontrada");

            _appEmpresas.RemoveFilial(model, id);

            return Json("OK");
        }
    }
}
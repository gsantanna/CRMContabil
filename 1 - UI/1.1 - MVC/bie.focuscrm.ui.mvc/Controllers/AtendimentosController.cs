using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
    public class AtendimentosController : BaseController
    {

        #region CONSTRUCTOR 

        //Entidade Negocio
        private readonly IUsuarioAppService _userAppSvc;
        private readonly IEmpresaAppService _empresaAppSvc;
        private readonly IAtendimentoAppService _atendimentoAppSvc;
        private readonly ISetorAppService _setorAppSvc;

        private readonly IPesquisaAppService _pesquisaAppSvc;
        private readonly IRespostaPesquisaAppService _respostaPesquisaAppSvc;


        //Notificação
        private readonly INotificationAppService _NotificationAppSvc;


        public AtendimentosController(IUsuarioAppService svc1, IEmpresaAppService svc2, IAtendimentoAppService svc3,
            ISetorAppService svc4, INotificationAppService svc5, IPesquisaAppService svc6, IRespostaPesquisaAppService svc7)
        {
            _userAppSvc = svc1;
            _empresaAppSvc = svc2;
            _atendimentoAppSvc = svc3;
            _setorAppSvc = svc4;
            _NotificationAppSvc = svc5;
            _pesquisaAppSvc = svc6;
            _respostaPesquisaAppSvc = svc7;
        }

        #endregion

        #region API 




        // GET: GetJson 
        public JsonResult GetJson()
        {
            Response.ClearHeaders();
            Response.ClearContent();
            Response.Clear();


            List<AtendimentoViewmodel> model = new List<AtendimentoViewmodel>();


            //Carrega as visitas do usuário. 
            //SE for Administrador ou focus , lista todas 
            switch (TipoUsuarioAtual)
            {
                case TipoUsuario.Superadmin:
                case TipoUsuario.Administrador:
                case TipoUsuario.Focus:
                    {
                        model =
                            Mapper.Map<IEnumerable<Atendimento>, IEnumerable<AtendimentoViewmodel>>(
                                _atendimentoAppSvc.GetAll()).ToList();
                        break;
                    }
                case TipoUsuario.Setor:
                    {
                        //carrega os setores do usuário atual 
                        var setores = _userAppSvc.GetById(User.Identity.GetUserId()).Setores;

                        var entsetor = from a in _atendimentoAppSvc.GetAll()
                                       join s in setores on a.id_setor equals s.id_setor
                                       select a;
                        //itens dos setores do usuário
                        model = Mapper.Map<IEnumerable<Atendimento>, IEnumerable<AtendimentoViewmodel>>(entsetor).ToList();

                        //itens a
                        model.AddRange(
                            Mapper.Map<IEnumerable<Atendimento>, IEnumerable<AtendimentoViewmodel>>(
                                _atendimentoAppSvc.GetAll().Where(x => x.UsuariosCliente.Any(u => u.id_usuario == User.Identity.GetUserId()))));
                        model = model.Distinct().ToList();
                        break;
                    }
                default: //cliente.

                    var empresaUsuario = _userAppSvc.GetById(User.Identity.GetUserId()).id_empresa;
                    var entcliente =
                        _atendimentoAppSvc.GetAll().Where(a =>
                        a.UsuariosCliente.Any(u => u.id_usuario == User.Identity.GetUserId()) ||
                        a.UsuariosFocus.Any(u => u.id_usuario == User.Identity.GetUserId())
                        );
                    model = Mapper.Map<IEnumerable<Atendimento>, IEnumerable<AtendimentoViewmodel>>(entcliente).ToList();
                    break;
            }


            // return new JsonResult2 { Data = objSaida, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return new JsonResult2 { Data = new { data = model } };
        }

        [Authorize(Roles = "Superadmin,Administrador,Focus,Setor")]
        public JsonResult Deletar(int id)
        {
            try
            {
                var model = _atendimentoAppSvc.GetById(id);
                if (model == null) throw new HttpException(404, "Item não encontrado");


                //verifica se  a reunião tem ATA se tiver, não pode deixar excluir a reunião.
                if (model.UsuariosFocus.Any(x => x.id_usuario == User.Identity.GetUserId()) ||
                    TipoUsuarioAtual == TipoUsuario.Administrador ||
                    TipoUsuarioAtual == TipoUsuario.Superadmin ||
                    TipoUsuarioAtual == TipoUsuario.Focus)
                {
                    _atendimentoAppSvc.Remove(model);
                    return Json(new { status = "Sucesso", mensagem = "Operação concluída com sucesso" });
                }
                else
                {
                    return
                        Json(
                            new
                            {
                                status = "Falha",
                                mensagem = "Acesso negado! Você não pode excluir um item ao qual não é proprietário"
                            });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = "Falha", mensagem = "Erro interno ao excluir o item. O erro foi: " + ex.Message });
            }

        }

        #endregion

        #region CRUD

        // GET: Atendimento
        public ActionResult Index()
        {
            ViewBag.PodeCriar = TipoUsuarioAtual != TipoUsuario.Cliente;

            return View();
        }

        #region Criar 

        [Authorize(Roles = "Superadmin,Administrador,Focus,Setor")]
        [HttpGet]
        public ActionResult Criar()
        {
            #region Preparacao 

            //preparação
            var Empresas = _empresaAppSvc.GetAll().OrderBy(x => x.Nome);
            ViewBag.Empresas = new SelectList(Empresas, "id_empresa", "Nome");

            ViewBag.TipoUsuarioAtual = TipoUsuarioAtual.ToString();

            if (TipoUsuarioAtual == TipoUsuario.Setor)
            //Se for setor só exibe ele mesmo como responsável //por garantia vou marretar o valor no post
            {
                //somente setor do cara 
                var Setores = _userAppSvc.GetById(User.Identity.GetUserId()).Setores.Where(x => x.Ativo);
                ViewBag.Setores = new SelectList(Setores, "id_setor", "Nome");
            }
            else
            {
                ViewBag.Usuarios = new SelectList(_userAppSvc.GetAll().Where(f => f.Ativo), "id_usuario", "Nome");
                ViewBag.Setores = new SelectList(_setorAppSvc.GetAll().Where(f => f.Ativo), "id_setor", "Nome");
            }

            #endregion

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Superadmin,Administrador,Focus,Setor")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Criar(AtendimentoViewmodel model)
        {
            #region Preparacao 

            var Empresas = _empresaAppSvc.GetAll().OrderBy(x => x.Nome);
            ViewBag.Empresas = new SelectList(Empresas, "id_empresa", "Nome", model.id_empresa);

            var Filiais = _empresaAppSvc.GetById(model.id_empresa).Filiais;
            ViewBag.Filiais = new SelectList(Filiais, "id_filial", "Nome", model.id_filial);

            ViewBag.Setores = new SelectList(_setorAppSvc.GetAll().Where(f => f.Ativo), "id_setor", "Nome",
              model.id_setor);


            //Usuários Empresa
            ViewBag.UsuariosCliente = new MultiSelectList(
                    _empresaAppSvc.GetById(model.id_empresa).Usuarios.Where(f => f.Ativo), "id_usuario", "Nome",
                    model.UsuariosClienteSelecionados);

            //Usuários Setor
            ViewBag.UsuariosFocus = new MultiSelectList(
                _setorAppSvc.GetById(model.id_setor).Funcionarios.Where(f => f.Ativo), "id_usuario", "Nome",
                model.UsuariosFocusSelecionados);

            #endregion

            #region Salvar 

            if (!ModelState.IsValid)
            {
                return View(model);
            }



            try
            {

                var entidade = Mapper.Map<AtendimentoViewmodel, Atendimento>(model);
                entidade.id_autor = User.Identity.GetUserId();
                entidade.Criado = DateTime.Now;
                entidade.Modificado = DateTime.Now;

                _atendimentoAppSvc.Add(entidade);

                // await DispararNotificacaoAtendimento("Um atendimento foi criado", entidade.id_atendimento);


            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty,
                    "Erro ao gravar o registro. verifique as permissões do usuário e tente novamente. O Erro foi: " +
                    ex.Message);
                return View(model);
            }

            #endregion




            return RedirectToAction("index");
        }

        #endregion

        #region EDITAR 

        [Authorize(Roles = "Superadmin,Administrador,Focus,Setor")]
        [HttpGet]
        public ActionResult Editar(int id)
        {
            var entidade = _atendimentoAppSvc.GetById(id);
            if (entidade == null) return new HttpNotFoundResult();

            //verifica se é somente leitura
            ViewBag.PodeEditar =
                !(TipoUsuarioAtual == TipoUsuario.Setor &&
                  !(entidade.UsuariosFocus.Any(x => x.id_usuario == User.Identity.GetUserId())));

            var model = Mapper.Map<Atendimento, AtendimentoViewmodel>(entidade);

            #region Preparacao 

            var Empresas = _empresaAppSvc.GetAll().OrderBy(x => x.Nome);
            ViewBag.Empresas = new SelectList(Empresas, "id_empresa", "Nome", model.id_empresa);

            var Filiais = _empresaAppSvc.GetById(model.id_empresa).Filiais;
            ViewBag.Filiais = new SelectList(Filiais, "id_filial", "Nome", model.id_filial);

            ViewBag.Setores = new SelectList(_setorAppSvc.GetAll().Where(f => f.Ativo), "id_setor", "Nome",
                model.id_setor);


            //Usuários Empresa
            ViewBag.UsuariosCliente = new MultiSelectList(
                _empresaAppSvc.GetById(model.id_empresa).Usuarios.Where(f => f.Ativo), "id_usuario", "Nome",
                model.UsuariosClienteSelecionados);

            //Usuários Setor
            ViewBag.UsuariosFocus = new MultiSelectList(
                _setorAppSvc.GetById(model.id_setor).Funcionarios.Where(f => f.Ativo), "id_usuario", "Nome",
                model.UsuariosFocusSelecionados);

            #endregion



            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Superadmin,Administrador,Focus,Setor")]
        public async Task<ActionResult> Editar(AtendimentoViewmodel model)
        {

            var entidade = _atendimentoAppSvc.GetById(model.id_atendimento);
            if (entidade == null) return new HttpNotFoundResult();


            #region Preparacao 

            var Empresas = _empresaAppSvc.GetAll().OrderBy(x => x.Nome);
            ViewBag.Empresas = new SelectList(Empresas, "id_empresa", "Nome", model.id_empresa);

            var Filiais = _empresaAppSvc.GetById(model.id_empresa).Filiais;
            ViewBag.Filiais = new SelectList(Filiais, "id_filial", "Nome", model.id_filial);

            ViewBag.Setores = new SelectList(_setorAppSvc.GetAll().Where(f => f.Ativo), "id_setor", "Nome",
                model.id_setor);


            //Usuários Empresa
            ViewBag.UsuariosCliente = new MultiSelectList(
                _empresaAppSvc.GetById(model.id_empresa).Usuarios.Where(f => f.Ativo), "id_usuario", "Nome",
                model.UsuariosClienteSelecionados);

            //Usuários Setor
            ViewBag.UsuariosFocus = new MultiSelectList(
                _setorAppSvc.GetById(model.id_setor).Funcionarios.Where(f => f.Ativo), "id_usuario", "Nome",
                model.UsuariosFocusSelecionados);

            #endregion



            try
            {
                // Atualiza os dados da entidade
                Mapper.Map(model, entidade, typeof(AtendimentoViewmodel), typeof(Atendimento));

                entidade.id_autor = User.Identity.GetUserId();
                entidade.Modificado = DateTime.Now;

                _atendimentoAppSvc.Update(entidade);

                await DispararNotificacaoAtendimento("Um atendimento foi modificado!", entidade.id_atendimento);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty,
                    "Erro ao salvar o registro, verifique se o usuário tem permissões adequadas e tente novamente. O Erro foi: " +
                    ex.Message);
                return View(model);
            }



            return RedirectToAction("index");


        }



        #endregion


        #endregion

        #region ATA 

        [HttpGet]
        public ActionResult ATA(int id_atendimento)
        {
            //verifica se a ATA já existe ou se vai criar 
            var entAtendimento = _atendimentoAppSvc.GetById(id_atendimento);
            if (entAtendimento == null) return new HttpNotFoundResult();

            ViewBag.HeaderText = $"Ata da reunião dia {entAtendimento.DataAgendada:dd/MM} na empresa: {entAtendimento.Filial.Nome}";
            ViewBag.Title = "Visualizar ATA";

            //verifica se é malandragem

            if (TipoUsuarioAtual == TipoUsuario.Cliente && !entAtendimento.UsuariosCliente.Any(x => x.id_usuario == User.Identity.GetUserId()))
            {
                return View("AcessoNegadoLogado", new ErroLogadoViewmodel { descricao = "Você tentou acessar um atendimento ao qual não possui acesso. Verifique suas permissões e tente novamente.", retorno = Url.Action("Index") });
            }

            ViewBag.PodeSalvar = TipoUsuarioAtual != TipoUsuario.Cliente;


            var model = Mapper.Map<Atendimento, ATAViewmodel>(entAtendimento);

            return View(model);

        }


        [HttpPost]
        [Authorize(Roles = "Superadmin,Administrador,Setor,Focus")]
        public async Task<ActionResult> ATA(ATAViewmodel model)
        {


            //encontra o atendimento 
            var atendimento = _atendimentoAppSvc.GetById(model.id_atendimento);
            if (atendimento == null) return new HttpNotFoundResult();

            ViewBag.HeaderText = $"Ata da reunião dia {atendimento.DataAgendada:dd/MM} na empresa: {atendimento.Filial.Nome}";



            if (model.Publicar)
            {
                atendimento.Publicado = DateTime.Now;
            }
            else
            {
                atendimento.Publicado = null;
            }

            atendimento.Presentes = model.Presentes;
            atendimento.Local = model.Local;
            atendimento.Resumo = model.Resumo;
            atendimento.HoraInicio = model.HoraInicio;
            atendimento.HoraFim = model.HoraFim;
            atendimento.id_autor = User.Identity.GetUserId();
            _atendimentoAppSvc.Update(atendimento);

            if (model.Publicar)
            {
                await DispararNotificacaoAtaPublicada(atendimento.id_atendimento);
            }

            #region PESQUISA 

            //verifica se já foi a pesquisa de satisfação 
            if (model.Publicar && !atendimento.RespostasPesquisas.Any())
            {
                //seleciona a pesquisa de satisfação 
                var pesquisas = _pesquisaAppSvc.GetAll().Where(x => x.Ativo).ToList();

                if ((!pesquisas.Any())) return RedirectToAction("ATA", new { model.id_atendimento });

                #region satisfação 

                //Pesquisa satisfação (cliente preenchido)
                if (atendimento.UsuariosCliente.Any() && pesquisas.Any(x => x.tp_pesquisa == TipoPesquisa.Satisfacao))
                {
                    var pesquisa_satisfacao =
                        pesquisas.Where(x => x.tp_pesquisa == TipoPesquisa.Satisfacao)
                            .OrderByDescending(x => x.Modificado)
                            .First();

                    foreach (var u in atendimento.UsuariosCliente)
                    {
                        var respSatisf = new RespostaPesquisa
                        {
                            id_atendimento = atendimento.id_atendimento,
                            DataEnvio = DateTime.Now,
                            id_pesquisa = pesquisa_satisfacao.id_pesquisa,
                            id_usuario = u.id_usuario
                        };
                        _respostaPesquisaAppSvc.Add(respSatisf);
                        await EnviarPesquisaSatisfacao("Satisfação", respSatisf.id_respostapesquisa,
                            _userManager.FindById(u.id_usuario).Email);
                    }
                }

                #endregion

                #region feedback 

                //Pesquisa feedback 
                if (atendimento.UsuariosFocus.Any() && pesquisas.Any(x => x.tp_pesquisa == TipoPesquisa.Feedback))
                {
                    var pesquisa_feedback =
                        pesquisas.Where(x => x.tp_pesquisa == TipoPesquisa.Feedback)
                            .OrderByDescending(x => x.Modificado)
                            .First();

                    foreach (var u in atendimento.UsuariosFocus)
                    {
                        var respFeedback = new RespostaPesquisa
                        {
                            id_atendimento = atendimento.id_atendimento,
                            DataEnvio = DateTime.Now,
                            id_pesquisa = pesquisa_feedback.id_pesquisa,
                            id_usuario = u.id_usuario
                        };
                        _respostaPesquisaAppSvc.Add(respFeedback);
                        await EnviarPesquisaSatisfacao("Feedback", respFeedback.id_respostapesquisa,
                            _userManager.FindById(u.id_usuario).Email);
                    }
                }

                #endregion

            }

            #endregion

            return RedirectToAction("ATA", new { model.id_atendimento });

        }


        #endregion

        #region ANEXOS

        [HttpGet]   //todos podem ver os anexos
        public JsonResult Anexos(int id)
        {
            Response.ClearHeaders();
            Response.ClearContent();
            Response.Clear();

            var atendimento = _atendimentoAppSvc.GetById(id);
            var model = Mapper.Map<ICollection<Anexo>, ICollection<AnexoViewmodel>>(atendimento.Anexos);


            return Json(model.Select(x => new { x.NomeArquivo, x.Mime, x.id_anexo, x.Tamanho }), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [Authorize(Roles = "Administrador,Superadmin,Focus,Setor")]
        public JsonResult Anexos(AnexoViewmodel model)
        {
            //carrrega a entidade
            var entidade = _atendimentoAppSvc.GetById(model.id_atendimento);
            if (entidade == null) return Json(new { status = "falha", mensagem = "Atendimento não encontrado" });

            if (Request.Files.Count == 0) return Json(new { status = "falha", mensagem = "Arquivo não fornecido" });

            //verificar se ele é o responsável pela ATA
            if (
                TipoUsuarioAtual == TipoUsuario.Administrador ||
                TipoUsuarioAtual == TipoUsuario.Superadmin ||
                TipoUsuarioAtual == TipoUsuario.Focus ||
                entidade.UsuariosFocus.Any(x => x.id_usuario == User.Identity.GetUserId()) ||
                entidade.id_autor == User.Identity.GetUserId()
                )
            {


                byte[] arrArquivo = null;
                var arq = Request.Files[0];



                using (var binaryReader = new BinaryReader(arq.InputStream))
                {
                    arrArquivo = binaryReader.ReadBytes(arq.ContentLength);
                }

                var objins = new Anexo
                {
                    id_atendimento = model.id_atendimento,
                    NomeArquivo = arq.FileName,
                    Mime = "application/octet-stream",
                    Tamanho = arrArquivo.LongLength,
                    Conteudo = arrArquivo,
                    Icone = arq.FileName.Split('.').Last()
                };

                _atendimentoAppSvc.AdicionarAnexo(objins);

                return Json(new { status = "sucesso", mensagem = "Anexo adicionado com sucesso." });
            }
            else
            {
                return Json(new { status = "falha", mensagem = "Acesso negado. O seu usuário não tem acesso para subir um documento para esta ATA." });
            }



        }


        public FileResult Anexo(int id)
        {
            Response.ClearHeaders();
            Response.ClearContent();
            Response.Clear();


            var entidade = _atendimentoAppSvc.GetAnexo(id);
            if (entidade == null) throw new HttpException(404, "Item não encontrado");

            return File(entidade.Conteudo, entidade.Mime, entidade.NomeArquivo);



        }


        [HttpPost]
        [Authorize(Roles = "Administrador,Superadmin,Focus,Setor")]
        public JsonResult RemoverAnexo(int id)
        {
            try
            {
                Response.ClearHeaders();
                Response.ClearContent();
                Response.Clear();

                var entidade = _atendimentoAppSvc.GetAnexo(id);
                if (entidade == null) throw new HttpException(404, "Item não encontrado");

                _atendimentoAppSvc.RemoverAnexo(entidade);
                return Json(new { status = "sucesso" });
            }
            catch (Exception ex)
            {
                return Json(new { status = "falha", mensagem = "Erro ao remover o anexo. o erro foi: " + ex.Message });
            }

        }

        #endregion

        #region MÉTODOS DE APOIO 

        public async Task<bool> DispararNotificacaoAtendimento(string Assunto, int id_atendimento)
        {

            //recarrega a entidade para trazer todos os campos
            var entidade = Mapper.Map<Atendimento, AtendimentoViewmodel>(_atendimentoAppSvc.GetById(id_atendimento));

            //monta a lista de estinatários 
            List<string> destinatarios = new List<string>();

            foreach (var u in entidade.UsuariosClienteSelecionados.Union(entidade.UsuariosFocusSelecionados))
            {
                destinatarios.Add(_userManager.FindById(u).Email);
            }

            //cria a notificação
            var notificacao = new Notification
            {
                Assunto = Assunto,
                Destinatarios = destinatarios.Distinct().ToList(),
                Valores = new List<Notification.ValoresTemplate>
                    {
                        new Notification.ValoresTemplate
                        {
                            Chave = "titulo",
                            Valor = Assunto
                        },
                        new Notification.ValoresTemplate
                        {
                            Chave = "assunto",
                            Valor = entidade.Assunto
                        },
                        new Notification.ValoresTemplate
                        {
                            Chave = "data",
                            Valor = entidade.DataAgendada.ToString("dd/MM/yy - HH:mm")
                        },
                        new Notification.ValoresTemplate
                        {
                            Chave = "cliente",
                            Valor = entidade.NomeEmpresa
                        },
                        new Notification.ValoresTemplate
                        {
                            Chave = "responsavel",
                            Valor = entidade.FuncionariosFocus
                        }
                    }
            };

            //dispara um parallel foreach pra enviar rápido
            await _NotificationAppSvc.DispararNotificacaoAsync(notificacao, TipoEntregaNotificacao.EmailTemplate,
                    "agendamento");

            return true;


        }

        public async Task<bool> DispararNotificacaoAtaPublicada(int id_atendimento)
        {
            //recarrega a entidade para trazer todos os campos
            var entidade = _atendimentoAppSvc.GetById(id_atendimento);

            //monta a lista de estinatários 
            List<string> destinatarios = new List<string>();

            foreach (var u in entidade.UsuariosCliente.Union(entidade.UsuariosFocus))
            {
                destinatarios.Add(_userManager.FindById(u.id_usuario).Email);
            }

            var notificacao = new Notification
            {
                Assunto = "Nova ata publicada!",
                Destinatarios = destinatarios.Distinct().ToList(),
                Valores = new List<Notification.ValoresTemplate>
                    {
                        new Notification.ValoresTemplate
                        {
                            Chave = "titulo",
                            Valor = "Nova ata publicada!"
                        },
                        new Notification.ValoresTemplate
                        {
                            Chave = "presentes",
                            Valor = entidade.Presentes.Replace("\n", "<br/>")
                        },
                        new Notification.ValoresTemplate
                        {
                            Chave = "local",
                            Valor = entidade.Local
                        },
                        new Notification.ValoresTemplate
                        {
                            Chave = "data",
                            Valor = entidade.DataAgendada.ToString("dd/MM/yyyy")
                        },
                        new Notification.ValoresTemplate
                        {
                            Chave = "inicio",
                            Valor = entidade.HoraInicio
                        },
                        new Notification.ValoresTemplate
                        {
                            Chave = "fim",
                            Valor = entidade.HoraFim
                        },
                        new Notification.ValoresTemplate
                        {
                            Chave = "resumo",
                            Valor = entidade.Resumo
                        },
                        new Notification.ValoresTemplate
                        {
                            Chave = "link",
                            Valor =
                                Url.Action("ata", "atendimentos", new {entidade.id_atendimento},
                                    protocol: Request.Url.Scheme)
                        }

                    }
            };

            //dispara a notificação 
            await _NotificationAppSvc.DispararNotificacaoAsync(notificacao, TipoEntregaNotificacao.EmailTemplate, "ata");

            return true;

        }

        public async Task<bool> EnviarPesquisaSatisfacao(string tipo, int id_resposta, string emaildestino)
        {
            var notificacao = new Notification
            {
                Assunto = $"Pesquisa de {tipo} disponível!",

                Destinatarios = new List<string> { emaildestino },
                Valores = new List<Notification.ValoresTemplate>
                    {
                        new Notification.ValoresTemplate
                        {
                            Chave = "tipo",
                            Valor = tipo
                        },
                        new Notification.ValoresTemplate
                        {
                            Chave = "link",
                            Valor =
                                Url.Action("index", "responderpesquisa", new { id = id_resposta },
                                    protocol: Request.Url.Scheme)
                        }

                    }
            };

            //dispara a notificação 
            await _NotificationAppSvc.DispararNotificacaoAsync(notificacao, TipoEntregaNotificacao.EmailTemplate, "pesquisa");


            return true;



        }

        #endregion

        #region ACOMPANHAMENTO
        [Authorize]
        public ActionResult Acompanhamento(int? id_ano, string filtro = "*")
        {
            if (filtro == string.Empty) filtro = "*";

            if (!id_ano.HasValue) id_ano = DateTime.Now.Year;

            var model = new AcompanhamentoViewmodel();


            var usuario = _userAppSvc.GetById(User.Identity.GetUserId());




            //caso seja cliente trava só os atendimentos do cliente 
            if (TipoUsuarioAtual == TipoUsuario.Cliente)
            {
                model.AcompDetalhe = _atendimentoAppSvc.GetAcompanhamentoDetalhe().Where(x => x.ano == id_ano && usuario.id_empresa == x.id_empresa).ToList();
            }
            else
            {
                model.AcompDetalhe = _atendimentoAppSvc.GetAcompanhamentoDetalhe().Where(x => x.ano == id_ano).ToList();
            }




            //carrega os dados de visitações.
            if (filtro != "*")
            {
                model.Acomp =
                    _atendimentoAppSvc.GetAcompanhamentoMacro()
                        .Where(x => x.id_empresa == (TipoUsuarioAtual == TipoUsuario.Cliente ? usuario.id_empresa : x.id_empresa) && x.grupo.ToUpper().Contains(filtro.ToUpper()) || x.nome_empresa.ToUpper().Contains(filtro.ToUpper())).ToList();
            }
            else
            {
                model.Acomp = _atendimentoAppSvc.GetAcompanhamentoMacro().Where(x => x.id_empresa == (TipoUsuarioAtual == TipoUsuario.Cliente ? usuario.id_empresa : x.id_empresa)).ToList();

            }





            ViewBag.filtro = filtro == "*" ? "" : filtro;
            ViewBag.HeaderText = "Acompanhamento de atendimentos " + id_ano.ToString();

            return View(model);
        }



        #endregion


    }
}
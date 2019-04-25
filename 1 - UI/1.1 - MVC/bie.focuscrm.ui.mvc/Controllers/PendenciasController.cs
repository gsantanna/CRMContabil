using System;
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
    public class PendenciasController : BaseController
    {

        #region Ctor

        private readonly IPendenciaAppService _pendenciaAppSvc;
        private readonly IAtendimentoAppService _atendimentoAppSvc;
        private readonly IUsuarioAppService _usuarioAppSvc;
        private readonly ISetorAppService _setorAppSvc;
        private readonly INotificationAppService _NotificationAppSvc;


        public PendenciasController(IPendenciaAppService svc1, IAtendimentoAppService svc2, IUsuarioAppService svc3, ISetorAppService svc4, INotificationAppService svc5)
        {
            _pendenciaAppSvc = svc1;
            _atendimentoAppSvc = svc2;
            _usuarioAppSvc = svc3;
            _setorAppSvc = svc4;
            _NotificationAppSvc = svc5;
        }
        #endregion

        #region API 
        public JsonResult2 GetJson(int? id_atendimento, int? id_setor)
        {
            Response.ClearHeaders();
            Response.ClearContent();
            Response.Clear();

            var model = new List<PendenciaViewmodel>();

            if (id_atendimento.HasValue)
            {
                var entidade = _atendimentoAppSvc.GetById(id_atendimento.Value);
                if (entidade == null) return new JsonResult2 { Data = new { status = "falha", mensagem = "Atendimento não encontrado ou usuário sem acesso ao mesmo" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                model = Mapper.Map<IEnumerable<Pendencia>, IEnumerable<PendenciaViewmodel>>(entidade.Pendencias).ToList();
            }
            else if (id_setor.HasValue)
            {
                var entidade = _setorAppSvc.GetById(id_setor.Value);
                if (entidade == null) return new JsonResult2 { Data = new { status = "falha", mensagem = "setor não encontrado ou usuário sem acesso ao mesmo" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                model = new List<PendenciaViewmodel>();
                foreach (var item in entidade.Funcionarios)
                {
                    model.AddRange(Mapper.Map<IEnumerable<Pendencia>, IEnumerable<PendenciaViewmodel>>(item.Pendencias.Where(x => x.Status != "Concluído").ToList()));
                }


            }
            else //todos os itens 
            {
                model = Mapper.Map<IEnumerable<Pendencia>, IEnumerable<PendenciaViewmodel>>(_pendenciaAppSvc.GetAll()).ToList();
            }

            return new JsonResult2 { Data = new { data = model }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }

        [Authorize(Roles = "Administrador,Superadmin,Focus,Setor")]
        [HttpPost]
        public JsonResult Adicionar(PendenciaViewmodel model)
        {
            try
            {
                Response.ClearHeaders();
                Response.ClearContent();
                Response.Clear();

                var entidade = Mapper.Map<PendenciaViewmodel, Pendencia>(model);
                _pendenciaAppSvc.Add(entidade);

                return Json(new { status = "sucesso", mensagem = "Operação concluída com sucesso", entidade.id_pendencia });
            }
            catch (Exception ex)
            {
                return Json(new { status = "falha", mensagem = "Erro ao inserir pendencia. o erro foi: " + ex.Message });
            }

        }

        [Authorize(Roles = "Administrador,Superadmin,Focus,Setor")]
        [HttpPost]
        public JsonResult Deletar(int id)
        {
            try
            {
                var model = _pendenciaAppSvc.GetById(id);
                if (model == null) throw new HttpException(404, "Item não encontrado");

                //só pode deletar a pendencia se ela foi criada por ele, OU se for administrador
                if (TipoUsuarioAtual == TipoUsuario.Setor && model.id_responsavel != User.Identity.GetUserId())
                {
                    return Json(new { status = "falha", mensagem = "Você não tem permissão para remover esta pendencia." });

                }

                _pendenciaAppSvc.Remove(model);
                return Json(new { status = "sucesso" });
            }
            catch (Exception ex)
            {
                return Json(new { status = "falha", mensagem = "Erro ao deletar a pendencia. o erro foi: " + ex.Message });
            }

        }

        [HttpPost]
        public JsonResult Comentar(ComentarioViewmodel model)
        {
            try
            {
                var entidade = _pendenciaAppSvc.GetById(model.id_pendencia);
                if (entidade == null)
                    throw new Exception(
                        "A pendencia não foi localizada. Você pode não ter acesso ou ela pode ter sido excluída por outro usuário");


                entidade.Comentarios.Add(new Comentario
                {
                    Conteudo = model.Conteudo,
                    Data = DateTime.Now,
                    id_autor = User.Identity.GetUserId(),
                    id_pendencia = entidade.id_pendencia
                });

                _pendenciaAppSvc.Update(entidade);
                return Json(new { status = "sucesso" });
            }
            catch (Exception ex)
            {
                return Json(new { status = "falha", mensagem = ex.Message });

            }
        }

        [HttpPost]
        public JsonResult RemoverComentario(int id)
        {
            try
            {
                //só pode remover se for o dono ou se for o administrador
                var entidade = _pendenciaAppSvc.GetComentario(id);
                if (entidade == null) throw new Exception("Comentário não disponível");

                if (TipoUsuarioAtual == TipoUsuario.Administrador || TipoUsuarioAtual == TipoUsuario.Superadmin ||
                    entidade.id_autor == User.Identity.GetUserId())
                {

                    _pendenciaAppSvc.RemoverComentario(id);
                    return Json(new { status = "sucesso" });

                }
                else
                {
                    throw new Exception("Você não tem acesso para remover este comentário");
                }

            }
            catch (Exception ex)
            {
                return Json(new { status = "falha", mensagem = ex.Message });
            }

        }


        public JsonResult GetComentarios(int id)
        {
            try
            {
                var entidade = _pendenciaAppSvc.GetById(id);
                if (entidade == null) throw new Exception("Pendencia não encontrada");


                var id_usuarioatual = User.Identity.GetUserId();


                return Json(entidade.Comentarios.OrderByDescending(x => x.Data).Select(x => new
                {
                    data = x.Data.ToString("dd/MM HH:mm"),
                    conteudo = x.Conteudo,
                    nomeautor = x.Usuario.Nome,
                    id_comentario = x.id_pendenciacomentario,
                    editar = x.id_autor == id_usuarioatual || TipoUsuarioAtual == TipoUsuario.Administrador || TipoUsuarioAtual == TipoUsuario.Superadmin

                }).ToArray(), JsonRequestBehavior.AllowGet);





            }
            catch (Exception ex)
            {
                return Json(new { status = "falha", mensagem = ex.Message });
            }

        }


        #endregion

        #region CRUD 


        public ActionResult Index(int? id_atendimento, int? id_setor)
        {
            ViewBag.PodeCriar = TipoUsuarioAtual != TipoUsuario.Cliente;
            ViewBag.id_setor = id_setor;
            ViewBag.id_atendimento = id_atendimento;

            return View();
        }


        public ActionResult Visualizar(int id, int? id_atendimento, bool? FromATA)
        {
            #region Preparacao 

            //carrega a entidade 
            var entidade = _pendenciaAppSvc.GetById(id);
            if (entidade == null) return new HttpNotFoundResult();


            var model = Mapper.Map<Pendencia, PendenciaViewmodel>(entidade);

            model.id_atendimentopassado = id_atendimento.HasValue;

            ViewBag.Admin = TipoUsuarioAtual == TipoUsuario.Administrador || TipoUsuarioAtual == TipoUsuario.Superadmin;
            ViewBag.id_usuario = User.Identity.GetUserId();
            ViewBag.id_atendimento = id_atendimento;
            ViewBag.FromATA = FromATA;


            return View(model);

            #endregion

        }



        #region CRIAR 

        [HttpGet]
        [Authorize(Roles = "Superadmin,Administrador,Focus,Setor")]
        public ActionResult Criar(int? id_atendimento)
        {
            #region Preparacao 

            var model = new PendenciaViewmodel { Prazo = DateTime.Now.Date };

            if (!id_atendimento.HasValue)
            //caso não passe na  querystring o ID do atendimento, vai aparecer um combo pro usuário escolher
            {
                var atendimentos = _atendimentoAppSvc.GetAll().Select(x => new { x.id_atendimento, Assunto = x.DataAgendada.ToString("dd/MM") + " - " + x.Assunto });
                ViewBag.Atendimentos = new SelectList(atendimentos, "id_atendimento", "Assunto");
                model.id_atendimento = -1;
                model.id_atendimentopassado = false;

            }
            else
            {
                var atendimento = _atendimentoAppSvc.GetById(id_atendimento.Value);
                if (atendimento == null) return new HttpNotFoundResult();
                model.id_atendimento = atendimento.id_atendimento;
                model.AssuntoAtendimento = atendimento.DataAgendada.ToString("dd/MM") + " - " + atendimento.Assunto;
                model.id_atendimentopassado = true;

            }



            if (TipoUsuarioAtual == TipoUsuario.Setor)
            //Se for setor só exibe ele mesmo como responsável //por garantia vou marretar o valor no post
            {
                //somente setor do cara 
                var usuarioAtual = _usuarioAppSvc.GetById(User.Identity.GetUserId());
                List<UsuarioViewmodel> UsuariosSetor = new List<UsuarioViewmodel>();

                foreach (var item in usuarioAtual.Setores)
                {
                    UsuariosSetor.AddRange(
                        item.Funcionarios.Select(x => new UsuarioViewmodel { id_usuario = x.id_usuario, Nome = x.Nome }));
                }

                ViewBag.Usuarios = new SelectList(UsuariosSetor.Distinct(), "id_usuario", "Nome");

            }
            else
            {
                ViewBag.Usuarios = new SelectList(_usuarioAppSvc.GetAll().Where(f => f.Ativo), "id_usuario", "Nome");
            }

            ViewBag.TipoStatus = new SelectList(Pendencia.TipoStatus);




            #endregion


            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Superadmin,Administrador,Focus,Setor")]
        public async System.Threading.Tasks.Task<ActionResult> Criar(PendenciaViewmodel model)
        {

            try
            {
                if (!ModelState.IsValid) throw new Exception("Erro na validação dos dados");


                //tenta inserir o cara
                var entidade = Mapper.Map<PendenciaViewmodel, Pendencia>(model);
                entidade.Criado = DateTime.Now;
                entidade.Modificado = DateTime.Now;


                entidade.Historico =
                    $"Item criado em {DateTime.Now:dd/MM/yyyy HH:mm:ss} Por: {Helpers.IdentityHelper.GetCurrentUser().Nome}<br/>";
                _pendenciaAppSvc.Add(entidade);


                #region Notificacao 
                //carrega o atendimento 
                var atend = _atendimentoAppSvc.GetById(entidade.id_atendimento);

                //async perde o path navigator
                var emailResp = _usuarioAppSvc.GetById(entidade.id_responsavel).Email;



                var dest = atend.UsuariosFocus.Select(x => x.Email).Union(atend.UsuariosCliente.Select(x => x.Email)).ToList();
                dest.Add(emailResp);

                //cria a notificação
                var notificacao = new Notification
                {
                    Assunto = "Pendencia criada!",
                    Destinatarios = dest.Distinct().ToList(),
                    Valores = new List<Notification.ValoresTemplate>
                    {
                        new Notification.ValoresTemplate
                        {
                            Chave = "titulo",
                            Valor = "Uma pendência acaba de ser adicionada no sistema!"
                        },
                        new Notification.ValoresTemplate
                        {
                            Chave = "conteudo",
                            Valor = entidade.Titulo
                        },
                        new Notification.ValoresTemplate
                        {
                            Chave = "status",
                            Valor = entidade.Status
                        },
                        new Notification.ValoresTemplate
                        {
                            Chave = "id",
                            Valor = entidade.id_pendencia.ToString()
                        }
                    }
                };

                try
                {
                    //dispara um parallel foreach pra enviar rápido
                    await _NotificationAppSvc.DispararNotificacaoAsync(notificacao, TipoEntregaNotificacao.EmailTemplate,
                            "pendencia");
                }
                catch (Exception ex)
                {
                    //TODO: Verificar o porque da notificação não estar funcionando --> AsignTo: José Silva Jr
                }


                #endregion



                if (model.id_atendimentopassado)
                {
                    return RedirectToAction("ata", "atendimentos", new { model.id_atendimento });
                }
                else
                {
                    return RedirectToAction("index");
                }

            }
            catch (Exception ex) //algum problema
            {
                ModelState.AddModelError("Erro ao adicionar a pendência", ex.Message);

                #region Preparacao 
                if (!model.id_atendimentopassado)
                //caso não passe na  querystring o ID do atendimento, vai aparecer um combo pro usuário escolher
                {
                    var atendimentos = _atendimentoAppSvc.GetAll();
                    ViewBag.Atendimentos = new SelectList(atendimentos, "id_atendimento", "Assunto", model.id_atendimento);
                }
                else
                {
                    var atendimento = _atendimentoAppSvc.GetById(model.id_atendimento);
                    if (atendimento == null) return new HttpNotFoundResult();
                    model.id_atendimento = atendimento.id_atendimento;
                    model.AssuntoAtendimento = atendimento.Assunto;
                    model.id_atendimentopassado = true;
                }


                ViewBag.TipoUsuarioAtual = TipoUsuarioAtual.ToString();

                if (TipoUsuarioAtual == TipoUsuario.Setor)
                //Se for setor só exibe ele mesmo como responsável //por garantia vou marretar o valor no post
                {
                    //somente setor do cara 
                    var Setores = _usuarioAppSvc.GetById(User.Identity.GetUserId()).Setores.Where(x => x.Ativo);
                    ViewBag.Setores = new SelectList(Setores, "id_setor", "Nome");
                }
                else
                {
                    ViewBag.Usuarios = new SelectList(_usuarioAppSvc.GetAll().Where(f => f.Ativo), "id_usuario", "Nome");
                    ViewBag.Setores = new SelectList(_usuarioAppSvc.GetAll().Where(f => f.Ativo), "id_setor", "Nome");
                }

                ViewBag.TipoStatus = new SelectList(Pendencia.TipoStatus);


                return View(model);

                #endregion


            }



        }

        #endregion



        #region EDITAR 


        [Authorize(Roles = "Superadmin,Administrador,Focus,Setor")]
        [HttpGet]
        public ActionResult Editar(int id, int? id_atendimento)
        {
            #region Preparacao 

            //carrega a entidade 
            var entidade = _pendenciaAppSvc.GetById(id);
            if (entidade == null) return new HttpNotFoundResult();

            //se for usuario SETOR e não for o responsável. não pode editar
            if (TipoUsuarioAtual == TipoUsuario.Setor && entidade.id_responsavel != User.Identity.GetUserId()) return RedirectToAction("visualizar", "pendencias", new { id, id_atendimento });


            var model = Mapper.Map<Pendencia, PendenciaViewmodel>(entidade);

            model.id_atendimentopassado = id_atendimento.HasValue;

            if (TipoUsuarioAtual != TipoUsuario.Setor)
            {
                ViewBag.Usuarios = new SelectList(_usuarioAppSvc.GetAll().Where(f => f.Ativo), "id_usuario", "Nome");
            }

            ViewBag.TipoStatus = new SelectList(Pendencia.TipoStatus, model.Status);
            ViewBag.TipoUsuarioAtual = TipoUsuarioAtual;

            return View(model);

            #endregion

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Superadmin,Administrador,Focus,Setor")]
        public async System.Threading.Tasks.Task<ActionResult> Editar(PendenciaViewmodel model)
        {
            //carrega a entidade 
            var entidade = _pendenciaAppSvc.GetById(model.id_pendencia);
            if (entidade == null) return new HttpNotFoundResult();

            #region Preparacao 
            //caso de algum problema processa os requisitos da pagina para renderização
            if (TipoUsuarioAtual != TipoUsuario.Setor)
            {
                ViewBag.Usuarios = new SelectList(_usuarioAppSvc.GetAll().Where(f => f.Ativo), "id_usuario", "Nome");
            }

            ViewBag.TipoStatus = new SelectList(Pendencia.TipoStatus, model.Status);
            ViewBag.TipoUsuarioAtual = TipoUsuarioAtual;

            #endregion

            //se for usuario SETOR e não for o responsável. não pode editar
            if (TipoUsuarioAtual == TipoUsuario.Setor && entidade.id_responsavel != User.Identity.GetUserId())
            {
                ModelState.AddModelError(string.Empty,
                    "Acesso negado! - Você não tem permissão para modificar este item!");
                return View(model);
            }


            //atualiza o item 
            entidade.Historico +=
                $"<br/>Pendencia modificada por:{Helpers.IdentityHelper.GetCurrentUser().Nome} as {DateTime.Now:dd/MM/yyyy HH:mm:ss}";

            if (entidade.Status != model.Status) entidade.Historico += $"Status anterior: {entidade.Status} Novo status: {model.Status} <br/>";
            if (entidade.Titulo != model.Titulo) entidade.Historico += $"Título anterior: {entidade.Status} Novo Título: {model.Status} <br/>";
            if (entidade.Prazo != model.Prazo) entidade.Historico += $"Prazo anterior: {entidade.Prazo:dd/MM/yyyy HH:mm} Novo Prazo: {model.Prazo:dd/MM/yyyy HH:mm} <br/>";

            if (entidade.id_responsavel != model.id_responsavel)
            {
                var novoResp = _usuarioAppSvc.GetById(model.id_responsavel).Nome;
                entidade.Historico += $"Responsável Anterior:{entidade.Responsavel.Nome} Novo Responsável: {novoResp} ";
            }






            entidade.Status = model.Status;
            entidade.Titulo = model.Titulo;
            entidade.Prazo = model.Prazo;
            entidade.Modificado = DateTime.Now;
            entidade.id_responsavel = model.id_responsavel;

            try
            {
                _pendenciaAppSvc.Update(entidade);



                #region Notificacao 
                //carrega o atendimento 
                var atend = _atendimentoAppSvc.GetById(entidade.id_atendimento);
                var emailResp = _usuarioAppSvc.GetById(entidade.id_responsavel).Email;

                var dest = atend.UsuariosFocus.Select(x => x.Email).Union(atend.UsuariosCliente.Select(x => x.Email)).ToList();
                dest.Add(emailResp);

                //cria a notificação
                var notificacao = new Notification
                {
                    Assunto = "Pendencia modificada!",
                    Destinatarios = dest.Distinct().ToList(),
                    Valores = new List<Notification.ValoresTemplate>
                    {
                        new Notification.ValoresTemplate
                        {
                            Chave = "titulo",
                            Valor = "Uma pendência ao qual você faz parte acaba de ser modificada no sistema!"
                        },
                        new Notification.ValoresTemplate
                        {
                            Chave = "conteudo",
                            Valor = entidade.Titulo
                        },
                        new Notification.ValoresTemplate
                        {
                            Chave = "status",
                            Valor = entidade.Status
                        },
                        new Notification.ValoresTemplate
                        {
                            Chave = "id",
                            Valor = entidade.id_pendencia.ToString()
                        }
                    }
                };

                try
                {
                    //dispara um parallel foreach pra enviar rápido
                    await _NotificationAppSvc.DispararNotificacaoAsync(notificacao, TipoEntregaNotificacao.EmailTemplate,
                            "pendencia");
                }
                catch (Exception ex)
                {
                    //TODO: Verificar o porque da notificação não estar funcionando --> AsignTo: José Silva Jr
                }


                #endregion

















                if (model.id_atendimentopassado)
                {
                    return RedirectToAction("ata", "atendimentos", new { model.id_atendimento });
                }
                else
                {
                    return RedirectToAction("index");
                }


            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(model);


        }







        #endregion





        #endregion //crud

    }
}
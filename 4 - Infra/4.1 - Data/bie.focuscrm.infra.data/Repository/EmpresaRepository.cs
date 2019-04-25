using System.Data.Entity;
using System.Linq;
using bie.focuscrm.domain.Entities;
using bie.focuscrm.domain.Interfaces.Repository;

namespace bie.focuscrm.infra.data.Repository
{
    public class EmpresaRepository : RepositoryBase<Empresa>, IEmpresaRepository
    {
        public new void Add(Empresa obj)
        {
            var usu = obj.Usuarios.ToList();

            obj.Usuarios.Clear();

            foreach (var u in usu)
                obj.Usuarios.Add(Db.Set<Usuario>().Find(u.id_usuario));

            Db.Set<Empresa>().Add(obj);

            Db.SaveChanges();
        }

        public new void Update(Empresa obj)
        {
            var entidade = Db.Set<Empresa>().Find(obj.id_empresa);
            
            // funcionarios
            var remover = entidade.Usuarios.Except(obj.Usuarios);

            foreach(var r in remover.ToList())
            {
                entidade.Usuarios.Remove(r);

                Db.SaveChanges();
            }
            
            foreach (var item in obj.Usuarios.ToList())
            {
                if(entidade.Usuarios.Where(x => x.id_usuario == item.id_usuario).Count() > 0)
                {
                    entidade.Usuarios.Remove(item);

                    Db.SaveChanges();
                }

                entidade.Usuarios.Add(Db.Set<Usuario>().Find(item.id_usuario));

                Db.SaveChanges();
            }

            // parametros de agendamento
            foreach (var p in obj.ParametrosAgendamento.ToList())
            {
                if (entidade.ParametrosAgendamento.Where(x => x.id_setor == p.id_setor).Count() > 0)
                {
                    entidade.ParametrosAgendamento.Remove(p);

                    Db.SaveChanges();
                }

                Db.Set<ParametroAgendamento>().Add(p);

                Db.SaveChanges();
            }

            entidade.Nome = obj.Nome;
            entidade.CNPJ = obj.CNPJ;
            entidade.RazaoSocial = obj.RazaoSocial;
            entidade.Grupo = obj.Grupo; 
            entidade.QtdFiliais = obj.QtdFiliais;
            entidade.QtdPrestadores = obj.QtdPrestadores;
            entidade.MetrosQuadrados = obj.MetrosQuadrados;
            entidade.Socio = obj.Socio;
            entidade.TelefoneSocio = obj.TelefoneSocio;
            entidade.EmailSocio = obj.EmailSocio;
            entidade.Site = obj.Site;
            entidade.NumeroCheckout = obj.NumeroCheckout;
            entidade.Atividade = obj.Atividade;
            entidade.InscEstadual = obj.InscEstadual;
            entidade.QtdFuncionarios = obj.QtdFuncionarios;
            entidade.Bancos = obj.Bancos;
            entidade.Endividamento = obj.Endividamento;
            entidade.AcoesFiscais = obj.AcoesFiscais;
            entidade.RegimeTributacao = obj.RegimeTributacao;
            entidade.SistemaRetaguarda = obj.SistemaRetaguarda;
            entidade.DiaRetiradaDocumentos = obj.DiaRetiradaDocumentos;
            entidade.RespostaWS = obj.RespostaWS;
            entidade.QtdNFS = obj.QtdNFS;
            entidade.Logradouro = obj.Logradouro;
            entidade.Numero = obj.Numero;
            entidade.Complemento = obj.Complemento;
            entidade.Bairro = obj.Bairro;
            entidade.Cidade = obj.Cidade;
            entidade.UF = obj.UF;
            entidade.CEP = obj.CEP;
            entidade.Telefone = obj.Telefone;
            entidade.OBS = obj.OBS;

            Db.SaveChanges();
        }
        
        public new void Remove(Empresa obj)
        {
            // filiais
            foreach (var f in obj.Filiais.ToList())
            {
                var atendimentos = Db.Set<Atendimento>().Where(x => x.id_filial == f.id_filial).ToList();

                foreach (var a in atendimentos)
                {
                    var pendencias = Db.Set<Pendencia>().Where(x => x.id_atendimento == a.id_atendimento).ToList();

                    foreach(var p in pendencias)
                    {
                        Db.Set<Pendencia>().Remove(p);

                        Db.SaveChanges();
                    }

                    Db.Set<Atendimento>().Remove(a);

                    Db.SaveChanges();
                }

                Db.Set<Filial>().Remove(f);

                Db.SaveChanges();
            }

            // usuarios
            foreach (var u in obj.Usuarios.ToList())
            {
                // pendencias (caso nao exista atendimento e/ou filial, garante a exclusao dos orfaos)
                var pendencias = Db.Set<Pendencia>().Where(x => x.id_responsavel == u.id_usuario).ToList();

                foreach (var p in pendencias)
                {
                    Db.Set<Pendencia>().Remove(p);

                    Db.SaveChanges();
                }

                // antendimentos (caso nao exista filial, garante a exclusao dos orfaos)
                //var atendimentos = Db.Set<Atendimento>().Where(x => x.id_usuariocliente == u.id_usuario).ToList();

                //foreach (var a in atendimentos)
                //{
                //    Db.Set<Atendimento>().Remove(a);

                //    Db.SaveChanges();
                //}

                //Db.Set<Usuario>().Remove(u);

                //Db.SaveChanges();
            }
            
            // parametros agendamento
            foreach (var p in obj.ParametrosAgendamento.ToList())
            {
                Db.Set<ParametroAgendamento>().Remove(p);

                Db.SaveChanges();
            }

            // empresa
            Db.Set<Empresa>().Remove(obj);

            Db.SaveChanges();
        }

        public void RemoveFilial(Empresa obj, int id)
        {
            foreach (var f in obj.Filiais.ToList())
            {
                if (f.id_filial == id)
                {
                    Db.Set<Filial>().Remove(f);

                    Db.SaveChanges();
                }
            }
        }
    }
}
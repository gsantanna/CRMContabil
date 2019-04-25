using System;
using System.Collections.Generic;
using bie.focuscrm.domain.Entities;
using bie.focuscrm.domain.Entities.StoredProcedures;
using bie.focuscrm.domain.Interfaces.Repository;
using System.Linq;

namespace bie.focuscrm.infra.data.Repository
{
    public class AtendimentoRepository : RepositoryBase<Atendimento>, IAtendimentoRepository
    {
        public void AdicionarAnexo(Anexo anexo)
        {
            Db.Set<Anexo>().Add(anexo);
            Db.SaveChanges();
        }



        public IEnumerable<AcompanhamentoAtendimentoDetalhe> GetAcompanhamentoDetalhe()
        {
            // return new List<AcompanhamentoAtendimentoDetalhe>();
            return Db.Database.SqlQuery<AcompanhamentoAtendimentoDetalhe>("SP_GetRelatorioAcompanhamentoDetalhe").ToList();
        }

        public IEnumerable<AcompanhamentoAtendimentoMacro> GetAcompanhamentoMacro()
        {
            return Db.Database.SqlQuery<AcompanhamentoAtendimentoMacro>("SP_GetRelatorioAcompanhamentoMacro").ToList();
        }

        public Anexo GetAnexo(int id)
        {
            return Db.Set<Anexo>().Find(id);
        }

        public void RemoverAnexo(Anexo anexo)
        {
            var entidade = Db.Set<Anexo>().Find(anexo.id_anexo);
            Db.Set<Anexo>().Remove(entidade);
            Db.SaveChanges();
        }

        public new void Update(Atendimento obj)
        {
            var uc = obj.UsuariosCliente.Select(x => x.id_usuario).ToList();
            var uf = obj.UsuariosFocus.Select(x => x.id_usuario).ToList();

            obj.UsuariosFocus.Clear();
            obj.UsuariosCliente.Clear();

            foreach (var u in uc)
            {
                obj.UsuariosCliente.Add(Db.Set<Usuario>().Find(u));
            }

            foreach (var u in uf)
            {
                obj.UsuariosFocus.Add(Db.Set<Usuario>().Find(u));
            }

            Db.SaveChanges();


        }

        public new void Add(Atendimento obj)
        {
            var uc = obj.UsuariosCliente.Select(x => x.id_usuario).ToList();
            var uf = obj.UsuariosFocus.Select(x => x.id_usuario).ToList();

            obj.UsuariosFocus.Clear();
            obj.UsuariosCliente.Clear();

            foreach (var u in uc)
            {
                obj.UsuariosCliente.Add(Db.Set<Usuario>().Find(u));
            }

            foreach (var u in uf)
            {
                obj.UsuariosFocus.Add(Db.Set<Usuario>().Find(u));
            }


            Db.Set<Atendimento>().Add(obj);
            Db.SaveChanges();
        }

        public new void Remove(Atendimento obj)
        {

            foreach(var pesq in Db.Set<RespostaPesquisa>().Where(x=> x.id_atendimento ==obj.id_atendimento).ToList())
            {
                foreach(var val in pesq.Valores.ToList())
                {
                    Db.Set<RespostaPesquisaValor>().Remove(val);
                    Db.SaveChanges();
                }

                Db.Set<RespostaPesquisa>().Remove(pesq);
                Db.SaveChanges();
            }








            foreach (var pend in  Db.Set<Pendencia>().Where(   x=> x.id_atendimento == obj.id_atendimento).ToList())
            {
                Db.Set<Pendencia>().Remove(pend);
                Db.SaveChanges();
            }

            Db.Set<Atendimento>().Remove(obj);
            Db.SaveChanges();

        }




    }
}
using System.Data.Entity;
using System.Linq;
using bie.focuscrm.domain.Entities;
using bie.focuscrm.domain.Interfaces.Repository;

namespace bie.focuscrm.infra.data.Repository
{
    public class SetorRepository : RepositoryBase<Setor>, ISetorRepository
    {

        public new void Update(Setor obj)
        {

            //Carrega os valores originais do item
            var entidade = Db.Set<Setor>().Find(obj.id_setor);
            entidade.Funcionarios.Clear();
            Db.SaveChanges();

            foreach (var item in obj.Funcionarios)
            {
                entidade.Funcionarios.Add(Db.Set<Usuario>().Find(item.id_usuario));
            }

            entidade.Nome = obj.Nome;
            entidade.id_usuarioresponsavel = obj.id_usuarioresponsavel;
            //entidade.Ativo = obj.Ativo; 
            Db.SaveChanges();

        }




        public new void Add(Setor obj)
        {


            //grava uma lista de funcionarios 
            var func = obj.Funcionarios.ToList();

            //limpa os funcionarios que vieram pois podem ter vindo de outro contexto 
            obj.Funcionarios.Clear();


            //para cada item na lista que foi gravada no passo 1
            foreach (var f in func)
            {
                //insere o objeto novo ( conectado) 
                obj.Funcionarios.Add(Db.Set<Usuario>().Find(f.id_usuario));
            }

            //insere o setor
            Db.Set<Setor>().Add(obj);

            //salva
            Db.SaveChanges();




        }

        public new void Remove(Setor obj)
        {
            var func = obj.Funcionarios.ToList();

            obj.Funcionarios.Clear();
            obj.Atendimentos.Clear();

            foreach (var f in func)
            {

                foreach (var r in f.Pesquisas.ToList())
                {
                    foreach (var s in r.Valores.ToList())
                    {
                        Db.Set<RespostaPesquisaValor>().Remove(s);

                        Db.SaveChanges();
                    }

                    Db.Set<RespostaPesquisa>().Remove(r);

                    Db.SaveChanges();
                }

                //Db.Set<Usuario>().Remove(f);

                //Db.SaveChanges();
            }

            Db.Set<Setor>().Remove(obj);

            Db.SaveChanges();
        }






    }




}
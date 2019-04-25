using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using bie.focuscrm.infra.data.Configurations;
using bie.focuscrm.domain.Entities.StoredProcedures;

namespace bie.focuscrm.infra.data.Context
{
    public class MainDataContext : DbContext
    {
        public MainDataContext() : base("MainConnectionString")
        {
            //TODO : Veriry why we need this horrible hack to ensure Entities SQL dll is loaded.
            //source: http://robsneuron.blogspot.in/2013/11/entity-framework-upgrade-to-6.html
            var ensureDllIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;

            Database.Log = Console.Write;
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.IsOptional());

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(300));

            modelBuilder.Properties<string>().Configure(p => p.IsUnicode(false));
                

            modelBuilder.Configurations.Add(new AtendimentoConfiguration());

            modelBuilder.Configurations.Add(new ParametroAgendamentoConfiguration());

            modelBuilder.Configurations.Add(new PendenciaConfiguration());

            modelBuilder.Configurations.Add(new PendenciaComentarioConfiguration());

            modelBuilder.Configurations.Add(new SetorConfiguration());

            modelBuilder.Configurations.Add(new AnexoAtaConfiguration());

            modelBuilder.Configurations.Add(new EmpresaConfiguration());

            modelBuilder.Configurations.Add(new FilialConfiguration());

            modelBuilder.Configurations.Add(new UsuarioConfiguration());

            modelBuilder.Configurations.Add(new AlertaConfiguration());

            modelBuilder.Configurations.Add(new AlertaAlcanceConfiguration());

            modelBuilder.Configurations.Add(new AlertaVisualizacaoConfiguration());

            modelBuilder.Configurations.Add(new PesquisaConfiguration());

            modelBuilder.Configurations.Add(new QuestaoPesquisaConfiguration());

            modelBuilder.Configurations.Add(new RespostaPesquisaConfiguration());

            modelBuilder.Configurations.Add(new RespostaPesquisaValorConfiguration());

            modelBuilder.Configurations.Add(new AcompanhamentoAtendimentoDetalheConfiguration());

            modelBuilder.Configurations.Add(new AcompanhamentoAtendimentoMacroConfiguration());
        }


    }




}

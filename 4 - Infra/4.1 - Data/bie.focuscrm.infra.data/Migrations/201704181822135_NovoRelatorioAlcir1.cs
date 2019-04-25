namespace bie.focuscrm.infra.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class NovoRelatorioAlcir1 : DbMigration
    {
        public override void Up()
        {
          

            CreateStoredProcedure("dbo.SP_GetRelatorioAcompanhamentoMacro",               
                body:
                @"  select e.grupo, e.id_empresa , e.nome nome_empresa , s.id_setor , s.nome nome_setor, pa.intervalo intervalo_parametro, visitacao.intervalo_ultima_visita  from tb_empresa  e 
                    inner join tb_parametroagendamento pa on pa.id_empresa=e.id_empresa 
                    inner join tb_setor s on s.id_setor=pa.id_setor 
                    left outer join (  
                    select f.id_empresa, a.id_setor ,max(DataAgendada) as ultimavisita  , datediff( day,  max(dataAgendada), getdate()) intervalo_ultima_visita
                    from tb_atendimento  a
                    inner join tb_filial f on a.id_filial=f.id_filial
                    where DataAgendada < getdate() and Publicado is not null 
                    group by f.id_empresa, a.id_setor
                    ) visitacao on  visitacao.id_empresa = e.id_empresa  and visitacao.id_setor=s.id_setor "
           ); //GetRelatorioAcompanhamentoMacro



            CreateStoredProcedure("dbo.SP_GetRelatorioAcompanhamentoDetalhe",
             
                body: @"select    a.id_filial, a.id_setor, id_empresa, id_atendimento, s.nome nome_setor,  month(DataAgendada) mes , year(DataAgendada) ano , DataAgendada ,
                    RIGHT('0' + cast(Day(DataAgendada) as varchar),2) + '/'+  RIGHT('0' + cast(Month(DataAgendada) as varchar),2) DataVisita
                    from   tb_atendimento a      
                    inner join tb_setor s on s.id_setor=a.id_setor 
                    inner join tb_filial f on a.id_filial=f.id_filial
	                where a.Publicado is not null"
                ); //SP_GetRelatorioAcompanhamentoDetalhe


        }

        public override void Down()
        {
            CreateTable(
                "dbo.vw_acompanhamento_macro",
                c => new
                {
                    id_empresa = c.Int(nullable: false),
                    id_filial = c.Int(nullable: false),
                    id_setor = c.Int(nullable: false),
                    nome_empresa = c.String(maxLength: 300, unicode: false),
                    nome_filial = c.String(maxLength: 300, unicode: false),
                    nome_setor = c.String(maxLength: 300, unicode: false),
                    intervalo_parametro = c.Int(nullable: false),
                    intervalo_ultima_visita = c.Int(),
                })
                .PrimaryKey(t => new { t.id_empresa, t.id_filial, t.id_setor });

            CreateTable(
                "dbo.vw_acompanhamento_detalhe",
                c => new
                {
                    id_atendimento = c.Int(nullable: false, identity: true),
                    id_filial = c.Int(nullable: false),
                    id_setor = c.Int(nullable: false),
                    nome_setor = c.String(maxLength: 300, unicode: false),
                    mes = c.Int(nullable: false),
                    ano = c.Int(nullable: false),
                    DataAgendada = c.DateTime(nullable: false),
                    DataVisita = c.String(maxLength: 300, unicode: false),
                    id_empresa = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.id_atendimento);

        }
    }
}

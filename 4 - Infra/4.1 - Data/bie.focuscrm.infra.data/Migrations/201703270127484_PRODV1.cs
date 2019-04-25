namespace bie.focuscrm.infra.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PRODV1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_atendimento",
                c => new
                    {
                        id_atendimento = c.Int(nullable: false, identity: true),
                        id_filial = c.Int(nullable: false),
                        Assunto = c.String(nullable: false, maxLength: 800, unicode: false),
                        DataAgendada = c.DateTime(nullable: false),
                        id_setor = c.Int(nullable: false),
                        id_autor = c.String(nullable: false, maxLength: 300, unicode: false),
                        Presentes = c.String(maxLength: 300, unicode: false),
                        Local = c.String(maxLength: 100, unicode: false),
                        HoraInicio = c.String(maxLength: 5, unicode: false),
                        HoraFim = c.String(maxLength: 5, unicode: false),
                        Resumo = c.String(unicode: false, storeType: "text"),
                        Criado = c.DateTime(nullable: false),
                        Modificado = c.DateTime(nullable: false),
                        Publicado = c.DateTime(),
                    })
                .PrimaryKey(t => t.id_atendimento)
                .ForeignKey("dbo.tb_usuario", t => t.id_autor)
                .ForeignKey("dbo.tb_filial", t => t.id_filial)
                .ForeignKey("dbo.tb_setor", t => t.id_setor)
                .Index(t => t.id_filial)
                .Index(t => t.id_setor)
                .Index(t => t.id_autor);
            
            CreateTable(
                "dbo.tb_ataanexo",
                c => new
                    {
                        id_anexo = c.Int(nullable: false, identity: true),
                        id_atendimento = c.Int(nullable: false),
                        NomeArquivo = c.String(nullable: false, maxLength: 1000, unicode: false),
                        Mime = c.String(nullable: false, maxLength: 40, unicode: false),
                        Icone = c.String(maxLength: 300, unicode: false),
                        Conteudo = c.Binary(nullable: false),
                        Tamanho = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.id_anexo)
                .ForeignKey("dbo.tb_atendimento", t => t.id_atendimento, cascadeDelete: true)
                .Index(t => t.id_atendimento);
            
            CreateTable(
                "dbo.tb_usuario",
                c => new
                    {
                        id_usuario = c.String(nullable: false, maxLength: 300, unicode: false),
                        Nome = c.String(maxLength: 300, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        Telefone = c.String(maxLength: 300, unicode: false),
                        Telefone2 = c.String(maxLength: 300, unicode: false),
                        id_empresa = c.Int(),
                    })
                .PrimaryKey(t => t.id_usuario)
                .ForeignKey("dbo.tb_empresa", t => t.id_empresa)
                .Index(t => t.id_empresa);
            
            CreateTable(
                "dbo.tb_pendenciacomentario",
                c => new
                    {
                        id_pendenciacomentario = c.Int(nullable: false, identity: true),
                        id_pendencia = c.Int(nullable: false),
                        id_autor = c.String(nullable: false, maxLength: 300, unicode: false),
                        Conteudo = c.String(nullable: false, maxLength: 2000, unicode: false),
                        Data = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id_pendenciacomentario)
                .ForeignKey("dbo.tb_pendencia", t => t.id_pendencia, cascadeDelete: true)
                .ForeignKey("dbo.tb_usuario", t => t.id_autor, cascadeDelete: true)
                .Index(t => t.id_pendencia)
                .Index(t => t.id_autor);
            
            CreateTable(
                "dbo.tb_pendencia",
                c => new
                    {
                        id_pendencia = c.Int(nullable: false, identity: true),
                        id_atendimento = c.Int(nullable: false),
                        Titulo = c.String(nullable: false, maxLength: 800, unicode: false),
                        Prazo = c.DateTime(nullable: false),
                        id_responsavel = c.String(nullable: false, maxLength: 300, unicode: false),
                        Status = c.String(nullable: false, maxLength: 30, unicode: false),
                        Criado = c.DateTime(nullable: false),
                        Modificado = c.DateTime(nullable: false),
                        Historico = c.String(unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => t.id_pendencia)
                .ForeignKey("dbo.tb_atendimento", t => t.id_atendimento)
                .ForeignKey("dbo.tb_usuario", t => t.id_responsavel)
                .Index(t => t.id_atendimento)
                .Index(t => t.id_responsavel);
            
            CreateTable(
                "dbo.tb_empresa",
                c => new
                    {
                        id_empresa = c.Int(nullable: false, identity: true),
                        Grupo = c.String(nullable: false, maxLength: 255, unicode: false),
                        Nome = c.String(nullable: false, maxLength: 300, unicode: false),
                        CNPJ = c.String(nullable: false, maxLength: 40, unicode: false),
                        QtdFiliais = c.Int(),
                        QtdPrestadores = c.Int(),
                        MetrosQuadrados = c.Int(),
                        Socio = c.String(maxLength: 200, unicode: false),
                        TelefoneSocio = c.String(maxLength: 50, unicode: false),
                        EmailSocio = c.String(maxLength: 100, unicode: false),
                        Site = c.String(maxLength: 300, unicode: false),
                        NumeroCheckout = c.Int(),
                        Atividade = c.String(maxLength: 400, unicode: false),
                        InscEstadual = c.String(maxLength: 300, unicode: false),
                        QtdFuncionarios = c.Int(),
                        Bancos = c.String(maxLength: 60, unicode: false),
                        Endividamento = c.String(maxLength: 100, unicode: false),
                        AcoesFiscais = c.String(maxLength: 500, unicode: false),
                        RegimeTributacao = c.String(maxLength: 100, unicode: false),
                        SistemaRetaguarda = c.String(maxLength: 100, unicode: false),
                        DiaRetiradaDocumentos = c.String(maxLength: 30, unicode: false),
                        RazaoSocial = c.String(maxLength: 300, unicode: false),
                        OBS = c.String(maxLength: 300, unicode: false),
                        Logradouro = c.String(maxLength: 300, unicode: false),
                        Numero = c.String(maxLength: 300, unicode: false),
                        Complemento = c.String(maxLength: 300, unicode: false),
                        CEP = c.String(maxLength: 15, unicode: false),
                        Cidade = c.String(maxLength: 300, unicode: false),
                        UF = c.String(maxLength: 300, unicode: false),
                        Bairro = c.String(maxLength: 300, unicode: false),
                        Telefone = c.String(maxLength: 300, unicode: false),
                        RespostaWS = c.String(unicode: false, storeType: "text"),
                        QtdNFS = c.Int(),
                    })
                .PrimaryKey(t => t.id_empresa);
            
            CreateTable(
                "dbo.tb_alertaalcance",
                c => new
                    {
                        id_alerta = c.Int(nullable: false),
                        id_empresa = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.id_alerta, t.id_empresa })
                .ForeignKey("dbo.tb_alerta", t => t.id_alerta, cascadeDelete: true)
                .ForeignKey("dbo.tb_empresa", t => t.id_empresa, cascadeDelete: true)
                .Index(t => t.id_alerta)
                .Index(t => t.id_empresa);
            
            CreateTable(
                "dbo.tb_alerta",
                c => new
                    {
                        id_alerta = c.Int(nullable: false, identity: true),
                        tp_classificacao = c.Int(nullable: false),
                        nom_alerta = c.String(nullable: false, maxLength: 255, unicode: false),
                        dsc_conteudo = c.String(unicode: false, storeType: "text"),
                        din_inicio = c.DateTime(nullable: false),
                        din_fim = c.DateTime(nullable: false),
                        qtd_max_exibicoes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id_alerta);
            
            CreateTable(
                "dbo.tb_alertaVisualizacao",
                c => new
                    {
                        id_visualizacao = c.Int(nullable: false, identity: true),
                        id_alerta = c.Int(nullable: false),
                        id_usuario = c.String(nullable: false, maxLength: 128, unicode: false),
                        din_visualizacao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id_visualizacao)
                .ForeignKey("dbo.tb_alerta", t => t.id_alerta, cascadeDelete: true)
                .Index(t => t.id_alerta);
            
            CreateTable(
                "dbo.tb_filial",
                c => new
                    {
                        id_filial = c.Int(nullable: false, identity: true),
                        id_empresa = c.Int(nullable: false),
                        CNPJ = c.String(nullable: false, maxLength: 40, unicode: false),
                        Nome = c.String(nullable: false, maxLength: 255, unicode: false),
                        RazaoSocial = c.String(maxLength: 300, unicode: false),
                        OBS = c.String(unicode: false, storeType: "text"),
                        Logradouro = c.String(maxLength: 300, unicode: false),
                        Numero = c.String(maxLength: 300, unicode: false),
                        Complemento = c.String(maxLength: 300, unicode: false),
                        CEP = c.String(maxLength: 300, unicode: false),
                        Cidade = c.String(maxLength: 300, unicode: false),
                        UF = c.String(maxLength: 300, unicode: false),
                        Telefone = c.String(maxLength: 50, unicode: false),
                        Bairro = c.String(maxLength: 300, unicode: false),
                        RespostaWS = c.String(unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => t.id_filial)
                .ForeignKey("dbo.tb_empresa", t => t.id_empresa, cascadeDelete: true)
                .Index(t => t.id_empresa);
            
            CreateTable(
                "dbo.tb_parametroagendamento",
                c => new
                    {
                        id_empresa = c.Int(nullable: false),
                        id_setor = c.Int(nullable: false),
                        Intervalo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.id_empresa, t.id_setor })
                .ForeignKey("dbo.tb_empresa", t => t.id_empresa, cascadeDelete: true)
                .ForeignKey("dbo.tb_setor", t => t.id_setor, cascadeDelete: true)
                .Index(t => t.id_empresa)
                .Index(t => t.id_setor);
            
            CreateTable(
                "dbo.tb_setor",
                c => new
                    {
                        id_setor = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 300, unicode: false),
                        id_usuarioresponsavel = c.String(maxLength: 300, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id_setor)
                .ForeignKey("dbo.tb_usuario", t => t.id_usuarioresponsavel)
                .Index(t => t.id_usuarioresponsavel);
            
            CreateTable(
                "dbo.tb_respostapesquisa",
                c => new
                    {
                        id_respostapesquisa = c.Int(nullable: false, identity: true),
                        id_pesquisa = c.Int(nullable: false),
                        id_usuario = c.String(nullable: false, maxLength: 300, unicode: false),
                        id_atendimento = c.Int(nullable: false),
                        DataEnvio = c.DateTime(nullable: false),
                        DataResposta = c.DateTime(),
                    })
                .PrimaryKey(t => t.id_respostapesquisa)
                .ForeignKey("dbo.tb_atendimento", t => t.id_atendimento)
                .ForeignKey("dbo.tb_pesquisa", t => t.id_pesquisa)
                .ForeignKey("dbo.tb_usuario", t => t.id_usuario)
                .Index(t => t.id_pesquisa)
                .Index(t => t.id_usuario)
                .Index(t => t.id_atendimento);
            
            CreateTable(
                "dbo.tb_pesquisa",
                c => new
                    {
                        id_pesquisa = c.Int(nullable: false, identity: true),
                        tp_pesquisa = c.Int(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        Criado = c.DateTime(nullable: false),
                        Modificado = c.DateTime(nullable: false),
                        Titulo = c.String(nullable: false, maxLength: 100, unicode: false),
                        Descricao = c.String(unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => t.id_pesquisa);
            
            CreateTable(
                "dbo.tb_questaopesquisa",
                c => new
                    {
                        id_questaopesquisa = c.Int(nullable: false, identity: true),
                        Ordem = c.Int(nullable: false),
                        Descricao = c.String(nullable: false, maxLength: 300, unicode: false),
                        id_pesquisa = c.Int(nullable: false),
                        Valores = c.String(maxLength: 300, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id_questaopesquisa)
                .ForeignKey("dbo.tb_pesquisa", t => t.id_pesquisa, cascadeDelete: true)
                .Index(t => t.id_pesquisa);
            
            CreateTable(
                "dbo.tb_respostapesquisavalor",
                c => new
                    {
                        id_questao = c.Int(nullable: false),
                        id_respostapesquisa = c.Int(nullable: false),
                        ValorResposta = c.String(nullable: false, maxLength: 300, unicode: false),
                    })
                .PrimaryKey(t => new { t.id_questao, t.id_respostapesquisa })
                .ForeignKey("dbo.tb_questaopesquisa", t => t.id_questao)
                .ForeignKey("dbo.tb_respostapesquisa", t => t.id_respostapesquisa)
                .Index(t => t.id_questao)
                .Index(t => t.id_respostapesquisa);
            
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
                "dbo.tb_setorfuncionario",
                c => new
                    {
                        id_setor = c.Int(nullable: false),
                        id_usuario = c.String(nullable: false, maxLength: 300, unicode: false),
                    })
                .PrimaryKey(t => new { t.id_setor, t.id_usuario })
                .ForeignKey("dbo.tb_setor", t => t.id_setor, cascadeDelete: true)
                .ForeignKey("dbo.tb_usuario", t => t.id_usuario, cascadeDelete: true)
                .Index(t => t.id_setor)
                .Index(t => t.id_usuario);
            
            CreateTable(
                "dbo.tb_atendimentousuariocliente",
                c => new
                    {
                        id_atendimento = c.Int(nullable: false),
                        id_usuario = c.String(nullable: false, maxLength: 300, unicode: false),
                    })
                .PrimaryKey(t => new { t.id_atendimento, t.id_usuario })
                .ForeignKey("dbo.tb_atendimento", t => t.id_atendimento, cascadeDelete: true)
                .ForeignKey("dbo.tb_usuario", t => t.id_usuario, cascadeDelete: true)
                .Index(t => t.id_atendimento)
                .Index(t => t.id_usuario);
            
            CreateTable(
                "dbo.tb_atendimentousuariofocus",
                c => new
                    {
                        id_atendimento = c.Int(nullable: false),
                        id_usuario = c.String(nullable: false, maxLength: 300, unicode: false),
                    })
                .PrimaryKey(t => new { t.id_atendimento, t.id_usuario })
                .ForeignKey("dbo.tb_atendimento", t => t.id_atendimento, cascadeDelete: true)
                .ForeignKey("dbo.tb_usuario", t => t.id_usuario, cascadeDelete: true)
                .Index(t => t.id_atendimento)
                .Index(t => t.id_usuario);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_atendimentousuariofocus", "id_usuario", "dbo.tb_usuario");
            DropForeignKey("dbo.tb_atendimentousuariofocus", "id_atendimento", "dbo.tb_atendimento");
            DropForeignKey("dbo.tb_atendimentousuariocliente", "id_usuario", "dbo.tb_usuario");
            DropForeignKey("dbo.tb_atendimentousuariocliente", "id_atendimento", "dbo.tb_atendimento");
            DropForeignKey("dbo.tb_atendimento", "id_setor", "dbo.tb_setor");
            DropForeignKey("dbo.tb_atendimento", "id_filial", "dbo.tb_filial");
            DropForeignKey("dbo.tb_atendimento", "id_autor", "dbo.tb_usuario");
            DropForeignKey("dbo.tb_respostapesquisa", "id_usuario", "dbo.tb_usuario");
            DropForeignKey("dbo.tb_respostapesquisa", "id_pesquisa", "dbo.tb_pesquisa");
            DropForeignKey("dbo.tb_respostapesquisavalor", "id_respostapesquisa", "dbo.tb_respostapesquisa");
            DropForeignKey("dbo.tb_respostapesquisavalor", "id_questao", "dbo.tb_questaopesquisa");
            DropForeignKey("dbo.tb_questaopesquisa", "id_pesquisa", "dbo.tb_pesquisa");
            DropForeignKey("dbo.tb_respostapesquisa", "id_atendimento", "dbo.tb_atendimento");
            DropForeignKey("dbo.tb_usuario", "id_empresa", "dbo.tb_empresa");
            DropForeignKey("dbo.tb_parametroagendamento", "id_setor", "dbo.tb_setor");
            DropForeignKey("dbo.tb_setor", "id_usuarioresponsavel", "dbo.tb_usuario");
            DropForeignKey("dbo.tb_setorfuncionario", "id_usuario", "dbo.tb_usuario");
            DropForeignKey("dbo.tb_setorfuncionario", "id_setor", "dbo.tb_setor");
            DropForeignKey("dbo.tb_parametroagendamento", "id_empresa", "dbo.tb_empresa");
            DropForeignKey("dbo.tb_filial", "id_empresa", "dbo.tb_empresa");
            DropForeignKey("dbo.tb_alertaalcance", "id_empresa", "dbo.tb_empresa");
            DropForeignKey("dbo.tb_alertaalcance", "id_alerta", "dbo.tb_alerta");
            DropForeignKey("dbo.tb_alertaVisualizacao", "id_alerta", "dbo.tb_alerta");
            DropForeignKey("dbo.tb_pendenciacomentario", "id_autor", "dbo.tb_usuario");
            DropForeignKey("dbo.tb_pendenciacomentario", "id_pendencia", "dbo.tb_pendencia");
            DropForeignKey("dbo.tb_pendencia", "id_responsavel", "dbo.tb_usuario");
            DropForeignKey("dbo.tb_pendencia", "id_atendimento", "dbo.tb_atendimento");
            DropForeignKey("dbo.tb_ataanexo", "id_atendimento", "dbo.tb_atendimento");
            DropIndex("dbo.tb_atendimentousuariofocus", new[] { "id_usuario" });
            DropIndex("dbo.tb_atendimentousuariofocus", new[] { "id_atendimento" });
            DropIndex("dbo.tb_atendimentousuariocliente", new[] { "id_usuario" });
            DropIndex("dbo.tb_atendimentousuariocliente", new[] { "id_atendimento" });
            DropIndex("dbo.tb_setorfuncionario", new[] { "id_usuario" });
            DropIndex("dbo.tb_setorfuncionario", new[] { "id_setor" });
            DropIndex("dbo.tb_respostapesquisavalor", new[] { "id_respostapesquisa" });
            DropIndex("dbo.tb_respostapesquisavalor", new[] { "id_questao" });
            DropIndex("dbo.tb_questaopesquisa", new[] { "id_pesquisa" });
            DropIndex("dbo.tb_respostapesquisa", new[] { "id_atendimento" });
            DropIndex("dbo.tb_respostapesquisa", new[] { "id_usuario" });
            DropIndex("dbo.tb_respostapesquisa", new[] { "id_pesquisa" });
            DropIndex("dbo.tb_setor", new[] { "id_usuarioresponsavel" });
            DropIndex("dbo.tb_parametroagendamento", new[] { "id_setor" });
            DropIndex("dbo.tb_parametroagendamento", new[] { "id_empresa" });
            DropIndex("dbo.tb_filial", new[] { "id_empresa" });
            DropIndex("dbo.tb_alertaVisualizacao", new[] { "id_alerta" });
            DropIndex("dbo.tb_alertaalcance", new[] { "id_empresa" });
            DropIndex("dbo.tb_alertaalcance", new[] { "id_alerta" });
            DropIndex("dbo.tb_pendencia", new[] { "id_responsavel" });
            DropIndex("dbo.tb_pendencia", new[] { "id_atendimento" });
            DropIndex("dbo.tb_pendenciacomentario", new[] { "id_autor" });
            DropIndex("dbo.tb_pendenciacomentario", new[] { "id_pendencia" });
            DropIndex("dbo.tb_usuario", new[] { "id_empresa" });
            DropIndex("dbo.tb_ataanexo", new[] { "id_atendimento" });
            DropIndex("dbo.tb_atendimento", new[] { "id_autor" });
            DropIndex("dbo.tb_atendimento", new[] { "id_setor" });
            DropIndex("dbo.tb_atendimento", new[] { "id_filial" });
            DropTable("dbo.tb_atendimentousuariofocus");
            DropTable("dbo.tb_atendimentousuariocliente");
            DropTable("dbo.tb_setorfuncionario");
            DropTable("dbo.vw_acompanhamento_macro");
            DropTable("dbo.vw_acompanhamento_detalhe");
            DropTable("dbo.tb_respostapesquisavalor");
            DropTable("dbo.tb_questaopesquisa");
            DropTable("dbo.tb_pesquisa");
            DropTable("dbo.tb_respostapesquisa");
            DropTable("dbo.tb_setor");
            DropTable("dbo.tb_parametroagendamento");
            DropTable("dbo.tb_filial");
            DropTable("dbo.tb_alertaVisualizacao");
            DropTable("dbo.tb_alerta");
            DropTable("dbo.tb_alertaalcance");
            DropTable("dbo.tb_empresa");
            DropTable("dbo.tb_pendencia");
            DropTable("dbo.tb_pendenciacomentario");
            DropTable("dbo.tb_usuario");
            DropTable("dbo.tb_ataanexo");
            DropTable("dbo.tb_atendimento");
        }
    }
}

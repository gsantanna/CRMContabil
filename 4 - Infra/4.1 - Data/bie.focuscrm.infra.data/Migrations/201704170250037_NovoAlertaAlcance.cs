namespace bie.focuscrm.infra.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NovoAlertaAlcance : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tb_alertaalcance", "id_empresa", "dbo.tb_empresa");
            DropIndex("dbo.tb_alertaalcance", new[] { "id_empresa" });
            DropPrimaryKey("dbo.tb_alertaalcance");
            AddColumn("dbo.tb_alertaalcance", "id_usuario", c => c.String(nullable: false, maxLength: 300, unicode: false));
            AddPrimaryKey("dbo.tb_alertaalcance", new[] { "id_alerta", "id_usuario" });
            CreateIndex("dbo.tb_alertaalcance", "id_usuario");
            AddForeignKey("dbo.tb_alertaalcance", "id_usuario", "dbo.tb_usuario", "id_usuario", cascadeDelete: true);
            DropColumn("dbo.tb_alertaalcance", "id_empresa");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_alertaalcance", "id_empresa", c => c.Int(nullable: false));
            DropForeignKey("dbo.tb_alertaalcance", "id_usuario", "dbo.tb_usuario");
            DropIndex("dbo.tb_alertaalcance", new[] { "id_usuario" });
            DropPrimaryKey("dbo.tb_alertaalcance");
            DropColumn("dbo.tb_alertaalcance", "id_usuario");
            AddPrimaryKey("dbo.tb_alertaalcance", new[] { "id_alerta", "id_empresa" });
            CreateIndex("dbo.tb_alertaalcance", "id_empresa");
            AddForeignKey("dbo.tb_alertaalcance", "id_empresa", "dbo.tb_empresa", "id_empresa", cascadeDelete: true);
        }
    }
}

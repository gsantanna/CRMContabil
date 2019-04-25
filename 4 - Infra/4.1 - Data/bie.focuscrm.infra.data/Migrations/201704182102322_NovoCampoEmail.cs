namespace bie.focuscrm.infra.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NovoCampoEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_usuario", "Email", c => c.String(maxLength: 200, unicode: false));

            Sql("update dest set dest.Email=orign.Email from tb_usuario dest inner join AspNetUsers orign on orign.ID = dest.id_usuario");
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_usuario", "Email");
        }
    }
}

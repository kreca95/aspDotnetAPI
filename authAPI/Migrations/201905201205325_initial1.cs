namespace authAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Resources", "TagsCompressed", c => c.String());
            DropColumn("dbo.Resources", "Tags");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Resources", "Tags", c => c.String());
            DropColumn("dbo.Resources", "TagsCompressed");
        }
    }
}

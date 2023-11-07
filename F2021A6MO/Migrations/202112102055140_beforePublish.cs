namespace F2021A6MO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class beforePublish : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Albums", "Background", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Artists", "Career", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Artists", "Career", c => c.String(maxLength: 800));
            AlterColumn("dbo.Albums", "Background", c => c.String(maxLength: 800));
        }
    }
}

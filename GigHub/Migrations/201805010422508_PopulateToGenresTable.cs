namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateToGenresTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres(Id,Name) VALUES(1,'course')");
            Sql("INSERT INTO Genres(Id,Name) VALUES(2,'talent')");
            Sql("INSERT INTO Genres(Id,Name) VALUES(3,'disscuss')");
            Sql("INSERT INTO Genres(Id,Name) VALUES(4,'funDay')");

        }

        public override void Down()
        {
            Sql("DELETE FROM Genres WHERE Id IN (1,2,3,4)");
        }
    }
}

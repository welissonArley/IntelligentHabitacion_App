using FluentMigrator;

namespace Homuai.Infrastructure.Migrations.Versions
{
    [Migration((long)EnumVersions.RegisterRooms, "Add rooms on home")]
    public class Version0000006 : Migration
    {
        public override void Down() { }
        public override void Up()
        {
            BaseVersion.CreateDefaultColumns(Create.Table("Room"))
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("HomeId").AsInt64().NotNullable().ForeignKey("FK_Room_Home_Id", "Home", "Id");
        }
    }
}

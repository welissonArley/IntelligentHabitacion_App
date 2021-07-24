using FluentMigrator;

namespace Homuai.Infrastructure.Migrations.Versions
{
    [Migration((long)EnumVersions.RegisterUsersFood, "Create table to save the user's food")]
    public class Version0000003 : Migration
    {
        public override void Down() { }

        public override void Up()
        {
            BaseVersion.CreateDefaultColumns(Create.Table("MyFood"))
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Quantity").AsDecimal().NotNullable()
                .WithColumn("Manufacturer").AsString().Nullable()
                .WithColumn("Type").AsInt32().NotNullable()
                .WithColumn("DueDate").AsDateTime().Nullable()
                .WithColumn("UserId").AsInt64().NotNullable().ForeignKey("FK_MyFood_User_Id", "User", "Id");
        }
    }
}

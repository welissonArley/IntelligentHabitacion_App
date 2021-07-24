using FluentMigrator;

namespace Homuai.Infrastructure.Migrations.Versions
{
    [Migration((long)EnumVersions.RegisterUser, "Create table to save the user's informations")]
    public class Version0000001 : Migration
    {
        public override void Down() { }

        public override void Up()
        {
            BaseVersion.CreateDefaultColumns(Create.Table("User"))
                .WithColumn("Name").AsString(2000).NotNullable()
                .WithColumn("Email").AsString(2000).NotNullable()
                .WithColumn("Password").AsString(2000).NotNullable();

            BaseVersion.CreateDefaultColumns(Create.Table("EmergencyContact"))
                .WithColumn("Name").AsString(2000).NotNullable()
                .WithColumn("Relationship").AsString(100).NotNullable()
                .WithColumn("Phonenumber").AsString().NotNullable()
                .WithColumn("UserId").AsInt64().NotNullable().ForeignKey("FK_EmergencyContact_User_Id", "User", "Id");

            BaseVersion.CreateDefaultColumns(Create.Table("Phonenumber"))
                .WithColumn("Number").AsString().NotNullable()
                .WithColumn("UserId").AsInt64().NotNullable().ForeignKey("FK_Phonenumber_User_Id", "User", "Id");

            Create.Table("Token")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsString(2000).NotNullable()
                .WithColumn("UserId").AsInt64().NotNullable().ForeignKey("FK_Token_User_Id", "User", "Id");

            BaseVersion.CreateDefaultColumns(Create.Table("Code"))
                .WithColumn("Value").AsString().NotNullable()
                .WithColumn("Type").AsInt64().NotNullable()
                .WithColumn("UserId").AsInt64().NotNullable().ForeignKey("FK_Code_User_Id", "User", "Id");
        }
    }
}

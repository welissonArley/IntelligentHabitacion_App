using FluentMigrator;

namespace Homuai.Infrastructure.Migrations.Versions
{
    [Migration((long)EnumVersions.RegisterHome, "Create table to save the homes's informations")]
    public class Version0000002 : Migration
    {
        public override void Down() { }

        public override void Up()
        {
            BaseVersion.CreateDefaultColumns(Create.Table("Home"))
                .WithColumn("ZipCode").AsString().NotNullable()
                .WithColumn("Address").AsString().NotNullable()
                .WithColumn("Number").AsString().NotNullable()
                .WithColumn("AdditionalAddressInfo").AsString().Nullable()
                .WithColumn("Neighborhood").AsString().Nullable()
                .WithColumn("City").AsString().NotNullable()
                .WithColumn("StateProvince").AsString().Nullable()
                .WithColumn("Country").AsInt32().NotNullable()
                .WithColumn("DeadlinePaymentRent").AsInt16().NotNullable()
                .WithColumn("NetworksName").AsString().Nullable()
                .WithColumn("NetworksPassword").AsString().Nullable()
                .WithColumn("AdministratorId").AsInt64().NotNullable().ForeignKey("FK_Home_User_Id", "User", "Id");

            BaseVersion.CreateDefaultColumns(Create.Table("HomeAssociation"))
                .WithColumn("HomeId").AsInt64().NotNullable().ForeignKey("FK_HomeAssociation_Home_Id", "Home", "Id")
                .WithColumn("JoinedOn").AsDateTime().NotNullable();

            Alter.Table("User")
                .AddColumn("ProfileColor").AsString(7).NotNullable()
                .AddColumn("HomeAssociationId").AsInt64().Nullable().ForeignKey("FK_User_HomeAssociation_Id", "HomeAssociation", "Id");
        }
    }
}

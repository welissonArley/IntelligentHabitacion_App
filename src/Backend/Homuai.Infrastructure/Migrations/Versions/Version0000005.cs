using FluentMigrator;

namespace Homuai.Infrastructure.Migrations.Versions
{
    [Migration((long)EnumVersions.PushNotification, "Add push Notification Id")]
    public class Version0000005 : Migration
    {
        public override void Down() { }
        public override void Up()
        {
            Alter.Table("User")
                .AddColumn("PushNotificationId").AsString(2000).NotNullable();

            Alter.Table("HomeAssociation")
                .AddColumn("ExitOn").AsDateTime().Nullable()
                .AddColumn("UserIdentity").AsInt64().NotNullable();
        }
    }
}

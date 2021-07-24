using FluentMigrator;

namespace Homuai.Infrastructure.Migrations.Versions
{
    [Migration((long)EnumVersions.ProfileColor, "Profile Color to Dark and Light mode")]
    public class Version0000008 : Migration
    {
        public override void Down() { }
        public override void Up()
        {
            Delete.Column("ProfileColor").FromTable("User");
            Alter.Table("User").AddColumn("ProfileColorLightMode").AsString(7).NotNullable()
                .AddColumn("ProfileColorDarkMode").AsString(7).NotNullable();
        }
    }
}

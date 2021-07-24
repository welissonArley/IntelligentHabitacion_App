using FluentMigrator;

namespace Homuai.Infrastructure.Migrations.Versions
{
    [Migration((long)EnumVersions.CleaningSchedule, "Tables to Cleaning schedule")]
    public class Version0000007 : Migration
    {
        public override void Down() { }
        public override void Up()
        {
            BaseVersion.CreateDefaultColumns(Create.Table("CleaningSchedule"))
                .WithColumn("ScheduleStartAt").AsDateTime().NotNullable()
                .WithColumn("ScheduleFinishAt").AsDateTime().Nullable()
                .WithColumn("UserId").AsInt64().NotNullable().ForeignKey("FK_CleaningSchedule_User_Id", "User", "Id")
                .WithColumn("HomeId").AsInt64().NotNullable().ForeignKey("FK_CleaningSchedule_Home_Id", "Home", "Id")
                .WithColumn("Room").AsString().NotNullable();

            BaseVersion.CreateDefaultColumns(Create.Table("CleaningTasksCompleted"))
                .WithColumn("CleaningScheduleId").AsInt64().NotNullable().ForeignKey("FK_CleaningTasksCompleted_CleaningSchedule_Id", "CleaningSchedule", "Id");

            Create.Table("CleaningRating")
                .WithColumn("Id").AsGuid().NotNullable()
                .WithColumn("Rating").AsInt32().NotNullable()
                .WithColumn("Feedback").AsString().Nullable()
                .WithColumn("CleaningTaskCompletedId").AsInt64().NotNullable().ForeignKey("FK_CleaningRating_CleaningTasksCompleted_Id", "CleaningTasksCompleted", "Id");

            Create.Table("CleaningRatingUser")
                .WithColumn("Id").AsGuid().NotNullable()
                .WithColumn("UserId").AsInt64().NotNullable().ForeignKey("FK_CleaningRatingUser_User_Id", "User", "Id")
                .WithColumn("CleaningTaskCompletedId").AsInt64().NotNullable().ForeignKey("FK_CleaningRatingUser_CleaningTasksCompleted_Id", "CleaningTasksCompleted", "Id");
        }
    }
}

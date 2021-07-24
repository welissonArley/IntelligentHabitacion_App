using FluentMigrator.Builders.Create.Table;

namespace Homuai.Infrastructure.Migrations
{
    public static class BaseVersion
    {
        public static ICreateTableColumnOptionOrWithColumnSyntax CreateDefaultColumns(ICreateTableWithColumnOrSchemaOrDescriptionSyntax Table)
        {
            return Table
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("CreateDate").AsDateTime().NotNullable()
                .WithColumn("Active").AsBoolean().NotNullable().WithDefaultValue(1);
        }
    }
}

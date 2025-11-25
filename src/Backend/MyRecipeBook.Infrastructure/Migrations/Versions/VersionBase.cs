using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace MyRecipeBook.Infrastructure.Migrations.Versions
{
    public abstract class VersionBase : ForwardOnlyMigration
    {
        protected ICreateTableColumnOptionOrWithColumnSyntax CreateTable(string table)
        {
            return Create.Table(table)
                .WithColumn("Id").AsInt64().PrimaryKey().Identity().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                .WithColumn("Active").AsBoolean().NotNullable().WithDefaultValue(true);
        }
    }
}

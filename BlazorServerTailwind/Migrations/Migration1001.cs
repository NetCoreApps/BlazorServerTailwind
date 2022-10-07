using BlazorServerTailwind.ServiceModel;
using ServiceStack.OrmLite;

namespace BlazorServerTailwind.Migrations;

/// <summary>
/// Failed migration test
/// </summary>
public class Migration1001 : MigrationBase
{
    public override void Up()
    {
        Db.Insert(new Hello
        {
            Name = "Test failure"
        });
    }
}
namespace CuteDev.Users.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CuteDev.Users.Data.DAL.Model.CuteModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        Data.BLL.bllPermissions perms = new Data.BLL.bllPermissions();
        Data.BLL.bllUsers user = new Data.BLL.bllUsers();
        Data.BLL.bllRoles roles = new Data.BLL.bllRoles();
        Data.BLL.bllRolesPermissions roleperms = new Data.BLL.bllRolesPermissions();

        protected override void Seed(CuteDev.Users.Data.DAL.Model.CuteModel context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            user.AddDefaultUsers(context);
            perms.AddDefaultPermissions(context);
            roles.AddDefaultRoles(context);
            roleperms.AddDefaultRolesPermissions(context);

        }
    }
}

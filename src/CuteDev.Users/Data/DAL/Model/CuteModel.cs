namespace CuteDev.Users.Data.DAL.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Migrations;

    public partial class CuteModel : CuteDev.Database.DAL.CuteModel
    {
        public CuteModel() : base() { }
        public CuteModel(string name) : base(name) { }

        public override void InitializeDatabase()
        {

        }

        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UsersMeta> UsersMeta { get; set; }
        public virtual DbSet<Permissions> Permissions { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<RolesPermissions> RolesPermissions { get; set; }
        public virtual DbSet<UsersRoles> UsersRoles { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CuteModel, Configuration>());
        }

    }
}

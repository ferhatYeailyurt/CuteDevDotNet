namespace CuteDev.Log.Data.DAL.Model
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

        public virtual DbSet<Logs> Logs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CuteModel, Configuration>());

            modelBuilder.Entity<Logs>()
                .Property(e => e.id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Logs>()
                .Property(e => e.creatorId)
                .HasPrecision(18, 0);
        }
    }
}

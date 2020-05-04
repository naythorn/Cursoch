namespace Курсоч
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class sixteenPersDB : DbContext
    {
        public sixteenPersDB()
            : base("name=sixteenPersDB")
        {
        }

        public virtual DbSet<my_test> my_test { get; set; }
        public virtual DbSet<my_test_decriptor> my_test_decriptor { get; set; }
        public virtual DbSet<question> questions { get; set; }
        public virtual DbSet<result> results { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<user> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<user>()
                .HasMany(e => e.my_test)
                .WithOptional(e => e.user)
                .HasForeignKey(e => e.host_login);

            modelBuilder.Entity<user>()
                .HasMany(e => e.results)
                .WithOptional(e => e.user)
                .HasForeignKey(e => e.user_login);
        }
    }
}

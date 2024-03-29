﻿using DB_Module.Models;
namespace DB_Module
{
    public class SQL : DbContext
    {
        public static string connectionString { get; set; } =
            "Server=balics-home.hu;Database=acs;User Id=sa;Password=Titok1234;TrustServerCertificate=true;";

        public SQL() : base(new DbContextOptionsBuilder().UseSqlServer(connectionString).Options)
        {
        }

        public SQL(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentRestriction>().HasKey(x => new { x.StudentId, x.RestrictionId });
            modelBuilder.Entity<StudentExtended>(ex =>
            {
                ex.HasNoKey();
                ex.ToView("StudentExtended");
            }
            );

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Guardian> Parents { get; set; }
        public DbSet<Privilege> Privileges { get; set; }
        public DbSet<Restriction> Restrictions { get; set; }
        public DbSet<Personnel> Personnel { get; set; }
        public DbSet<Role> PersonRoles { get; set; }
        public DbSet<GateLog> GateLogs { get; set; }
        public DbSet<StudentRestriction> StudentRestrictions { get; set; }
        public DbSet<StudentPrivilege> StudentPrivileges { get; set; }
        public DbSet<StudentExtended> ExtendedStudents { get; set; }
    }
}
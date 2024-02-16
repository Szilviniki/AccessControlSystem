using DB_Module.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Module
{
    public class SQL : DbContext
    {
        public static string connectionString { get; set; }

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
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Privilege> Privileges { get; set; }
        public DbSet<Restriction> Restrictions { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Role> PersonRoles { get; set; }
        public DbSet<GateLog> GateLogs { get; set; }
        public DbSet<StudentRestriction> StudentRestrictions { get; set; }
        public DbSet<StudentPrivilege> StudentPrivileges { get; set; }
        public DbSet<StudentParent> studentParents { get; set; }
        public DbSet<StudentExtended> ExtendedStudents { get; set; }
    }
}
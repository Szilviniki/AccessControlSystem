using DB_Module.Models;

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
            modelBuilder.Entity<StudentExtended>(ex =>
                {
                    ex.HasNoKey();
                    ex.ToView("StudentExtended");
                }
            );
            modelBuilder.Entity<ActiveRule>(ar =>
            {
                ar.HasNoKey();
                ar.ToView("ActiveRules");
            });
            modelBuilder.Entity<ActiveParoleRule>(ar =>
            {
                ar.HasNoKey();
                ar.ToView("ActiveParoleRules");
            });
            
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Guardian> Parents { get; set; }
        public DbSet<ParoleRule> ParoleRules { get; set; }
        public DbSet<LockRule> LockRules { get; set; }
        public DbSet<Personnel> Personnel { get; set; }
        public DbSet<Role> PersonRoles { get; set; }
        public DbSet<GateLog> GateLogs { get; set; }

        public DbSet<StudentLockRule> StudentsLockRules { get; set; }
        
        public DbSet<StudentParoleRule> StudentsParoleRules { get; set; }
        
        public DbSet<StudentExtended> ExtendedStudents { get; set; }
        
        public DbSet<ActiveRule> ActiveRules { get; set; }
        
        public DbSet<ActiveParoleRule> ActiveParoleRules { get; set; }
    }
}
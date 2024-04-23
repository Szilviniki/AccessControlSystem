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

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Guardian> Guardians { get; set; }

        public DbSet<Personnel> Personnel { get; set; }
        
        public DbSet<GateLog> GateLogs { get; set; }
        
        public DbSet<Note> Notes { get; set; }
        
    }
}
using Microsoft.EntityFrameworkCore;
using Foramag.Models;
using ForamagApp.Models;

namespace Foramag.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<V_Clt_Detail> V_Clt_Detail { get; set; }
        public DbSet<V_CA_COMM> V_CA_COMM { get; set; }

        public DbSet<V_Doc_lignes> V_Doc_Lignes { get; set; }

        public DbSet<Login> t_Login { get; set; }
        public DbSet<Document> Documents { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<V_Marque> V_Marque { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasNoKey();
        }
    }
}
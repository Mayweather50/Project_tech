using Microsoft.EntityFrameworkCore;

namespace Web_Tech.Models
{
    public class Project_DbContext:DbContext
    {
        public Project_DbContext()
        {

        }
        public DbSet<Markup> Markups { get; set; }
        public Project_DbContext(DbContextOptions<UserContext> options)
            : base(options)
        {
           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-QBPQ3KL;Database=Project_Db;Trusted_Connection=True;");
            }
            optionsBuilder.LogTo(Console.WriteLine);


        }
    }
}

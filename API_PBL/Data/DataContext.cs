using Microsoft.EntityFrameworkCore;
using API_PBL.Models.DatabaseModels;

namespace API_PBL.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<GameTag>()
            //    .HasKey(m => new { m.IdGame, m.IdTag });
            //modelBuilder.Entity<GameTag>()
            //    .HasOne(m => m.Game)
            //    .WithMany(c => c.GameTag)
            //    .HasForeignKey(m => m.IdGame);
            //modelBuilder.Entity<GameTag>()
            //    .HasOne(m => m.Tag)
            //    .WithMany(c => c.GameTag)
            //    .HasForeignKey(m => m.IdTag);
        }
        public DbSet<Game> Games { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
    }
}

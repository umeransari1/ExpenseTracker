using ExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users => Set<User>();
        public DbSet<Expense> Expenses => Set<Expense>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(b =>
            {
                b.HasKey(u => u.Id);
                b.HasIndex(u => u.Username).IsUnique();
                b.HasIndex(u => u.Email).IsUnique();
            });

            modelBuilder.Entity<Expense>(b =>
            {
                b.HasKey(e => e.Id);
                
                b.Property(e => e.RowId)
                .IsRequired();
                b.HasIndex(e => e.RowId).IsUnique();

                b.HasOne(e => e.User)
                 .WithMany(u => u.Expenses)
                 .HasForeignKey(e => e.UserId)
                 .OnDelete(DeleteBehavior.Cascade);

                b.Property(e => e.Title)
                 .IsRequired()
                 .HasMaxLength(200);

                b.Property(e => e.Category)
                 .HasMaxLength(100);

                b.Property(e => e.Amount)
                 .HasColumnType("decimal(18,2)");
            });
        }
    }
}

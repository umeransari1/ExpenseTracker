using ExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users => Set<User>();
        public DbSet<Expense> Expenses => Set<Expense>();
        public DbSet<Category> Categories => Set<Category>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(b =>
            {
                b.HasKey(u => u.Id);
                b.HasIndex(u => u.Username).IsUnique();
                b.HasIndex(u => u.Email).IsUnique();

                b.Property(u => u.Username).IsRequired().HasMaxLength(100);
                b.Property(u => u.Email).IsRequired().HasMaxLength(200);

                b.HasMany(u => u.Expenses)
                 .WithOne(e => e.User)
                 .HasForeignKey(e => e.UserId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            // CATEGORY CONFIG
            modelBuilder.Entity<Category>(b =>
            {
                b.HasKey(c => c.Id);
                b.Property(c => c.RowId).IsRequired();
                b.HasIndex(c => c.RowId).IsUnique();
                
                b.Property(c => c.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Expense>(b =>
            {
                b.HasKey(e => e.Id);
                b.Property(e => e.RowId).IsRequired();
                b.HasIndex(e => e.RowId).IsUnique();

                b.Property(e => e.Title).IsRequired().HasMaxLength(200);
                b.Property(e => e.Amount).HasColumnType("decimal(18,2)");

                // FK to User
                b.HasOne(e => e.User)
                 .WithMany(u => u.Expenses)
                 .HasForeignKey(e => e.UserId)
                 .OnDelete(DeleteBehavior.Cascade);

                // FK to Category
                b.HasOne(e => e.Category)
                 .WithMany(c => c.Expenses)
                 .HasForeignKey(e => e.CategoryId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            AddData(modelBuilder);
        }

        private void AddData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Food & Dining" }, 
                new Category { Id = 2, Name = "Transportation" }, 
                new Category { Id = 3, Name = "Groceries" }, 
                new Category { Id = 4, Name = "Utilities" }, 
                new Category { Id = 5, Name = "Entertainment" },
                new Category { Id = 6, Name = "Health & Fitness" }, 
                new Category { Id = 7, Name = "Education" }, 
                new Category { Id = 8, Name = "Shopping" }, 
                new Category { Id = 9, Name = "Rent" }, 
                new Category { Id = 10, Name = "Travel" }, 
                new Category { Id = 11, Name = "Bills" }, 
                new Category { Id = 12, Name = "Insurance" },
                new Category { Id = 13, Name = "Miscellaneous" }
            );
        }
    }
}

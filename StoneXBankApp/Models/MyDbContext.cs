using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace StoneXBankApp.Models
{
    public class MyDbContext : DbContext
    {
        public DbSet<BankAccount> bankAccounts { get; set; }
        public DbSet<Currency> currencies { get; set; }
        public DbSet<Profile> profiles { get; set; }
        public DbSet<Transaction> transactions { get; set; }
        public DbSet<SavedContactData> savedContactData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=BankApp.db");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccount>()
                .HasOne(e => e.Currency)
                .WithMany();
            modelBuilder.Entity<BankAccount>()
                .HasOne(e => e.Profile)
                .WithMany();
            modelBuilder.Entity<Transaction>()
                .HasOne(e => e.sender)
                .WithMany();

            modelBuilder.Entity<Transaction>()
                .HasOne(e => e.recipient)
                .WithMany();

            modelBuilder.Entity<Currency>()
                .HasData(
                new Currency() { Id = 1, Name = "PLN" },
                new Currency() { Id = 2, Name = "USD" },
                new Currency() { Id = 3, Name = "EUR" });
            base.OnModelCreating(modelBuilder);
        }
    }
}

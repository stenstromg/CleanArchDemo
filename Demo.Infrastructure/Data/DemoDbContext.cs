using Demo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.Extensions.Configuration;

namespace Demo.Infrastructure.Data
{
    public class DemoDbContext : DbContext
    {
        #region properties

        string? _connectionString { get; set; }

        #endregion properties

        #region ctor

        public DemoDbContext(DbContextOptions options, IConfiguration config) : base(options)
        {
        }

        #endregion ctor

        #region properties

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Email> Emails { get; set; }    

        public DbSet<Person> People { get; set; }

        public DbSet<PhoneNumber> PhoneNumbers { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<UserLogin> UserLogins { get; set; }

        #endregion properties

        #region event handlers

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            string? dd = _connectionString;
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Email>()
                .HasOne(email => email.Contact)
                .WithMany(contact => contact.Emails);

            //// Sets up the One-to-Many relationship between the Contact (1) and Email (many)
            ////
            //modelBuilder.Entity<Email>()
            //    .HasOne<Contact>(e => e.Contact)
            //    .WithMany(c => c.Emails);

            //// Sets up the One-to-Many relationship between the Contact (1) and PhoneNumber (many)
            ////
            //modelBuilder.Entity<PhoneNumber>()
            //    .HasOne<Contact>(e => e.Contact)
            //    .WithMany(c => c.PhoneNumbers);


            base.OnModelCreating(modelBuilder);
        }

        #endregion event handlers

        #region private 

        static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }

        #endregion private
    }
}

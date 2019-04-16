namespace Final_Assignment_jccummin_wpf.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataCollection : DbContext
    {
        public DataCollection()
            : base("name=DataConnection")
        {
        }

        public virtual DbSet<Reciept> Reciepts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reciept>()
                .Property(e => e.Expense)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Reciept>()
                .Property(e => e.Credit)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Reciept>()
                .Property(e => e.Balance)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Reciept>()
                .Property(e => e.TotalCash)
                .HasPrecision(10, 2);
        }
    }
}

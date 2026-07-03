using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.Data
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public List<InvoiceItem> Items { get; set; } = new();
    }

    public class InvoiceItem
    {
        public int ItemID { get; set; }
        public int InvoiceID { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public Invoice? Invoice { get; set; }
    }

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Invoice> Invoices => Set<Invoice>();
        public DbSet<InvoiceItem> InvoiceItems => Set<InvoiceItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>().HasKey(i => i.InvoiceID);
            modelBuilder.Entity<InvoiceItem>().HasKey(i => i.ItemID);

            modelBuilder.Entity<InvoiceItem>()
                .HasOne(ii => ii.Invoice)
                .WithMany(i => i.Items)
                .HasForeignKey(ii => ii.InvoiceID);

            // Seed data (matches init.sql)
            modelBuilder.Entity<Invoice>().HasData(
                new Invoice { InvoiceID = 1, CustomerName = "John Doe" }
            );
            modelBuilder.Entity<InvoiceItem>().HasData(
                new InvoiceItem { ItemID = 1, InvoiceID = 1, Name = "Widget A", Price = 19.99 },
                new InvoiceItem { ItemID = 2, InvoiceID = 1, Name = "Widget B", Price = 5.50 }
            );
        }
    }
}

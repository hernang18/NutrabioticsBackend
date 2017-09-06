namespace NutraBioticsBackend.Models
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<NutraBioticsBackend.Models.ShipTo> ShipToes { get; set; }

        public DbSet<NutraBioticsBackend.Models.Contact> Contacts { get; set; }

        public DbSet<NutraBioticsBackend.Models.Part> Parts { get; set; }

        public DbSet<NutraBioticsBackend.Models.PriceList> PriceLists { get; set; }

        public DbSet<NutraBioticsBackend.Models.PriceListPart> PriceListParts { get; set; }

        public DbSet<NutraBioticsBackend.Models.CustomerPriceList> CustomerPriceLists { get; set; }

        public DbSet<NutraBioticsBackend.Models.Country> Countries { get; set; }

        public DbSet<NutraBioticsBackend.Models.Vendor> Vendors { get; set; }

        public DbSet<NutraBioticsBackend.Models.Company> Companies { get; set; }

        public DbSet<NutraBioticsBackend.Models.Plant> Plants { get; set; }

        public DbSet<NutraBioticsBackend.Models.Calendar> Calendars { get; set; }

        public DbSet<NutraBioticsBackend.Models.InvoiceHeader> InvoiceHeaders { get; set; }

        public DbSet<NutraBioticsBackend.Models.CashHeader> CashHeaders { get; set; }

        public DbSet<NutraBioticsBackend.Models.InvoiceDetail> InvoiceDetails { get; set; }

        public DbSet<NutraBioticsBackend.Models.OrderHeader> OrderHeaders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<NutraBioticsBackend.Models.OrderDetailTmp> OrderDetailTmp { get; set; }

        public System.Data.Entity.DbSet<NutraBioticsBackend.Models.Territory> Territories { get; set; }

        public System.Data.Entity.DbSet<NutraBioticsBackend.Models.PersonContact> PersonContacts { get; set; }

        public System.Data.Entity.DbSet<NutraBioticsBackend.Models.SalesRep> SalesReps { get; set; }
    }
}
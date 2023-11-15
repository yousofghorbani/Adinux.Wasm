using Microsoft.EntityFrameworkCore;

namespace Adinux.Wasm.Server.Models
{
    public class EFContext:DbContext
    {
        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {
        }

        public DbSet<UserForm> UserForms { get; set; }
        public DbSet<CatalogueRequest> CatalogueRequests { get; set; }
        public DbSet<ResumeRequest> ResumeRequests { get; set; }
        public DbSet<RepresentationRequest> RepresentationRequests { get; set; }
        public DbSet<PriceRequest> PriceRequests { get; set; }
        public DbSet<CatalogueRequestSendType> CatalogueRequestSendTypes { get; set; }
        public DbSet<ResumeRequestSendType> ResumeRequestSendTypes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //optionsBuilder.UseSqlServer(@"Server=.;Database=BMSHashemi;User Id=admin;Password=aA123456;",
            //    options => options.EnableRetryOnFailure()); 
            optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

       //     modelBuilder.Entity<Product>()
       //.Property(b => b.IsActive)
       //.HasDefaultValue(true);

       //     modelBuilder.Entity<ProductVariant>()
       //.Property(b => b.IsActive)
       //.HasDefaultValue(true);

       //     modelBuilder.Entity<ProductVariant>()
       //    .Property(b => b.Bias)
       //    .HasDefaultValue(0);


        }
    }
}

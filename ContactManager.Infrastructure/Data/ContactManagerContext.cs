using ContactManager.Core.Entities;
using Microsoft.EntityFrameworkCore;


namespace ContactManager.Infrastructure.Data
{

    public class ContactManagerContext : DbContext
    {
        public ContactManagerContext() { }
        public ContactManagerContext(DbContextOptions<ContactManagerContext> options) : base(options)
        {
        }

        
        public DbSet<LogInfo> LogInfo { get; set; }
        public DbSet<Contacts> Contacts { get; set; }



        //SQL View Type context need to comment at migration time
        //SQL View Type context need to comment at migration time

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=10.11.0.16;Initial Catalog=dbContactManagerV1;Persist Security Info=True;User ID=sa;Password=p@ssw0rd1111;Encrypt=False");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Intentionally rolling back this change to fix issue: https://github.com/dotnet-architecture/eShopOnWeb/issues/292
            //Will follow up after issue has been resolved.
            //base.OnModelCreating(builder);
            //builder.ApplyAllConfigurationsFromCurrentAssembly();

            // alternately this is built-in to EF Core 2.2
            //builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
           
            
           
            
        }

        


       

        
        
    }
}

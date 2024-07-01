using ContactManager.Core.Entities;
using ContactManager.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace ContactManager.Web.Data.Seeders
{
    public static class ContactDataSeeder
    {
        public static async Task SeedDataAsync(IServiceProvider services, ILogger logger)
        {
            var context = services.GetRequiredService<ContactManagerContext>();
            if (!context.Contacts.Any())
            {
                context.Contacts.AddRange(
                AddContacts());

                await context.SaveChangesAsync();
            }

        }

        static IEnumerable<Contacts> AddContacts()
        {
            return new List<Contacts>()
            {
                new Contacts() { FirstName = "David",
                LastName = "Warner",
                PhoneNumber = "8174821740",
                EmailAddress = "davidwarner@yopmail.com",
                DOB = new DateTime(1990,05,21),
                CompanyName = "Google Inc",
                Lat="40.769790",
                Long="-111.888130",
                Address="12 North , Salt lake city , Utah , USA - 84101"
                },
                new Contacts() { FirstName = "Michel",
                LastName = "Jackson",
                PhoneNumber = "8174821790",
                EmailAddress = "mochel@yopmail.com",
                DOB = new DateTime(1995,05,21),
                CompanyName = "Yahoo Inc",
                Lat="40.758540",
                Long="-111.891710",
                Address="18 South , Salt lake city , Utah , USA - 84101"
                },
                new Contacts() { FirstName = "Michel",
                LastName = "Clark",
                PhoneNumber = "8174821740",
                EmailAddress = "clark@yopmail.com",
                DOB = new DateTime(1997,05,21),
                CompanyName = "Microsoft Inc",
                Lat="40.758430",
                Long="-111.890130",
                Address="20 East, Salt lake city , Utah , USA - 84101"
                },
            };
        }


    }
}

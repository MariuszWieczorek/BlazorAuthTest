using MwTech.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MwTech.Infrastructure.Persistence.Extensions;
static class ModelBuilderExtensionsResource
{
    public static void SeedResources(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Resource>().HasData(
                       new Resource
                       {
                           Id = 1,
                           Name = "nd",
                           ResourceNumber = "nd",
                           Description = "nd",
                           ProductCategoryId = 1
                       },

                       new Resource
                       {
                           Id = 2,
                           Name = "OSOBA",
                           ResourceNumber = "osoba",
                           Description = "osoba",
                           ProductCategoryId = 1
                       },

                       new Resource
                       {
                           Id = 3,
                           Name = "PRASA",
                           ResourceNumber = "Prasa",
                           Description = "prasa",
                           ProductCategoryId = 1
                       },

                       new Resource
                       {
                           Id = 4,
                           Name = "KONF",
                           ResourceNumber = "KONF",
                           Description = "maszyna konfekcyjna",
                           ProductCategoryId = 1
                       },

                       new Resource
                       {
                           Id = 5,
                           Name = "DRUTOWKA",
                           ResourceNumber = "DRUT",
                           Description = "drutówka",
                           ProductCategoryId = 1
                       },

                       new Resource
                       {
                           Id = 6,
                           Name = "LWB",
                           ResourceNumber = "LWB",
                           Description = "linia wytłaczania bieżnika",
                           ProductCategoryId = 1
                       }
                   );
    }

}


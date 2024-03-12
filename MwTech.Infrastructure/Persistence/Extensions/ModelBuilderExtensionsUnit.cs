using MwTech.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MwTech.Infrastructure.Persistence.Extensions;
static class ModelBuilderExtensionsUnit
{
    public static void SeedUnits(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Unit>().HasData(
              new ProductCategory
              {
                  Id = 1,
                  Name = "Ogólny",
                  Description = "ogólny"
              },
              new ProductCategory
              {
                  Id = 2,
                  Name = "Mieszanka",
                  Description = "Mieszanka"
              },

              new ProductCategory
              {
                  Id = 3,
                  Name = "Opona",
                  Description = "Opona"
              },

               new ProductCategory
               {
                   Id = 4,
                   Name = "Dętka",
                   Description = "Dętka"
               },

               new ProductCategory
               {
                   Id = 5,
                   Name = "Wytłaczany",
                   Description = "Wytłaczany"
               },

               new ProductCategory
               {
                   Id = 6,
                   Name = "Formowany",
                   Description = "Formowany"
               });
    }

}


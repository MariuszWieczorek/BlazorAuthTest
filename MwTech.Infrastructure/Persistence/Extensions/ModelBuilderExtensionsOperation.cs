using MwTech.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MwTech.Infrastructure.Persistence.Extensions;
static class ModelBuilderExtensionsOperation
{
    public static void SeedOperations(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Operation>().HasData(
                   new Operation
                   {
                       Id = 1,
                       OperationNumber = "nd",
                       Name = "nd",
                       Description = "nd"
                   },

                   new Operation
                   {
                       Id = 2,
                       OperationNumber = "OP_BIEZNIK_WYTL",
                       Name = "Opony - Wytłaczanie bieżnika",
                       Description = "wytłaczanie bieżnika"
                   },

                   new Operation
                   {
                       Id = 3,
                       OperationNumber = "OP_DRUTOWKA_PROD",
                       Name = "Opony - Produkcja Drutówki",
                       Description = "produkcja drutówki"
                   },

                   new Operation
                   {
                       Id = 4,
                       OperationNumber = "OP_KONFEKCJA",
                       Name = "Opony - Konfekcja Opony",
                       Description = "konfekcja opony"
                   },

                   new Operation
                   {
                       Id = 5,
                       OperationNumber = "OP_CIECIE_KORDU",
                       Name = "Opony - Cięcie Kordu",
                       Description = "cięcie kordu"
                   }


               );
    }

}


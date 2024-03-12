using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities
{
    public class ProductVersionProperty
    {
        public int Id { get; set; }


        [Display(Name = "Cecha")]
        public Property? Property { get; set; }

        [Display(Name = "Cecha")]
        public int PropertyId { get; set; }


        [Display(Name = "Wersja Produktu")]
        public ProductVersion? ProductVersion { get; set; }

        [Display(Name = "Wersja Produktu")]
        public int ProductVersionId { get; set; }

        [Display(Name = "Wartość Cel")]
        public decimal? Value { get; set; }

        [Display(Name = "Wartość Min")]
        public decimal? MinValue { get; set; }

        [Display(Name = "Wartość Max")]
        public decimal? MaxValue { get; set; }

        [Display(Name = "Tekst")]
        public string? Text { get; set; }

    }
}

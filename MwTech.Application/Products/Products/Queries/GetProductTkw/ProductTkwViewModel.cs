using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.Products.Queries.GetProductTkw
{
    public class ProductTkwViewModel
    {
        public int ProductId { get; set; }
        public string ProductNumber { get; set; }
        public List<ProductTkw> ListOfProductTkw { get; set; }
    }
}

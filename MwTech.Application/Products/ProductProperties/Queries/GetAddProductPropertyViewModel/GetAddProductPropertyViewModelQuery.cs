using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductProperties.Queries.GetAddProductPropertyViewModel;

public class GetAddProductPropertyViewModelQuery : IRequest<AddProductPropertyViewModel>
{
    public int ProductPropertiesVersionId { get; set; }
}

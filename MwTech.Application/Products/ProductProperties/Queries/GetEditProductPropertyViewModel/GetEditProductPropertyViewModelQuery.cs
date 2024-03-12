using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductProperties.Queries.GetEditProductPropertyViewModel;

public class GetEditProductPropertyViewModelQuery : IRequest<EditProductPropertyViewModel>
{
    public int ProductPropertyId { get; set; }
    public int ProductPropertiesVersionId { get; set; }

}

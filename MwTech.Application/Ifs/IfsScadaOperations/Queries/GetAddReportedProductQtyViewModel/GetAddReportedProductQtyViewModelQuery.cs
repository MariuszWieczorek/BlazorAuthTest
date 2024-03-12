using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Ifs.IfsScadaOperations.Queries.GetAddReportedProductQtyViewModel;

public class GetAddReportedProductQtyViewModelQuery : IRequest<AddReportedProductQtyViewModel>
{
    public int OP_ID { get; set; }
}

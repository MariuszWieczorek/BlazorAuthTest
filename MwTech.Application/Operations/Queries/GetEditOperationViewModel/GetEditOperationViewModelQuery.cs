using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Operations.Queries.GetEditOperationViewModel;

public class GetEditOperationViewModelQuery : IRequest<EditOperationViewModel>
{
    public int Id { get; set; }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Props.Queries.GetEditPropertyViewModel;

public class GetEditPropertyViewModelQuery : IRequest<EditPropertyViewModel>
{
    public int Id { get; set; }
}

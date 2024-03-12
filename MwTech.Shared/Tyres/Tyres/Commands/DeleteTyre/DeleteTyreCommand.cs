using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Shared.Tyres.Tyres.Commands.DeleteTyre;

public class DeleteTyreCommand : IRequest
{
    public int Id { get; set; }
}

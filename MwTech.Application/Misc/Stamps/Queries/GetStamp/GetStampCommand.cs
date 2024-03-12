using MediatR;
using MwTech.Application.Misc.Stamps.Commands.SendStampToPrinter;

namespace MwTech.Application.Misc.Stamps.Queries.GetStamp;

public class GetStampCommand : IRequest<SendStampToPrinterCommand>
{
    public string Stamp { get; set; }

}

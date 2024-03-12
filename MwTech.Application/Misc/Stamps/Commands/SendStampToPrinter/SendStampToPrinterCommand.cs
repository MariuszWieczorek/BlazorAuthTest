using MediatR;


namespace MwTech.Application.Misc.Stamps.Commands.SendStampToPrinter;

public class SendStampToPrinterCommand : IRequest
{
    public string StampPathToDisplay { get; set; }
    public string StampPathToCopy { get; set; }
    public string StampName { get; set; }

}

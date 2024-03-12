using MediatR;
using MwTech.Application.Misc.Stamps.Commands.SendStampToPrinter;

namespace MwTech.Application.Misc.Stamps.Queries.GetStamp;

internal class GetStampCommandHandler : IRequestHandler<GetStampCommand, SendStampToPrinterCommand>
{

    public GetStampCommandHandler()
    {
    }

    public async Task<SendStampToPrinterCommand> Handle(GetStampCommand request, CancellationToken cancellationToken)
    {

        string sourceImg = null;

        string sourcePath = @"\\kab-svr-oradb01\ifs_pieczatki";
        if (request.Stamp != null)
        {
            sourceImg = Path.Combine(sourcePath, request.Stamp);
        }
        
            
        
        string base64ImageRepresentation = string.Empty;
        string img = null;

        if (File.Exists(sourceImg))
        {
            byte[] imageArray = File.ReadAllBytes(sourceImg);
            base64ImageRepresentation = Convert.ToBase64String(imageArray);
            img = "data:image/png;base64," + base64ImageRepresentation;
        }
        else
        {
            sourceImg = null;
        }

        var stamp = new SendStampToPrinterCommand
        {
            StampPathToDisplay = img,
            StampPathToCopy = sourceImg,
            StampName = request.Stamp,
        };


        return stamp;
    }




}

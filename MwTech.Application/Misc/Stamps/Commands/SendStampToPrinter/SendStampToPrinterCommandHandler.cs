using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Common.Tools;
using MwTech.Domain.Entities;


namespace MwTech.Application.Misc.Stamps.Commands.SendStampToPrinter;

public class SendStampToPrinterCommandHandler : IRequestHandler<SendStampToPrinterCommand>
{
    private readonly IConfiguration _configuration;

    public SendStampToPrinterCommandHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task Handle(SendStampToPrinterCommand request, CancellationToken cancellationToken)
    {

        // dodać w menedżerze poświadczeń serwera login: file pass: 12345678
        string path2 = @"\\kab-svr-oradb01\ifs_pieczatki\backup";

        string destinationPath =  _configuration.GetSection("PathToStampPrinter").Value;
        string user =  _configuration.GetSection("ReaUser").Value;
        string pass =  _configuration.GetSection("ReaPass").Value;
        string img3 = Path.Combine(destinationPath, "stamp.png");


        NetworkShare.DisconnectFromShare(destinationPath, true); //Disconnect in case we are currently connected with our credentials;

        NetworkShare.ConnectToShare(destinationPath, user, pass); //Connect with the new credentials

        if (request.StampPathToCopy != null)
        {
            File.Copy(request.StampPathToCopy, img3, true);
        }

        NetworkShare.DisconnectFromShare(destinationPath, false); //Disconnect from the server.

        return;
    }
}

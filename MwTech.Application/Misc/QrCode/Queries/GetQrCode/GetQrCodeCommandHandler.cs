using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Misc.QrCode.Queries.GetQrCode;

internal class GetQrCodeCommandHandler : IRequestHandler<GetQrCodeCommand, QrCodeToPrint>
{
    private readonly IQrCodeGenerator _QrCodeGenerator;

    public GetQrCodeCommandHandler(IQrCodeGenerator QrCodeGenerator)
    {
        _QrCodeGenerator = QrCodeGenerator;
    }

    public async Task<QrCodeToPrint> Handle(GetQrCodeCommand request, CancellationToken cancellationToken)
    {
        var QrCode = new QrCodeToPrint
        {
            QrCodeText = request.TextToBeEncoded,
            QrCodeImg = _QrCodeGenerator.Get(request.TextToBeEncoded)
        };

        return QrCode;

    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Misc.QrCode.Queries.GetQrCode;

public class GetQrCodeCommand : IRequest<QrCodeToPrint>
{
    public string TextToBeEncoded { get; set; }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Common.Interfaces;


public interface IQrCodeGenerator
{
    string Get(string message);
    
}

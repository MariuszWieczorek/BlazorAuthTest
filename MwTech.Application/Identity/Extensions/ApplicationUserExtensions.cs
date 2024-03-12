using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Identity.Extensions;

public static class ApplicationUserExtensions
{

    public static string FullName(this ApplicationUser val)
    {
        
        return val.FirstName + " " + val.LastName;
    }
}

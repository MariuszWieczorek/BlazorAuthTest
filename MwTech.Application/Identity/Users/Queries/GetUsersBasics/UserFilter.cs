using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Application.Identity.Users.Queries.GetUsersBasics;

public class UserFilter
{
    [Display(Name = "Nazwa")]
    public string UserName { get; set; }

}

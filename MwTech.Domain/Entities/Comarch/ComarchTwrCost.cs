using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities.Comarch;

public class ComarchTwrCost
{
    public string ProductNumber { get; set; }
    public decimal Cost { get; set; }
    public string Document { get; set; }

}


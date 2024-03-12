using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities.Comarch;

public class ComarchLoopedIndex
{
    public string LoopedIndex { get; set; }
    public string NoLoopedIndex { get; set; }

}


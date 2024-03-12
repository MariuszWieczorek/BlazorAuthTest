using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities;

public class TreeViewTest
{
    public int? ObjectId { get; set; }
    public string ObjectName { get; set; }
    public int? ParentId { get; set; }
    public string ParentName { get; set; }

}

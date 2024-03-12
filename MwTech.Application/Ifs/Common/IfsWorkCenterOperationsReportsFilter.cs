using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Application.Ifs.Common;

public class IfsWorkCenterOperationsReportsFilter
{

    [Display(Name = "Indeks Produktu")]
    public string ProductNumber { get; set; }

    [Display(Name = "Zlecenie")]
    public string OrderNo { get; set; }

    [Display(Name = "Gniazdo")]
    public string WorkCenterNo { get; set; }

    [Display(Name = "Dział")]
    public string DepartmentNo { get; set; }

    [Display(Name = "Linia")]
    public string ProductionLine { get; set; }

    [Display(Name = "Zmiana")]
    public string Shift { get; set; }

    [Display(Name = "Początek")]
    public DateTime? StartDateFrom { get; set; }

    [Display(Name = "Koniec")]
    public DateTime? StartDateTo { get; set; }
}

using MediatR;
using System.ComponentModel.DataAnnotations;


namespace MwTech.Application.AccountingPeriods.Commands.AddAccountingPeriod;

public class AddAccountingPeriodCommand : IRequest
{

    [Display(Name = "Symbol")]
    public string PeriodNumber { get; set; }

    [Display(Name = "Nazwa")]
    public string Name { get; set; }


    [Display(Name = "Data Od")]
    public DateTime StartDate { get; set; }


    [Display(Name = "Data Do")]
    public DateTime EndDate { get; set; }

    [Display(Name = "Aktywny")]
    public bool IsActive { get; set; }

    [Display(Name = "Domyślny")]
    public bool IsDefault { get; set; }

    [Display(Name = "Zamknięty")]
    public bool IsClosed { get; set; }

    [Display(Name = "Opis")]
    public string? Description { get; set; }

}

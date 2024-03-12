using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Units.Commands.EditUnit;

public class EditUnitCommand : IRequest
{

    public int Id { get; set; }

    [Required(ErrorMessage = "Pole 'Kod Jednostki miary' jest wymagany.")]
    [DisplayName("Kod Jednostki Miary")]
    public string UnitCode { get; set; }

    [Required(ErrorMessage = "Pole 'Nazwa' jest wymagane.")]
    [DisplayName("Nazwa")]
    public string Name { get; set; }

    [DisplayName("Opis")]
    public string? Description { get; set; }

    [Display(Name = "Waga")]
    public bool Weight { get; set; }

    [Display(Name = "Czas")]
    public bool Time { get; set; }

    [Display(Name = "Cost")]
    public bool Cost { get; set; }

    [Display(Name = "Logiczna")]
    public bool Boolean { get; set; }

    [Display(Name = "Ikona")]
    public string Icon { get; set; }

    [Display(Name = "okres w sekundach")]
    public int PeriodInSeconds { get; set; }
}

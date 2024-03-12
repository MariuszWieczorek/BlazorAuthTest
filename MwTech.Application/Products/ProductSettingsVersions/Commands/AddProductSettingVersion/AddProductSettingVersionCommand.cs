using MediatR;
using MwTech.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MwTech.Application.Products.ProductSettingVersions.Commands.AddProductSettingVersion;

public class AddProductSettingVersionCommand : IRequest
{

    [Required(ErrorMessage = "Pole 'Numer Wersji' jest wymagane")]
    [Display(Name = "Numer Wersji")]
    public int ProductSettingVersionNumber { get; set; }


    [Required(ErrorMessage = "Pole 'Numer Wariantu' jest wymagane")]
    [Display(Name = "Numer Wariantu")]
    public int AlternativeNo { get; set; }


    [Display(Name = "Domyślna")]
    public bool DefaultVersion { get; set; }

    [Display(Name = "Aktywna")]
    public bool IsActive { get; set; } = true;

    [Required(ErrorMessage = "Pole 'Nazwa Wersji' jest wymagane")]
    [Display(Name = "Nazwa Wersji")]
    public string Name { get; set; } = string.Empty;


    [Display(Name = "Produkt")]
    public Product? Product { get; set; }

     [Required(ErrorMessage = "Pole 'Produkt' jest wymagane")]
    [Display(Name = "Produkt")]
    public int ProductId { get; set; }


    [Display(Name = "Kategoria Maszyny")]
    public MachineCategory? MachineCategory { get; set; }

    
    [Required(ErrorMessage = "Pole 'Kategoria Maszyny' jest wymagane")]
    [Display(Name = "Kategoria Maszyny")]
    public int MachineCategoryId { get; set; }


    [Display(Name = "Maszyna")]
    public Machine? Machine { get; set; }


    [Display(Name = "Maszyna")]
    public int MachineId { get; set; }

    [Display(Name = "Gniazdo")]
    public Resource? WorkCenter { get; set; }


    [Display(Name = "Gniazdo")]
    public int WorkCenterId { get; set; }


    [Display(Name = "Opis")]
    public string? Description { get; set; }




    // akceptacja level 1
    [Display(Name = "akceptacja 1")]
    public bool IsAccepted01 { get; set; }
    [Display(Name = "akceptacja 1 przez")]
    public string? Accepted01ByUserId { get; set; }
    [Display(Name = "akceptacja 1 przez")]
    public ApplicationUser? Accepted01ByUser { get; set; }
    [Display(Name = "akceptacja 1 czas")]
    public DateTime? Accepted01Date { get; set; }


    // akceptacja level 2
    [Display(Name = "akceptacja 2")]
    public bool IsAccepted02 { get; set; }
    [Display(Name = "akceptacja 2 przez")]
    public string? Accepted02ByUserId { get; set; }
    [Display(Name = "akceptacja 2 przez")]
    public ApplicationUser? Accepted02ByUser { get; set; }
    [Display(Name = "akceptacja 2 czas")]
    public DateTime? Accepted02Date { get; set; }


    // akceptacja level 3
    [Display(Name = "akceptacja 2")]
    public bool IsAccepted03 { get; set; }
    [Display(Name = "akceptacja 2 przez")]
    public string? Accepted03ByUserId { get; set; }
    [Display(Name = "akceptacja 2 przez")]
    public ApplicationUser? Accepted03ByUser { get; set; }
    [Display(Name = "akceptacja 2 czas")]
    public DateTime? Accepted03Date { get; set; }


    // utworzenie wersji
    public DateTime? CreatedDate { get; set; }
    public string? CreatedByUserId { get; set; }
    public ApplicationUser? CreatedByUser { get; set; }

    public DateTime? ModifiedDate { get; set; }
    public string? ModifiedByUserId { get; set; }
    public ApplicationUser? ModifiedByUser { get; set; }
    public int MwbaseId { get; set; }


    public string VersionToCopyName { get; set; }
    public int VersionToCopyNumber { get; set; }

    [Display(Name = "wersja do skopiowania")]
    public int VersionToCopyId { get; set; }


}

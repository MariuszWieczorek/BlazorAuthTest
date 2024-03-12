﻿using MediatR;
using MwTech.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MwTech.Application.Products.RouteVersions.Commands.EditRouteVersion;

public class EditRouteVersionCommand : IRequest
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Pole 'Numer Wersji' jest wymagane")]
    [Display(Name = "Numer Wersji")]
    public int VersionNumber { get; set; }

    [Required(ErrorMessage = "Pole 'Numer Wariantu' jest wymagane")]
    [Display(Name = "Numer Wariantu")]
    public int AlternativeNo { get; set; }

    [Display(Name = "Domyślna")]
    public bool DefaultVersion { get; set; }

    [Display(Name = "Do IFS")]
    public bool ToIfs { get; set; }


    [Display(Name = "Czy aktywne")]
    public bool IsActive { get; set; }

    [Display(Name = "Nazwa Wersji")]
    public string Name { get; set; } = String.Empty;

    [Display(Name = "Produkt")]
    public Product? Product { get; set; }
    public int ProductId { get; set; }

    [Display(Name = "ilość produktu")]
    public decimal ProductQty { get; set; } = 1;


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


    // utworzenie wersji
    public DateTime? CreatedDate { get; set; }
    public string? CreatedByUserId { get; set; }
    public ApplicationUser? CreatedByUser { get; set; }

    public DateTime? ModifiedDate { get; set; }
    public string? ModifiedByUserId { get; set; }
    public ApplicationUser? ModifiedByUser { get; set; }
    public int MwbaseId { get; set; }

    [Display(Name = "Kategoria Produktu")]
    public int? ProductCategoryId { get; set; }

}

using MediatR;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MwTech.Application.Products.Routes.Commands.AddRoute;

public class AddRouteCommand : IRequest
{

    [Display(Name = "Wersja Marszruty")]
    public RouteVersion? RouteVersion { get; set; }

    [Display(Name = "Wersja Marszruty")]
    public int RouteVersionId { get; set; }


    [Display(Name = "Produkt")]
    public int ProductId { get; set; }


    [Display(Name = "Produkt")]
    public Product Product { get; set; }

    [Display(Name = "Kolejność")]
    public int OrdinalNumber { get; set; }

    [Display(Name = "Operacja")]
    public Operation? Operation { get; set; }

    [Display(Name = "Operacja")]
    public int OperationId { get; set; }

    [Display(Name = "Gniazdo")]
    public int WorkCenterId { get; set; }

    [Display(Name = "Gniazdo")]
    public Resource? WorkCenter { get; set; }


    [Display(Name = "Kategoria zaszeregowania")]
    public int ResourceId { get; set; }


    [Display(Name = "Ilość Pracowników")]
    public Decimal ResourceQty { get; set; }

    public Resource? Resource { get; set; }


    [Display(Name = "Pracochłonność")]
    public Decimal OperationLabourConsumption { get; set; }

    [Display(Name = "Maszynochłonność")]
    public Decimal OperationMachineConsumption { get; set; }



    [Display(Name = "Przezbrojenie - Pracownik")]
    public int? ChangeOverResourceId { get; set; }

    [Display(Name = "Przezbrojenie zasób")]
    public Resource? ChangeOverResource { get; set; }

    [Display(Name = "Przezbrojenie - ilość pracowników")]
    public decimal ChangeOverNumberOfEmployee { get; set; }

    [Display(Name = "Przezbrojenie - pracochłonność")]
    public decimal ChangeOverLabourConsumption { get; set; }

    [Display(Name = "Przezbrojenie - maszynochłonność")]
    public decimal ChangeOverMachineConsumption { get; set; }
    

    [Display(Name = "Zachodzenie")]
    public decimal Overlap { get; set; }

    [Display(Name = "Czas Transportu")]
    public decimal MoveTime { get; set; }

    [Display(Name = "Kategoria Produktu")]
    public int? ProductCategoryId { get; set; }

    [Display(Name = "Narzędzie")]
    public int? RoutingToolId { get; set; }

    public RoutingTool RoutingTool { get; set; }
    
    [Display(Name = "Ilość Narzędzi")]
    public int? ToolQuantity { get; set; }
}

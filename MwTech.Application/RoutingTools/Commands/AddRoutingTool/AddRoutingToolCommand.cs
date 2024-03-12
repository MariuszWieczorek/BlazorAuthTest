using MediatR;
using System.ComponentModel.DataAnnotations;


namespace MwTech.Application.RoutingTools.Commands.AddRoutingTool;

public class AddRoutingToolCommand : IRequest
{
    [Required(ErrorMessage = "Pole 'Symbol' jest wymagane")]
    [Display(Name = "Symbol")]
    public string ToolNumber { get; set; }


    [Required(ErrorMessage = "Pole 'Nazwa' jest wymagane")]
    [Display(Name = "Nazwa")]
    public string Name { get; set; }

}

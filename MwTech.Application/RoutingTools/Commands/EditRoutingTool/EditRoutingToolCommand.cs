using MediatR;
using System.ComponentModel.DataAnnotations;


namespace MwTech.Application.RoutingTools.Commands.EditRoutingTool;

public class EditRoutingToolCommand : IRequest
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Pole 'Symbol' jest wymagane")]
    [Display(Name = "Symbol")]
    public string ToolNumber { get; set; }

    
    [Required(ErrorMessage = "Pole 'Nazwa' jest wymagane")]
    [Display(Name = "Nazwa")]
    public string Name { get; set; }
}

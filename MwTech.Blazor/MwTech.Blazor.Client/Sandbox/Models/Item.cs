using System.ComponentModel.DataAnnotations;

namespace MwTech.Blazor.Client.Sandbox.Models;

public class Item
{
    [Required(ErrorMessage = "Pole 'Opis' jest wymagane.")]
    public string Description { get; set; }
    public State State { get; set; }
}

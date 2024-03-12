using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities;

// Ustawienie przypisane do wersji ustawień.
public class ProductSettingVersionPosition
{
    public int Id { get; set; }
    public int ProductSettingVersionId { get; set; }
    public ProductSettingVersion? ProductSettingVersion { get; set; }
    public int SettingId { get; set; }
    public Setting? Setting { get; set; }
    public decimal? MinValue { get; set; }
    public decimal? Value { get; set; }
    public decimal? MaxValue { get; set; }
    public string? Text { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime? ModifiedDate { get; set; }
    public string? ModifiedByUserId { get; set; }
    public ApplicationUser? ModifiedByUser { get; set; }
    public int MwbaseId { get; set; }
}

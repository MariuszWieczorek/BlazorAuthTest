using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductSettingsVersionsPositions.Commands.ImportProductSettingVersionPositionFromExcel;

public class SettingFromExcelToImport

{
    public int MachineCategoryId { get; set; }
    public int MachineId { get; set; }
    public int WorkCenterId { get; set; }
    public int ProductId { get; set; }
    public int AltNo { get; set; }
    public int VersionNo { get; set; }
    public string VersionName { get; set; }
    public int SettingId { get; set; }
    public decimal? MinValue { get; set; }
    public decimal? Value { get; set; }
    public decimal? MaxValue { get; set; }
    public string Text { get; set; }
}

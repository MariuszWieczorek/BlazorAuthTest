

using MwTech.Domain.Entities;
using MwTech.Domain.Enums;

namespace MwTech.Application.Common.Interfaces;

public interface IProductCsvService
{
    Task GenerateCsv(int productId, int ProductSettingVersionId, CsvTrigger csvTrigger);
    Task GenerateStartOrderCsv(string productNumber, decimal qty, int number, string workCenterNo, int operationId);
}

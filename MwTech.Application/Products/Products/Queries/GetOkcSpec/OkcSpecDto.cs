
namespace MwTech.Application.Products.Products.Queries.GetOkcSpec;
public class OkcSpecDto
{
    public int Id { get; set; }
    public string ProductNumber { get; set; }
    public string Name { get; set; }
    public int TechCardNumber { get; set; }

    public decimal OkcSzerokosc { get; set; }
    public decimal OkcKatCiecia { get; set; }
    public decimal OkgWagaGramNaM2 { get; set; }

    public decimal OkcPrzelMbNaKg { get; set; }


    public decimal DistanceBeetwenCuts { get; set; }
    public decimal CordArea { get; set; }
    public decimal CordWeight { get; set; }


}

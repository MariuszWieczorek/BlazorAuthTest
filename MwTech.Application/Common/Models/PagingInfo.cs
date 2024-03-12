// Model Widoku do przekazywania danych między kontrolerem a widokiem
// aby zapewnić prawidłowe działanie atrybutów pomocniczych znaczników 
// Przechowujemy w nim informację:
// o ogólnej liczbie pozycji
// o wyświetlanej ilości pozycji na stronie
// o bieżącej stronie
// oraz o ilości stron Wyliczamy dzieląc ogólną liczbę pozycji przez ilość pozycji na stronie

namespace MwTech.Application.Common.Models;

public class PagingInfo
{
    public int TotalItems { get; set; }
    public int ItemsPerPage { get; set; } = 1;
    public int CurrentPage { get; set; }
    public int TotalPages =>
        (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
}

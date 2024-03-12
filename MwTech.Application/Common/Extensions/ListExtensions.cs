namespace MwTech.Application.Common.Extensions;

public static class ListExtensions
{
    public static List<T> Except<T, TKey>(this List<T> items, List<T> other, Func<T, TKey> getKeyFunc)
    {
        return items
            .GroupJoin(other, getKeyFunc, getKeyFunc, (item, tempItems) => new { item, tempItems })
            .SelectMany(x => x.tempItems.DefaultIfEmpty(), (x, temp) => new { x, temp })
            .Where(x => ReferenceEquals(null, x.temp) || x.temp.Equals(default(T)))
            .Select(x => x.x.item)
            .ToList();
    }
}

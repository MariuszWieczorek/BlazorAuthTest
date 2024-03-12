using MwTech.Application.Common.Interfaces;


namespace MwTech.Infrastructure.Services;

public class DateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.Now;
}

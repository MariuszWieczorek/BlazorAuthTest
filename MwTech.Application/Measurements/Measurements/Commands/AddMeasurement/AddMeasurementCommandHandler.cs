using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using MwTech.Domain.Entities.Measurements;
using Unit = MediatR.Unit;

namespace MwTech.Application.Measurements.Measurements.Commands.AddMeasurement;

public class AddMeasurementCommandHandler : IRequestHandler<AddMeasurementCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUserService;

    public AddMeasurementCommandHandler(IApplicationDbContext context, IDateTimeService dateTimeService, ICurrentUserService currentUserService)
    {
        _context = context;
        _dateTimeService = dateTimeService;
        _currentUserService = currentUserService;
    }
    public async Task Handle(AddMeasurementCommand request, CancellationToken cancellationToken)
    {
        var dateTime = _dateTimeService.Now;

        var measurementHeaderToAdd = new MeasurementHeader
        {
            CreatedDate = dateTime,
            CreatedByUserId = _currentUserService.UserId,
            ProductId = request.ProductId.GetValueOrDefault(),
            Shift = cacluculateShift(dateTime),
        };

        _context.MeasurementHeaders.Add(measurementHeaderToAdd);

        var measurmentPositionToAdd = new MeasurementPosition
        {
            MeasurementHeader = measurementHeaderToAdd,
            Value = request.Value.GetValueOrDefault(),
        };

        _context.MeasurementPositions.Add(measurmentPositionToAdd);

        await _context.SaveChangesAsync();

        return;
    }

    private int cacluculateShift(DateTime dateTime)
    {
        int shift = 0;
        var currentHour = dateTime.Hour;

        if (currentHour >= 6 && currentHour < 14)
        {
            shift = 1;
        }

        if (currentHour >= 14 && currentHour < 22)
        {
            shift = 2;
        }

        if (currentHour >= 22 || (currentHour >= 0 && currentHour < 6))
        {
            shift = 3;
            }

        return shift;

    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities.Measurements;

namespace MwTech.Application.Measurements.Measurements.Queries.GetMeasurements;

public class GetMeasurementsQueryHandler : IRequestHandler<GetMeasurementsQuery, GetMeasurementsViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly IProductService _productService;

    public GetMeasurementsQueryHandler(IApplicationDbContext context, IProductService productService)
    {
        _context = context;
        _productService = productService;
    }
    public async Task<GetMeasurementsViewModel> Handle(GetMeasurementsQuery request, CancellationToken cancellationToken)
    {
        var Measurements =  _context.MeasurementPositions
            .Include(x=>x.MeasurementHeader)
            .Include(x=>x.MeasurementHeader.Product)
            .Include(x=>x.MeasurementHeader.CreatedByUser)
            .OrderByDescending(x=>x.Id)
            .AsNoTracking()
            .AsQueryable();

        Measurements = Filter(Measurements, request.MeasurementFilter);

        // stronicowanie
        if (request.PagingInfo != null)
        {

            request.PagingInfo.TotalItems = Measurements.Count();
            request.PagingInfo.ItemsPerPage = 100;

            if (request.PagingInfo.ItemsPerPage > 0 && request.PagingInfo.TotalItems > 0)
                Measurements = Measurements
                    .Skip((request.PagingInfo.CurrentPage - 1) * request.PagingInfo.ItemsPerPage)
                    .Take(request.PagingInfo.ItemsPerPage);
        }

        var measurements = await Measurements
            .OrderByDescending(x=>x.Id)
            .ToListAsync();

        foreach (var measurement in measurements) 
        {
            decimal targetWeightInKg = Math.Round( await _productService.CalculateWeight(measurement.MeasurementHeader.ProductId, 0), 2 );
            decimal minWeightInKg = Math.Round(targetWeightInKg - targetWeightInKg * 0.05M,2);
            decimal maxWeightInKg = Math.Round(targetWeightInKg + targetWeightInKg * 0.05M,2);

            measurement.TargetValue = targetWeightInKg;
            measurement.MinValue = minWeightInKg;
            measurement.MaxValue = maxWeightInKg;

        }


        var vm = new GetMeasurementsViewModel
            { 
              Measurements = measurements,
              MeasurementFilter = request.MeasurementFilter,
              PagingInfo = request.PagingInfo,
            };

        return vm;
           
    }


    public IQueryable<MeasurementPosition> Filter(IQueryable<MeasurementPosition> measurements, MeasurementFilter filter)
    {
        if (filter != null)
        {
            if (filter.DateTimeFrom != null)
                measurements = measurements.Where(x => x.MeasurementHeader.CreatedDate >= filter.DateTimeFrom);

            if (filter.DateTimeTo != null)
                measurements = measurements.Where(x => x.MeasurementHeader.CreatedDate <= filter.DateTimeTo);

            if (filter.ProductNumber != null)
                measurements = measurements.Where(x => x.MeasurementHeader.Product.ProductNumber.Contains(filter.ProductNumber));

        }

        return measurements;
    }
}

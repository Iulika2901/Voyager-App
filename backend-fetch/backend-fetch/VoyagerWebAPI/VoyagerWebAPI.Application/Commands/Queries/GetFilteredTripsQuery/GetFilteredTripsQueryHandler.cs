using MediatR;
using VoyagerWebAPI.Domain;
using VoyagerWebAPI.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace VoyagerWebAPI.Application.Queries.GetFilteredTrips;

public class GetFilteredTripsQueryHandler : IRequestHandler<GetFilteredTripsQuery, List<Trip>>
{
    private readonly ITripRepository _repository; 

    public GetFilteredTripsQueryHandler(ITripRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Trip>> Handle(GetFilteredTripsQuery request, CancellationToken cancellationToken)
    {
        var allTrips = await _repository.GetAllAsync();
        
        var query = allTrips.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(request.DestinationName))
        {
            query = query.Where(t => t.Destination != null && 
                                     t.Destination.Name.Contains(request.DestinationName, StringComparison.OrdinalIgnoreCase));
        }

        if (request.Status.HasValue)
        {
            query = query.Where(t => t.Status == request.Status.Value);
        }

        if (request.MaxBudget.HasValue)
        {
            query = query.Where(t => t.Budget <= request.MaxBudget.Value);
        }

        return query.ToList(); 
    }
}
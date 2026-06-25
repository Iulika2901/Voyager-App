using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VoyagerWebAPI.Application.DTOs;
using VoyagerWebAPI.Application.Interfaces;

namespace VoyagerWebAPI.Application.Queries.GetAll;

public class GetAllTripsQueryHandler : IRequestHandler<GetAllTripsQuery, IEnumerable<TripDto>>
{
    private readonly ITripRepository _repository;

    public GetAllTripsQueryHandler(ITripRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<IEnumerable<TripDto>> Handle(GetAllTripsQuery request, CancellationToken cancellationToken)
    {
        var trips = await _repository.GetAllAsync();

        return trips.Select(trip => new TripDto(
            trip.Id,
            trip.Name,
            new DestinationDto(trip.Destination.Name, trip.Destination.Country),
            trip.StartDate,
            trip.EndDate,
            trip.Status.ToString(),
            trip.Budget,
            trip.TotalExpenses,
            trip.RemainingBudget,
            trip.DurationInDays,
            trip.Expenses.Select(e => new ExpenseDto(e.Description, e.Amount, e.Category.ToString(), e.Date)).ToList()
        ));
    }
}
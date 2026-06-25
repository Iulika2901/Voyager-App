using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VoyagerWebAPI.Application.DTOs;
using VoyagerWebAPI.Application.Interfaces;

namespace VoyagerWebAPI.Application.Queries.GetById;

public class GetTripByIdQueryHandler : IRequestHandler<GetTripByIdQuery, TripDto>
{
    private readonly ITripRepository _repository;

    public GetTripByIdQueryHandler(ITripRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<TripDto> Handle(GetTripByIdQuery request, CancellationToken cancellationToken)
    {
        var trip = await _repository.GetByIdAsync(request.Id);

        if (trip == null) return null;

        return new TripDto(
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
        );
    }
}
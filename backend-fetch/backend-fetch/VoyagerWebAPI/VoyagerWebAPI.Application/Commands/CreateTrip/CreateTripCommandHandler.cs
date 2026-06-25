using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VoyagerWebAPI.Application.DTOs;
using VoyagerWebAPI.Application.Interfaces;
using VoyagerWebAPI.Domain;

namespace VoyagerWebAPI.Application.Commands.CreateTrip;

public class CreateTripCommandHandler : IRequestHandler<CreateTripCommand, TripDto>
{
    private readonly ITripRepository _repository;

    public CreateTripCommandHandler(ITripRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<TripDto> Handle(CreateTripCommand request, CancellationToken cancellationToken)
    {
        var destination = new Destination(request.DestinationName, request.DestinationCountry);
        
        var trip = new Trip(
            request.Name, 
            destination, 
            request.StartDate, 
            request.EndDate, 
            request.Status, 
            request.Budget);

        var createdTrip = await _repository.CreateAsync(trip);

        return new TripDto(
            createdTrip.Id,
            createdTrip.Name,
            new DestinationDto(createdTrip.Destination.Name, createdTrip.Destination.Country),
            createdTrip.StartDate,
            createdTrip.EndDate,
            createdTrip.Status.ToString(),
            createdTrip.Budget,
            createdTrip.TotalExpenses,
            createdTrip.RemainingBudget,
            createdTrip.DurationInDays,
            createdTrip.Expenses.Select(e => new ExpenseDto(e.Description, e.Amount, e.Category.ToString(), e.Date)).ToList()
        );
    }
}
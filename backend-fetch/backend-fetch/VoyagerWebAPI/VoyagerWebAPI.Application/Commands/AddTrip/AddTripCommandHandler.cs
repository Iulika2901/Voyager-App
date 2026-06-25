using VoyagerWebAPI.Application.Interfaces;
using VoyagerWebAPI.Application.DTOs;
using VoyagerWebAPI.Application.Commands.AddTrip;
using VoyagerWebAPI.Domain;
using MediatR;

namespace VoyagerWebAPI.Application.Commands.AddTrip;

public class AddTripCommandHandler : IRequestHandler<AddTripCommand, TripDto>
{
    private readonly ITripRepository _repository;

    public AddTripCommandHandler(ITripRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<TripDto> Handle(AddTripCommand request, CancellationToken cancellationToken)
    {
        var tripToCreate = new Trip(
            request.Name,
            request.Destination, 
            request.StartDate,
            request.EndDate,
            request.Status,
            request.Budget
        );
        
        var createdTrip = await _repository.CreateAsync(tripToCreate);
    
        var destinationDto = new DestinationDto(
            createdTrip.Destination.Name, 
            createdTrip.Destination.Country 
        );
        return new TripDto(
            createdTrip.Id,                 // int
            createdTrip.Name,               // string
            destinationDto,                 // DestinationDto
            createdTrip.StartDate,          // DateTime
            createdTrip.EndDate,            // DateTime
            createdTrip.Status.ToString(),  // string 
            createdTrip.Budget,             // decimal
            createdTrip.TotalExpenses,      // decimal 
            createdTrip.RemainingBudget,    // decimal 
            createdTrip.DurationInDays,     // int 
            new List<ExpenseDto>()          // List<ExpenseDto> 
        );
    }
}
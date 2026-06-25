using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VoyagerWebAPI.Application.DTOs;
using VoyagerWebAPI.Application.Interfaces;
using VoyagerWebAPI.Domain;

namespace VoyagerWebAPI.Application.Commands.UpdateTrip;

public class UpdateTripCommandHandler : IRequestHandler<UpdateTripCommand, TripDto>
{
    private readonly ITripRepository _repository;

    public UpdateTripCommandHandler(ITripRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<TripDto> Handle(UpdateTripCommand request, CancellationToken cancellationToken)
    {
        var existingTrip = await _repository.GetByIdAsync(request.Id) 
                           ?? throw new ArgumentException($"Trip with ID {request.Id} not found.");

        existingTrip.Name = request.Name;
        existingTrip.Destination = new Destination(request.DestinationName, request.DestinationCountry);
        existingTrip.StartDate = request.StartDate;
        existingTrip.EndDate = request.EndDate;
        existingTrip.Status = request.Status;
        existingTrip.Budget = request.Budget;

        var updatedTrip = await _repository.UpdateAsync(request.Id, existingTrip);

        return new TripDto(
            updatedTrip.Id,
            updatedTrip.Name,
            new DestinationDto(updatedTrip.Destination.Name, updatedTrip.Destination.Country),
            updatedTrip.StartDate,
            updatedTrip.EndDate,
            updatedTrip.Status.ToString(),
            updatedTrip.Budget,
            updatedTrip.TotalExpenses,
            updatedTrip.RemainingBudget,
            updatedTrip.DurationInDays,
            updatedTrip.Expenses.Select(e => new ExpenseDto(e.Description, e.Amount, e.Category.ToString(), e.Date)).ToList()
        );
    }
}
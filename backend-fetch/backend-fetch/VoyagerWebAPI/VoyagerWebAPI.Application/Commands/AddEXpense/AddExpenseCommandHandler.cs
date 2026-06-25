using VoyagerWebAPI.Application.Interfaces;
using VoyagerWebAPI.Application.Commands.AddExpense;
using VoyagerWebAPI.Application.DTOs;
using VoyagerWebAPI.Domain;
using MediatR;

namespace VoyagerWebAPI.Application.Commands.AddExpense;

public class AddExpenseCommandHandler : IRequestHandler<AddExpenseCommand, TripDto>
{
    private readonly ITripRepository _repository;

    public AddExpenseCommandHandler(ITripRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<TripDto> Handle(AddExpenseCommand request, CancellationToken cancellationToken)
    {
        var trip = await _repository.GetByIdAsync(request.TripId)
                   ?? throw new KeyNotFoundException($"Trip-ul cu ID-ul {request.TripId} nu a fost găsit.");

        var newExpense = new Expense(
            request.Description,
            request.Amount,
            request.Category,
            request.Date
        );

        trip.Expenses.Add(newExpense);

        var updatedTrip = await _repository.UpdateAsync(trip.Id, trip);

        var destinationDto = new DestinationDto(updatedTrip.Destination.Name, updatedTrip.Destination.Country);
        
        var expensesDtoList = updatedTrip.Expenses.Select(e => new ExpenseDto(
            e.Description, 
            e.Amount, 
            e.Category.ToString(), 
            e.Date
        )).ToList();

        return new TripDto(
            updatedTrip.Id,
            updatedTrip.Name,
            destinationDto,
            updatedTrip.StartDate,
            updatedTrip.EndDate,
            updatedTrip.Status.ToString(),
            updatedTrip.Budget,
            updatedTrip.TotalExpenses,
            updatedTrip.RemainingBudget,
            updatedTrip.DurationInDays,
            expensesDtoList
        );
    }
}
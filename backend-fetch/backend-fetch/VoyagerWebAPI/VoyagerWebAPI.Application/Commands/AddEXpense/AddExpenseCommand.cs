namespace VoyagerWebAPI.Application.Commands.AddExpense;

using MediatR;
using VoyagerWebAPI.Application.DTOs;
using VoyagerWebAPI.Domain; 
public record AddExpenseCommand(
    int TripId,
    string Description,
    decimal Amount,
    ExpenseCategory Category, 
    DateTime Date
) : IRequest<TripDto>;
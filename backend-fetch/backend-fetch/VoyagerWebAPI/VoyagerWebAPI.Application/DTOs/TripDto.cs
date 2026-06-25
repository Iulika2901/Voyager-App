namespace VoyagerWebAPI.Application.DTOs;

public record TripDto(
    int Id, 
    string Name, 
    DestinationDto Destination, 
    DateTime StartDate, 
    DateTime EndDate, 
    string Status, 
    decimal Budget, 
    decimal TotalExpenses, 
    decimal RemainingBudget, 
    int DurationInDays, 
    List<ExpenseDto> Expenses);
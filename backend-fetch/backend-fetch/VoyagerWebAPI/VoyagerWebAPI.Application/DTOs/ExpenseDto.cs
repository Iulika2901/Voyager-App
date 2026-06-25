namespace VoyagerWebAPI.Application.DTOs;

public record ExpenseDto(
    string Description,
    decimal Amount,
    string Category,
    DateTime Date   
    );
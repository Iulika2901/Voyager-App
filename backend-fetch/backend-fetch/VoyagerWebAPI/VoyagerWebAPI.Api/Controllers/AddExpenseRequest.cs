using System;
using VoyagerWebAPI.Domain;

namespace VoyagerWebAPI.Api.Controllers;

public record AddExpenseRequest(
    string Description, 
    decimal Amount, 
    ExpenseCategory Category, 
    DateTime Date
);
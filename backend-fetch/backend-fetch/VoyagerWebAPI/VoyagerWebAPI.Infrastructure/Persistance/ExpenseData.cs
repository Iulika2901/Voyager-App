using System;
using VoyagerWebAPI.Domain;

namespace VoyagerWebAPI.Infrastructure.Persistence;

public class ExpenseData
{
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public ExpenseCategory Category { get; set; }
    public DateTime Date { get; set; }
}
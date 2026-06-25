using System;

namespace VoyagerWebAPI.Domain;

public class Expense
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public ExpenseCategory Category { get; set; } 
    public DateTime Date { get; set; }

    private Expense() { }
    
    public Expense(string description, decimal amount, ExpenseCategory category, DateTime date)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than 0");

        Description = description;
        Amount = amount;
        Category = category;
        Date = date;
    }
}
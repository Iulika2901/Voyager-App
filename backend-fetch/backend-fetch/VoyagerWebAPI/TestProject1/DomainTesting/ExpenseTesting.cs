using System;
using Xunit;
using VoyagerWebAPI.Domain; 

using Assert = Xunit.Assert;
using TheoryAttribute = Xunit.TheoryAttribute;

namespace TestProject1;

public class ExpenseTesting
{
    [Fact]
    public void Constructor_WithValidData()
    {
        // Arrange
        var description = "Taxi to airport";
        var amount = 2000m; 
        var category = ExpenseCategory.Transport; 
        var date = new DateTime(2024, 5, 15);

        // Act
        var expense = new Expense(description, amount, category, date);

        // Assert
        Assert.NotNull(expense);
        Assert.Equal(description, expense.Description);
        Assert.Equal(amount, expense.Amount);
        Assert.Equal(category, expense.Category);
        Assert.Equal(date, expense.Date);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-2000)]
    [InlineData(-0.01)]
    public void Constructor_WithInvalidAmount(decimal invalidAmount)
    {
        // Arrange
        var description = "Invalid expense";
        var category = ExpenseCategory.Transport;
        var date = DateTime.Now;

        // Act
        Action act = () => new Expense(description, invalidAmount, category, date);

        // Assert
        var exception = Assert.Throws<ArgumentException>(act);
        
    }
}
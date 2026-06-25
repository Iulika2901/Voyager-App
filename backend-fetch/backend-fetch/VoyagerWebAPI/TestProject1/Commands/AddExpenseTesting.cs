using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using VoyagerWebAPI.Application.Commands.AddExpense;
using VoyagerWebAPI.Application.DTOs;
using VoyagerWebAPI.Application.Interfaces;
using VoyagerWebAPI.Domain;

using Assert = Xunit.Assert;
using TheoryAttribute = Xunit.TheoryAttribute;


namespace TestProject1;

public class AddExpenseCommandHandlerTests
{
     [Fact]
    public async Task Handle_ValidCommand()
    {
        // Arrange
        var mockRepository = new Mock<ITripRepository>();
        
        var command = new AddExpenseCommand(
            TripId: 1,
            Description: "Cina la restaurant",
            Amount: 150.50m,
            Category: ExpenseCategory.Food,  
            Date: DateTime.Now
        );

        var existingTrip = new Trip(
            "Euro Trip", 
            new Destination("Paris", "France"), 
            DateTime.Now, 
            DateTime.Now.AddDays(5), 
            TripStatus.Planning, 
            2000m
        );
       
        mockRepository.Setup(repo => repo.GetByIdAsync(command.TripId))
            .ReturnsAsync(existingTrip);

        mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<int>(), existingTrip))
            .ReturnsAsync(existingTrip);

        var handler = new AddExpenseCommandHandler(mockRepository.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Single(existingTrip.Expenses); 
        Assert.Equal(command.Description, existingTrip.Expenses[0].Description);
        Assert.Equal(command.Amount, existingTrip.Expenses[0].Amount);
        
        mockRepository.Verify(repo => repo.GetByIdAsync(command.TripId), Times.Once);
        mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<int>(), existingTrip), Times.Once);
    }

     [Fact]
    public async Task Handle_TripNotFound()
    {
        // Arrange
        var mockRepository = new Mock<ITripRepository>();
        var command = new AddExpenseCommand(99, "Bilet muzeu", 50m, ExpenseCategory.Activities, DateTime.Now);

        mockRepository.Setup(repo => repo.GetByIdAsync(command.TripId))
            .ReturnsAsync((Trip)null);

        var handler = new AddExpenseCommandHandler(mockRepository.Object);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(
            () => handler.Handle(command, CancellationToken.None)
        );

        Assert.Equal($"Trip-ul cu ID-ul {command.TripId} nu a fost găsit.", exception.Message);
        
        mockRepository.Verify(repo => repo.GetByIdAsync(command.TripId), Times.Once);
        mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<int>(), It.IsAny<Trip>()), Times.Never);
    }
}
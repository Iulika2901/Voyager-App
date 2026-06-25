using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using VoyagerWebAPI.Application.Commands.AddExpense;
using VoyagerWebAPI.Application.DTOs;
using VoyagerWebAPI.Domain;

using VoyagerWebAPI.Api.Controllers; 
using Assert = Xunit.Assert;
using TheoryAttribute = Xunit.TheoryAttribute;


namespace TestProject1;

public class TripControllerTests
{
    [Fact]
    public async Task AddExpense()
    {
         var mockMediator = new Mock<IMediator>();
        var controller = new TripController(mockMediator.Object);
        
         var request = new AddExpenseRequest(
            "Masa de pranz", 
            100m, 
            ExpenseCategory.Food, 
            DateTime.Now
        );
        
        var expectedDto = new TripDto(
            1, "Euro Trip", new DestinationDto("Paris", "France"), 
            DateTime.Now, DateTime.Now.AddDays(5), "Pending", 2000m, 100m, 1900m, 5, 
            new List<ExpenseDto> { new ExpenseDto("Masa de pranz", 100m, "Food", DateTime.Now) }
        );

        mockMediator.Setup(m => m.Send(It.IsAny<AddExpenseCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedDto);

        // Act
        var result = await controller.AddExpense(1, request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedDto = Assert.IsType<TripDto>(okResult.Value);
        Assert.Equal(expectedDto.Id, returnedDto.Id);
        
        mockMediator.Verify(m => m.Send(It.IsAny<AddExpenseCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using VoyagerWebAPI.Application.Queries.GetAll;
using VoyagerWebAPI.Application.DTOs;
using VoyagerWebAPI.Application.Interfaces;
using VoyagerWebAPI.Domain;

using Assert = Xunit.Assert;
using TheoryAttribute = Xunit.TheoryAttribute;


namespace TestProject1;

public class GetAllTripsQueryHandlerTests
{
    [Fact]
    public async Task Handle_WhenTripsExist()
    {
        // Arrange
        var mockRepository = new Mock<ITripRepository>();
        var query = new GetAllTripsQuery();

        var trip1 = new Trip("Vacanta Paris", new Destination("Paris", "Franta"), DateTime.Now, DateTime.Now.AddDays(3), TripStatus.Completed, 1000m);
        var trip2 = new Trip("Vacanta Roma", new Destination("Roma", "Italia"), DateTime.Now, DateTime.Now.AddDays(5), TripStatus.Planning, 1500m);
        
        var tripsList = new List<Trip> { trip1, trip2 };

        mockRepository.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(tripsList);

        var handler = new GetAllTripsQueryHandler(mockRepository.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Equal(2, resultList.Count);
        Assert.Equal("Vacanta Paris", resultList[0].Name);
        Assert.Equal("Vacanta Roma", resultList[1].Name);
        
        mockRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenNoTripsExist()
    {
        // Arrange
        var mockRepository = new Mock<ITripRepository>();
        var query = new GetAllTripsQuery();

        mockRepository.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(new List<Trip>()); 

        var handler = new GetAllTripsQueryHandler(mockRepository.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result); 
    }
}
using System;
using Xunit;
using VoyagerWebAPI.Domain;

using Assert = Xunit.Assert;
using TheoryAttribute = Xunit.TheoryAttribute;

namespace TestProject1;

public class TripTests
{
    [Fact]
    public void Constructor_WithValidData()
    {
        // Arrange
        var name = "Euro Trip";
        var destination = new Destination("Paris", "France"); 
        var startDate = new DateTime(2024, 6, 1);
        var endDate = new DateTime(2024, 6, 10);
        var status = TripStatus.Planning; 
        var budget = 2000.00m;

        // Act
        var trip = new Trip(name, destination, startDate, endDate, status, budget);

        // Assert
        Assert.NotNull(trip);
        Assert.Equal(name, trip.Name);
        Assert.Equal(destination, trip.Destination);
        Assert.Equal(startDate, trip.StartDate);
        Assert.Equal(9, trip.DurationInDays); 
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Constructor_WithEmptyName(string invalidName)
    {
        // Arrange
        var destination = new Destination("Paris", "France");
        
        // Act
        Action act = () => new Trip(invalidName, destination, DateTime.Now, DateTime.Now.AddDays(2), TripStatus.Planning, 1000m);
        
        // Assert
        var exception = Assert.Throws<ArgumentException>(act);
        Assert.Equal("Name cannot be empty", exception.Message);
        
    }

    [Fact]
    public void Constructor_WithEndDateBeforeStartDate()
    {
        // Arrange
        var destination = new Destination("Paris", "France");
        var startDate = new DateTime(2024, 6, 10);
        var endDate = new DateTime(2024, 6, 1);

        // Act
        Action act = () => new Trip("Trip", destination, startDate, endDate, TripStatus.Planning, 1000m);

        // Assert
        var exception = Assert.Throws<ArgumentException>(act);
        Assert.Equal("End date cannot be before start date", exception.Message);
    }
    

    [Fact]
    public void Constructor_WithNullDestination()
    {
        // Act 
        Action act = () => new Trip("Trip", null, DateTime.Now, DateTime.Now.AddDays(2), TripStatus.Planning, 1000m);

        // Assert
        var exception = Assert.Throws<ArgumentNullException>(act);
        Assert.Equal("destination", exception.ParamName); }
}
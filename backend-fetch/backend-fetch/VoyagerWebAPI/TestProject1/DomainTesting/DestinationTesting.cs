using System;
using Xunit;
using VoyagerWebAPI.Domain; 

using Assert = Xunit.Assert;
using TheoryAttribute = Xunit.TheoryAttribute;


namespace TestProject1;

public class DestinationTests
{
    [Fact]
    public void Constructor_WithValidData()
    {
        // Arrange
        var validName = "Paris";
        var validCountry = "France";

        // Act
        var destination = new Destination(validName, validCountry);

        // Assert
        Assert.NotNull(destination);
        Assert.Equal(validName, destination.Name);
        Assert.Equal(validCountry, destination.Country);
    }

     [Theory]
    [InlineData("", "France")]      
    [InlineData("Paris", "")]        
    [InlineData("", "")]             
    [InlineData("   ", "France")]    
    [InlineData(null, "France")]     
    [InlineData("Paris", null)]      
    public void Constructor_WithInvalidData(string invalidName, string invalidCountry)
    {
        // Act
        Action act = () => new Destination(invalidName, invalidCountry);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }
}
using Microsoft.AspNetCore.Mvc;
using SunriseSunsetTracker.Api.Controllers;
using SunriseSunsetTracker.Api.Data.DTO;
using SunriseSunsetTracker.Api.Interfaces;
using SunriseSunsetTracker.Common.Contracts.Responses;

namespace SunsetSunriseTracker.Api.Tests.Controllers;

[TestFixture]
public class EventTimeControllerTestFixture
{
    private readonly EventTimeController _eventTimeController;
    private readonly Mock<ISunriseSunsetService> _sunriseSunsetServiceMock;

    public EventTimeControllerTestFixture()
    {
        _sunriseSunsetServiceMock = new Mock<ISunriseSunsetService>();
        _eventTimeController = new EventTimeController(_sunriseSunsetServiceMock.Object);

    }

    [Test]
    public async Task GetTodaySunriseSunsetTime_WithValidCoordinates_ReturnsOkResult()
    {
        // Arrange
        const double latitude = 12.34;
        const double longitude = 56.78;
        _sunriseSunsetServiceMock.Setup(x => x.GetSunriseSunsetTimeAsync(
            It.IsAny<double>(),
            It.IsAny<double>()))
            .ReturnsAsync(new GetSunriseSunsetResponse
            {
                
                Results = new SunriseSunsetInfo
                {
                    Sunrise = DateTime.Now.AddHours(6),
                    Sunset = DateTime.Now.AddHours(18)
                }
            });
        
        // Act
        var result = await _eventTimeController.GetTodaySunriseSunsetTime(latitude, longitude);

        // Assert
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        var okResult = (OkObjectResult)result;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult.Value, Is.InstanceOf<SunriseSunsetTime>());
    }
    
    [Test]
    public async Task GetTodaySunriseSunsetTime_WithInvalidCoordinates_ReturnsBadRequest()
    {
        // Arrange
        const int invalidLatitude = 100;
        const int invalidLongitude = 200;

        // Act
        var result = await _eventTimeController.GetTodaySunriseSunsetTime(invalidLatitude, invalidLongitude);

        // Assert
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }
    
    [Test]
    public async Task GetTodaySunriseTime_ValidCoordinates_ReturnsOkWithSunriseTime()
    {
        // Arrange
        const double latitude = 40.7128;
        const double longitude = -74.0060;
        
        _sunriseSunsetServiceMock.Setup(x => x.GetSunriseSunsetTimeAsync(
                It.IsAny<double>(),
                It.IsAny<double>()))
            .ReturnsAsync(new GetSunriseSunsetResponse
            {
                
                Results = new SunriseSunsetInfo
                {
                    Sunrise = DateTime.Now.AddHours(6),
                    Sunset = DateTime.Now.AddHours(18)
                }
            });

        // Act
        var result = await _eventTimeController.GetTodaySunriseTime(latitude, longitude);

        // Assert
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        var okResult = result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult!.Value, Is.InstanceOf<DateTime>());
    }

    [Test]
    public async Task GetTodaySunriseTime_InvalidCoordinates_ReturnsBadRequest()
    {
        // Arrange
        const double invalidLatitude = 100.0;
        const double invalidLongitude = -74.0060;

        // Act
        var result = await _eventTimeController.GetTodaySunriseTime(invalidLatitude, invalidLongitude);

        // Assert
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }
    
    [Test]
    public async Task GetTodaySunsetTime_ValidCoordinates_ReturnsOkWithSunriseTime()
    {
        // Arrange
        const double latitude = 40.7128;
        const double longitude = -74.0060;
        
        _sunriseSunsetServiceMock.Setup(x => x.GetSunriseSunsetTimeAsync(
                It.IsAny<double>(),
                It.IsAny<double>()))
            .ReturnsAsync(new GetSunriseSunsetResponse
            {
                
                Results = new SunriseSunsetInfo
                {
                    Sunrise = DateTime.Now.AddHours(6),
                    Sunset = DateTime.Now.AddHours(18)
                }
            });

        // Act
        var result = await _eventTimeController.GetTodaySunsetTime(latitude, longitude);

        // Assert
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        var okResult = result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult!.Value, Is.InstanceOf<DateTime>());
    }

    [Test]
    public async Task GetTodaySunsetTime_InvalidCoordinates_ReturnsBadRequest()
    {
        // Arrange
        const double invalidLatitude = 100.0;
        const double invalidLongitude = -74.0060;

        // Act
        var result = await _eventTimeController.GetTodaySunsetTime(invalidLatitude, invalidLongitude);

        // Assert
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

}
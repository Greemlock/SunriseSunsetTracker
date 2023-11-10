using Microsoft.AspNetCore.Mvc;
using SunriseSunsetTracker.Api.Controllers;
using SunriseSunsetTracker.Api.Data.DTO;
using SunriseSunsetTracker.Api.Interfaces;
using SunriseSunsetTracker.Api.Services;
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
    public async Task GetTodaySunriseSunsetTime_ValidCoordinates_ReturnsOkWithEventTimeResponse()
    {
        // Arrange
        const double validLatitude = 37.7749;
        const double validLongitude = -122.4194;
        var expectedSunriseTime = DateTime.Now;
        var expectedSunsetTime = DateTime.Now;

        var sunriseSunsetResponse = new GetSunriseSunsetResponse
        {
            Results = new SunriseSunsetInfo
            {
                Sunrise = expectedSunriseTime,
                Sunset = expectedSunsetTime
            }
        };

        _sunriseSunsetServiceMock.Setup(service =>
            service.GetSunriseSunsetTimeAsync(validLatitude, validLongitude)).ReturnsAsync(sunriseSunsetResponse);

        // Act
        var result = await _eventTimeController.GetTodaySunriseSunsetTime(validLatitude, validLongitude);

        // Assert
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        var okResult = result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(okResult!.Value, Is.Not.Null);
            Assert.That(okResult.Value, Is.InstanceOf<EventTimeResponseModel>());
        });
        
        var eventTimeResponse = okResult!.Value as EventTimeResponseModel;
        Assert.That(eventTimeResponse, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(eventTimeResponse!.SunriseTime, Is.EqualTo(expectedSunriseTime));
            Assert.That(eventTimeResponse.SunsetTime, Is.EqualTo(expectedSunsetTime));
        });
    }
}
using Microsoft.AspNetCore.Mvc;
using SunriseSunsetTracker.Api.Controllers;
using SunriseSunsetTracker.Api.Interfaces;
using SunriseSunsetTracker.Common.Database.Entities;

#pragma warning disable CS8618

namespace SunsetSunriseTracker.Api.Tests.Controllers;

[TestFixture]
public class CityControllerTestFixture
{
    private readonly Mock<IEntityRepository<City>> _cityRepositoryMock;
    private readonly CityController _cityController;

    public CityControllerTestFixture()
    {
        _cityRepositoryMock = new Mock<IEntityRepository<City>>();
        _cityController = new CityController(_cityRepositoryMock.Object);
    }
    
    [Test]
    public async Task GetCity_ValidId_ReturnsOk()
    {
        // Arrange
        const int validId = 1;
        var expectedCity = new City { Id = validId, Name = "TestCity" };
        _cityRepositoryMock.Setup(repo => repo.GetByIdAsync(validId)).ReturnsAsync(expectedCity);

        // Act
        var result = await _cityController.GetCity(validId);

        // Assert
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        var okResult = result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(okResult!.Value, Is.Not.Null);
            Assert.That(okResult.Value, Is.EqualTo(expectedCity));
        });
    }

    [Test]
    public async Task GetCity_InvalidId_ReturnsNotFound()
    {
        // Arrange
        const int invalidId = 999;
        _cityRepositoryMock.Setup(repo => repo.GetByIdAsync(invalidId)).ReturnsAsync((City)null!);

        // Act
        var result = await _cityController.GetCity(invalidId);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
    }
    
    [Test]
    public async Task GetAllCities_WithCities_ReturnsOk()
    {
        // Arrange
        var expectedCities = new List<City>
        {
            new City { Id = 1, Name = "TestCity1" }, 
            new City { Id = 2, Name = "TestCity2" }
        };
        _cityRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expectedCities);

        // Act
        var result = await _cityController.GetAllCities();

        // Assert
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        var okResult = result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(okResult!.Value, Is.Not.Null);
            Assert.That(okResult.Value, Is.EqualTo(expectedCities));
        });
    }

    [Test]
    public async Task GetAllCities_WithoutCities_ReturnsNotFound()
    {
        // Arrange
        _cityRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<City>());

        // Act
        var result = await _cityController.GetAllCities();

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
    }
    
    [Test]
    public async Task AddCity_ValidEntity_ReturnsCreated()
    {
        // Arrange
        var cityToAdd = new City { Id = 1, Name = "TestCity" };

        // Act
        var result = await _cityController.AddCity(cityToAdd);

        // Assert
        Assert.That(result, Is.InstanceOf<CreatedResult>());
        var createdResult = result as CreatedResult;
        Assert.That(createdResult, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(createdResult!.Value, Is.Not.Null);
            Assert.That(createdResult.Value, Is.EqualTo(cityToAdd));
        });
    }

    [Test]
    public async Task AddCity_NullEntity_ReturnsBadRequest()
    {
        // Act
        var result = await _cityController.AddCity(null);

        // Assert
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }
    
    [Test]
    public async Task UpdateCity_ValidEntity_ReturnsOk()
    {
        // Arrange
        var existingCity = new City { Id = 1, Name = "ExistingCity" };
        _cityRepositoryMock.Setup(repo => repo.GetByIdAsync(existingCity.Id)).ReturnsAsync(existingCity);

        var updatedCity = new City { Id = 1, Name = "UpdatedCity" };

        // Act
        var result = await _cityController.UpdateCity(updatedCity);

        // Assert
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        var okResult = result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(okResult!.Value, Is.Not.Null);
            Assert.That(okResult.Value, Is.EqualTo(updatedCity));
        });
    }

    [Test]
    public async Task UpdateCity_NullEntity_ReturnsBadRequest()
    {
        // Act
        var result = await _cityController.UpdateCity(null);

        // Assert
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task UpdateCity_NonExistingEntity_ReturnsNotFound()
    {
        // Arrange
        var nonExistingCity = new City { Id = 999, Name = "NonExistingCity" };
        _cityRepositoryMock.Setup(repo => repo.GetByIdAsync(nonExistingCity.Id)).ReturnsAsync((City)null!);

        // Act
        var result = await _cityController.UpdateCity(nonExistingCity);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
    }
    
    [Test]
    public async Task DeleteCity_ExistingId_ReturnsOk()
    {
        // Arrange
        const int existingId = 1;
        _cityRepositoryMock.Setup(repo => repo.GetByIdAsync(existingId)).ReturnsAsync(new City { Id = existingId, Name = "ExistingCity" });

        // Act
        var result = await _cityController.DeleteCity(existingId);

        // Assert
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        var okResult = result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(okResult!.Value, Is.Not.Null);
            Assert.That(okResult.Value, Is.EqualTo($"City {existingId} has been deleted."));
        });
    }

    [Test]
    public async Task DeleteCity_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        const int nonExistingId = 999;
        _cityRepositoryMock.Setup(repo => repo.GetByIdAsync(nonExistingId)).ReturnsAsync((City)null!);

        // Act
        var result = await _cityController.DeleteCity(nonExistingId);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
    }
}
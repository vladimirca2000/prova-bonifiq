using Moq;
using ProvaPub.Interfaces.Repositories;
using ProvaPub.Models;
using ProvaPub.Services;
using Xunit;

namespace ProvaPub.Tests;

public class OrderServiceTests
{
    private readonly Mock<IOrdemRepository> _orderRepositoryMock;
    private readonly OrderService _orderService;

    public OrderServiceTests()
    {
        _orderRepositoryMock = new Mock<IOrdemRepository>();
        _orderService = new OrderService(_orderRepositoryMock.Object);
    }

    [Fact]
    public async Task CanPurchase_ShouldReturnTrue_WhenCustomerCanPurchase()
    {
        // Arrange
        _orderRepositoryMock.Setup(repo => repo.Purchase(It.IsAny<int>())).ReturnsAsync(true);
        _orderRepositoryMock.Setup(repo => repo.CountAsync(It.IsAny<int>(), It.IsAny<DateTime>())).ReturnsAsync(0);

        // Act
        var result = await _orderService.CanPurchase(1, 50);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task CanPurchase_ShouldThrowException_WhenCustomerHasNotBoughtBeforeAndPurchaseValueIsGreaterThan100()
    {
        // Arrange
        _orderRepositoryMock.Setup(repo => repo.Purchase(It.IsAny<int>())).ReturnsAsync(false);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => _orderService.CanPurchase(1, 150));
        Assert.Equal("O cliente não comprou antes e o valor da compra é maior que 100", exception.Message);
    }

    [Fact]
    public async Task CanPurchase_ShouldThrowException_WhenCustomerHasAlreadyPurchasedThisMonth()
    {
        // Arrange
        _orderRepositoryMock.Setup(repo => repo.Purchase(It.IsAny<int>())).ReturnsAsync(true);
        _orderRepositoryMock.Setup(repo => repo.CountAsync(It.IsAny<int>(), It.IsAny<DateTime>())).ReturnsAsync(1);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => _orderService.CanPurchase(1, 50));
        Assert.Equal("O cliente já comprou este mês", exception.Message);
    }

    [Fact]
    public async Task CanPurchase_ShouldReturnTrue_WhenCustomerHasNotBoughtBeforeAndPurchaseValueIsLessThanOrEqualTo100()
    {
        // Arrange
        _orderRepositoryMock.Setup(repo => repo.Purchase(It.IsAny<int>())).ReturnsAsync(false);
        _orderRepositoryMock.Setup(repo => repo.CountAsync(It.IsAny<int>(), It.IsAny<DateTime>())).ReturnsAsync(0);

        // Act
        var result = await _orderService.CanPurchase(1, 100);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task CanPurchase_ShouldReturnTrue_WhenCustomerHasBoughtBeforeAndPurchaseValueIsGreaterThan100()
    {
        // Arrange
        _orderRepositoryMock.Setup(repo => repo.Purchase(It.IsAny<int>())).ReturnsAsync(true);
        _orderRepositoryMock.Setup(repo => repo.CountAsync(It.IsAny<int>(), It.IsAny<DateTime>())).ReturnsAsync(0);

        // Act
        var result = await _orderService.CanPurchase(1, 150);

        // Assert
        Assert.True(result);
    }

}

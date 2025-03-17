using ProvaPub.Interfaces.Repositories;
using ProvaPub.Services;
using Moq;
using Xunit;
using ProvaPub.Models;
using ProvaPub.Interfaces;

namespace ProvaPub.Tests;

public class CustomerServiceTests
{
    private readonly Mock<IOrderService> _orderServiceMock;
    private readonly Mock<ICustomerRepository> _customerRepositoryMock;
    private readonly CustomerService _customerService;

    public CustomerServiceTests()
    {
        _customerRepositoryMock = new Mock<ICustomerRepository>();
        _orderServiceMock = new Mock<IOrderService>();
        _customerService = new CustomerService(_customerRepositoryMock.Object, _orderServiceMock.Object);
    }

    [Fact]
    public async Task CanPurchase_ShouldThrowArgumentOutOfRangeException_WhenCustomerIdIsInvalid()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _customerService.CanPurchase(0, 50));
    }

    [Fact]
    public async Task CanPurchase_ShouldThrowArgumentOutOfRangeException_WhenPurchaseValueIsInvalid()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _customerService.CanPurchase(1, 0));
    }

    [Fact]
    public void CanPurchase_ShouldThrowInvalidOperationException_WhenCustomerDoesNotExist()
    {
        // Arrange
        var custumerId = 1;

        // Act
        var exception = Assert.Throws<Exception>(() => _customerService.CustumerExist(custumerId));

        //Assert
        Assert.Equal("O cliente não existe", exception.Message);
    }

    // Teste para verificar se esta dentro do horario de funcionamento
    [Fact]
    public void CanPurchase_WhenOutOfBusinessHours_ShouldThrowException()
    {
        // Arrange
        var hourPurchase = new DateTime(2022, 1, 1, 6, 0, 0);

        // Act
        var exception = Assert.Throws<Exception>(() => _customerService.HourPurchase(hourPurchase));

        // Assert
        Assert.Equal("Fora do horário de funcionamento", exception.Message);
    }
}

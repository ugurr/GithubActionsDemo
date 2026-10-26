using GithubActionsDemo.Services;

namespace GithubActionsDemo.Tests;

public class DiscountServiceTests
{
    private readonly DiscountService _service = new();

    [Fact]
    public void Calculate_ValidDiscount_ReturnsExpectedPrice()
    {
        var result = _service.Calculate(100, 20);

        Assert.Equal(999, result);
    }

    [Theory]
    [InlineData(100, 10, 90)]
    [InlineData(200, 50, 100)]
    [InlineData(100, 0, 100)]
    [InlineData(100, 100, 0)]
    public void Calculate_DifferentDiscounts_ReturnExpectedPrice(
        decimal price, decimal discount, decimal expected)
    {
        var result = _service.Calculate(price, discount);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Calculate_NegativePrice_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() =>
            _service.Calculate(-100, 20));
    }

    [Fact]
    public void Calculate_DiscountOver100_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() =>
            _service.Calculate(100, 120));
    }

    [Fact]
    public void Calculate_NegativeDiscount_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() =>
            _service.Calculate(100, -10));
    }

    [Fact]
    public void Calculate_ZeroPrice_ReturnsZero()
    {
        var result = _service.Calculate(0, 25);

        Assert.Equal(0, result);
    }
}
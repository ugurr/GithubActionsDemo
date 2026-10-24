namespace GithubActionsDemo.Services;

public class DiscountService
{
    public decimal Calculate(decimal price, decimal discountPercentage)
    {
        if (price < 0)
            throw new ArgumentException("Price cannot be negative");

        if (discountPercentage < 0 || discountPercentage > 100)
            throw new ArgumentException("Invalid discount percentage");

        return price - (price * discountPercentage / 100);
    }
}
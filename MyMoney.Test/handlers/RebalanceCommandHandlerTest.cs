using Moq;
using MyMoney.handlers;
using MyMoney.models;

namespace MyMoney.Test.handlers;

public class RebalanceCommandHandlerTest
{
    private readonly Mock<IPortfolioService> service;
    private readonly RebalanceCommandHandler handler;

    public RebalanceCommandHandlerTest()
    {
        service = new Mock<IPortfolioService>();
        handler = new RebalanceCommandHandler(service.Object);
    }

    [Fact]
    public void Execute_should_return_CANNOT_REBALANCE_if_portfolio_cannot_be_rebalanced()
    {
        var portfolio = new Portfolio(1000, 500);
        service.Setup(x => x.GetPortfolio()).Returns(portfolio);

        Assert.Equal("CANNOT_REBALANCE", handler.Execute());
    }

    [Fact]
    public void Execute_should_rebalance_portfolio()
    {
        var portfolio = new Portfolio(1000, 500);
        service.Setup(x => x.GetPortfolio()).Returns(portfolio);

        portfolio.UpdateMonthlyChange(Month.JANUARY, 10, 20);
        portfolio.UpdateMonthlyChange(Month.FEBRUARY, 10, 20);
        portfolio.UpdateMonthlyChange(Month.MARCH, 10, 20);
        portfolio.UpdateMonthlyChange(Month.APRIL, 10, 20);
        portfolio.UpdateMonthlyChange(Month.MAY, 10, 20);
        portfolio.UpdateMonthlyChange(Month.JUNE, 10, 20);

        Assert.Equal("2174 1087", handler.Execute());
    }
}
using Moq;
using MyMoney.handlers;
using MyMoney.models;

namespace MyMoney.Test.handlers;

public class BalanceCommandHandlerTest
{
    private readonly Mock<IPortfolioService> service;
    private readonly BalanceCommandHandler handler;

    public BalanceCommandHandlerTest()
    {
        service = new Mock<IPortfolioService>();
        handler = new BalanceCommandHandler(service.Object);
    }

    [Fact]
    public void Execute_should_return_portfolio_balances_for_given_month()
    {
        var portfolio = new Portfolio(1000, 500);
        service.Setup(x => x.GetPortfolio()).Returns(portfolio);
        Assert.Equal("1000 500", handler.Execute("JANUARY"));
    }
}
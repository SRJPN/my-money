using Moq;
using MyMoney.handlers;
using MyMoney.models;

namespace MyMoney.Test.handlers;

public class ChangeCommandHandlerTest
{
    private readonly Mock<IPortfolioService> service;
    private readonly ChangeCommandHandler handler;

    public ChangeCommandHandlerTest()
    {
        service = new Mock<IPortfolioService>();
        handler = new ChangeCommandHandler(service.Object);
    }

    [Fact]
    public void Execute_should_add_monthlyChange_to_portfolio()
    {
        var portfolio = new Portfolio(1000, 500);
        service.Setup(x => x.GetPortfolio()).Returns(portfolio);
        handler.Execute("10.00%", "20%", "JANUARY");

        Assert.Equal(new int[] { 1100, 600 }, portfolio.ShowBalances(Month.JANUARY));
    }
}
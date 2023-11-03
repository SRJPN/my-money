using Moq;
using MyMoney.handlers;
using MyMoney.models;
using Xunit;

namespace MyMoney.Test.handlers
{
    public class SIPCommandHandlerTest
    {
        private readonly Mock<IPortfolioService> service;
        private readonly SIPCommandHandler handler;

        public SIPCommandHandlerTest()
        {
            service = new Mock<IPortfolioService>();
            handler = new SIPCommandHandler(service.Object);
        }

        [Fact]
        public void Execute_should_add_sip_amounts_to_existing_portfolio()
        {
            var portfolio = new Portfolio(1000, 500);
            service.Setup(x => x.GetPortfolio()).Returns(portfolio);

            handler.Execute("100", "50");

            portfolio.UpdateMonthlyChange(Month.FEBRUARY, 0, 0);

            Assert.Equal(new int[] { 1100, 550 }, portfolio.ShowBalances(Month.FEBRUARY));
        }
    }
}
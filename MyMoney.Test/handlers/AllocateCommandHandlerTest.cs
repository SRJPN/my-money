using Moq;
using MyMoney.handlers;
using MyMoney.models;
using Xunit;

namespace MyMoney.Test.handlers
{
    public class AllocateCommandHandlerTest
    {
        private readonly Mock<IPortfolioService> service;
        private readonly AllocateCommandHandler handler;

        public AllocateCommandHandlerTest()
        {
            service = new Mock<IPortfolioService>();
            handler = new AllocateCommandHandler(service.Object);
        }

        [Fact]
        public void Execute_should_create_new_portfolio_with_allocated_asset_values()
        {
            service.Setup(x => x.InitializePortfolio(It.IsAny<Portfolio>())).Returns<Portfolio>(x => x);
            handler.Execute("1000", "500");

            service.Verify(x => x.InitializePortfolio(It.IsAny<Portfolio>()));
        }
    }
}
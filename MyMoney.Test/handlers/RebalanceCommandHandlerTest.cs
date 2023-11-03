using MyMoney.handlers;
using MyMoney.models;

namespace MyMoney.Test.handlers
{
    public class RebalanceCommandHandlerTest
    {
        private readonly RebalanceCommandHandler handler;

        public RebalanceCommandHandlerTest()
        {
            handler = new RebalanceCommandHandler();
        }

        [Fact]
        public void Execute_should_raise_execption_if_no_portfolio()
        {
            Portfolio.Instance = null;
            Assert.Throws<Exception>(() =>
            {
                handler.Execute("JANUARY");
            });
        }

        [Fact]
        public void Execute_should_return_CANNOT_REBALANCE_if_portfolio_cannot_be_rebalanced()
        {
            Portfolio.Instance = new Portfolio(1000, 500);
            Assert.Equal("CANNOT_REBALANCE", handler.Execute());
        }

        [Fact]
        public void Execute_should_rebalance_portfolio()
        {
            Portfolio.Instance = new Portfolio(1000, 500);
            Portfolio.Instance.UpdateMonthlyChange(Month.JANUARY, 10, 20);
            Portfolio.Instance.UpdateMonthlyChange(Month.FEBRUARY, 10, 20);
            Portfolio.Instance.UpdateMonthlyChange(Month.MARCH, 10, 20);
            Portfolio.Instance.UpdateMonthlyChange(Month.APRIL, 10, 20);
            Portfolio.Instance.UpdateMonthlyChange(Month.MAY, 10, 20);
            Portfolio.Instance.UpdateMonthlyChange(Month.JUNE, 10, 20);

            Assert.Equal("2174 1087", handler.Execute());
        }
    }
}
using MyMoney.handlers;
using MyMoney.models;

namespace MyMoney.Test.handlers
{
    public class BalanceCommandHandlerTest
    {
        private readonly BalanceCommandHandler handler;

        public BalanceCommandHandlerTest()
        {
            handler = new BalanceCommandHandler();
        }

        [Fact]
        public void Execute_should_raise_execption_if_no_portfolio()
        {
            Portfolio.Instance = null;
            Assert.Throws<Exception>(() => {
                handler.Execute("JANUARY");
            });
        }

        [Fact]
        public void Execute_should_return_portfolio_balances_for_given_month()
        {
            Portfolio.Instance = new Portfolio(1000, 500);
            Assert.Equal("1000 500", handler.Execute("JANUARY"));
        }
    }
}
using MyMoney.handlers;
using MyMoney.models;

namespace MyMoney.Test.handlers
{
    public class ChangeCommandHandlerTest
    {
        private readonly ChangeCommandHandler handler;

        public ChangeCommandHandlerTest()
        {
            handler = new ChangeCommandHandler();
        }

        [Fact]
        public void Execute_should_raise_execption_if_no_portfolio()
        {
            Portfolio.Instance = null;
            Assert.Throws<Exception>(() => {
                handler.Execute("10.00%", "20%", "JANUARY");
            });
        }

        [Fact]
        public void Execute_should_add_monthlyChange_to_portfolio()
        {
            Portfolio.Instance = new Portfolio(1000, 500);
            handler.Execute("10.00%", "20%", "JANUARY");

            Assert.Equal(new int[]{1100, 600}, Portfolio.Instance.ShowBalances(Month.JANUARY));
        }
    }
}
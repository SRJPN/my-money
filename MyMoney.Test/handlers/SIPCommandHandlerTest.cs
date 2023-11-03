using MyMoney.handlers;
using MyMoney.models;

namespace MyMoney.Test.handlers
{
    public class SIPCommandHandlerTest
    {
        private readonly SIPCommandHandler handler;

        public SIPCommandHandlerTest()
        {
            handler = new SIPCommandHandler();
        }

        [Fact]
        public void Execute_should_raise_execption_if_no_portfolio()
        {
            Portfolio.Instance = null;
            Assert.Throws<Exception>(() =>
            {
                handler.Execute("100", "50");
            });
        }

        [Fact]
        public void Execute_should_add_sip_amounts_to_existing_portfolio()
        {
            Portfolio.Instance = new Portfolio(1000, 500);

            handler.Execute("100", "50");

            Portfolio.Instance.UpdateMonthlyChange(Month.FEBRUARY, 0, 0);

            Assert.Equal(new int[] { 1100, 550 }, Portfolio.Instance.ShowBalances(Month.FEBRUARY));
        }
    }
}
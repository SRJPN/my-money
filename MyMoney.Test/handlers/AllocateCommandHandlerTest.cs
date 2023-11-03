using MyMoney.handlers;
using MyMoney.models;

namespace MyMoney.Test.handlers
{
    public class AllocateCommandHandlerTest
    {
        [Fact]
        public void Execute_should_create_new_portfolio_with_allocated_asset_values()
        {
            var handler = new AllocateCommandHandler();

            handler.Execute("1000", "500");

            Assert.Equal(new int[]{ 1000,500}, Portfolio.Instance.ShowBalances(Month.JANUARY));
        }
    }
}
using MyMoney.models;

namespace MyMoney.Test.models
{
    public class PortfolioTests
    {
        [Fact]
        public void Ctor_should_create_list_of_assets_with_initial_allocation_amounts()
        {
            var portfolio = new Portfolio(1000, 1500, 2000);

            Assert.IsType<Portfolio>(portfolio);
        }

        [Fact]
        public void ShowBalance_should_return_balances_of_all_assets()
        {
            var portfolio = new Portfolio(1000, 1500, 2000);

            var expected = new int[]{ 1000, 1500, 2000};

            Assert.Equal(expected, portfolio.ShowBalances(Month.JANUARY));
        }
    }
}
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

            var expected = new int[] { 1000, 1500, 2000 };

            Assert.Equal(expected, portfolio.ShowBalances(Month.JANUARY));
        }

        [Fact]
        public void AddSips_should_add_sipAmounts_to_respective_assets()
        {
            var portfolio = new Portfolio(1000, 1500, 2000);
            portfolio.AddSips(100, 150, 200);
            portfolio.UpdateMonthlyChange(Month.JANUARY, 0, 0, 0);
            portfolio.UpdateMonthlyChange(Month.FEBRUARY, 0, 0, 0);


            var expected = new int[] { 1100, 1650, 2200 };

            Assert.Equal(expected, portfolio.ShowBalances(Month.FEBRUARY));
        }

        [Fact]
        public void UpdateMonthlyChange_should_add_monthly_market_changes_to_respective_assets()
        {
            var portfolio = new Portfolio(1000, 1500, 2000);
            portfolio.UpdateMonthlyChange(Month.JANUARY, 10, 15, 20);

            var expected = new int[] { 1100, 1725, 2400 };

            Assert.Equal(expected, portfolio.ShowBalances(Month.JANUARY));
        }
    }
}
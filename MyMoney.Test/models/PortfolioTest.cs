using System;
using MyMoney.models;
using Xunit;

namespace MyMoney.Test.models
{
    public class PortfolioTest
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

        [Fact]
        public void CanRebalance_should_return_false_if_assets_cannot_be_rebalanced()
        {
            var portfolio = new Portfolio(1000);
            portfolio.UpdateMonthlyChange(Month.JANUARY, 10);
            portfolio.UpdateMonthlyChange(Month.FEBRUARY, 10);
            portfolio.UpdateMonthlyChange(Month.MARCH, 10);
            portfolio.UpdateMonthlyChange(Month.APRIL, 10);
            portfolio.UpdateMonthlyChange(Month.MAY, 10);

            Assert.False(portfolio.CanRebalance);
        }

        [Fact]
        public void CanRebalance_should_return_true_if_assets_can_be_rebalanced()
        {
            var portfolio = new Portfolio(1000);
            portfolio.UpdateMonthlyChange(Month.JANUARY, 10);
            portfolio.UpdateMonthlyChange(Month.FEBRUARY, 10);
            portfolio.UpdateMonthlyChange(Month.MARCH, 10);
            portfolio.UpdateMonthlyChange(Month.APRIL, 10);
            portfolio.UpdateMonthlyChange(Month.MAY, 10);
            portfolio.UpdateMonthlyChange(Month.JUNE, 10);

            Assert.True(portfolio.CanRebalance);
        }

        [Fact]
        public void Rebalance_should_throw_excpetion_if_cannot_be_rebalanced()
        {
            var portfolio = new Portfolio(1000);

            Assert.Throws<Exception>(() =>
            {
                portfolio.Rebalance();
            });
        }

        [Fact]
        public void Rebalance_should_return_rebalanced_value()
        {
            var portfolio = new Portfolio(1000, 500);
            portfolio.UpdateMonthlyChange(Month.JANUARY, 5, 10);
            portfolio.UpdateMonthlyChange(Month.FEBRUARY, 5, 10);
            portfolio.UpdateMonthlyChange(Month.MARCH, 5, 10);
            portfolio.UpdateMonthlyChange(Month.APRIL, 5, 10);
            portfolio.UpdateMonthlyChange(Month.MAY, 5, 10);
            portfolio.UpdateMonthlyChange(Month.JUNE, 5, 10);

            Assert.Equal(new int[] { 1337, 884 }, portfolio.ShowBalances(Month.JUNE));
            Assert.Equal(new int[] { 1480, 740 }, portfolio.Rebalance());
        }
    }
}
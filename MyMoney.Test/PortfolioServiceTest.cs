using System;
using MyMoney.models;
using Xunit;

namespace MyMoney.Test
{
    public class PortfolioServiceTest
    {
        [Fact]
        public void GetPortfolio_should_throw_exception_on_portfolio_not_initialized()
        {
            var portfolioService = new PortfolioService();
            Assert.Throws<Exception>(() =>
            {
                portfolioService.GetPortfolio();
            });
        }

        [Fact]
        public void GetPortfolio_should_return_initialized_portfolio()
        {
            var portfolio = new Portfolio(100);
            var portfolioService = new PortfolioService();
            portfolioService.InitializePortfolio(portfolio);
            Assert.Equal(portfolio, portfolioService.GetPortfolio());
        }
    }
}
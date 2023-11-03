using System;
using MyMoney.models;

namespace MyMoney
{
    public class PortfolioService : IPortfolioService
    {
        private Portfolio Instance { get; set; }

        public Portfolio GetPortfolio()
        {
            if (Instance == null)
            {
                throw new Exception("Portfolio is not allocated");
            }
            return Instance;
        }

        public Portfolio InitializePortfolio(Portfolio portfolio)
        {
            Instance = portfolio;
            return Instance;
        }
    }

    public interface IPortfolioService
    {
        Portfolio GetPortfolio();
        Portfolio InitializePortfolio(Portfolio portfolio);
    }
}
using System;
using MyMoney.models;

namespace MyMoney.handlers
{
    public class RebalanceCommandHandler : ICommandHandler
    {
        public string Execute(params string[] args)
        {
            if (Portfolio.Instance == null)
            {
                throw new Exception("Portfolio is not allocated");
            }
            var portfolio = Portfolio.Instance;
            if (!portfolio.CanRebalance)
                return "CANNOT_REBALANCE";
            var balances = portfolio.Rebalance();
            return string.Join(" ", balances);
        }
    }
}
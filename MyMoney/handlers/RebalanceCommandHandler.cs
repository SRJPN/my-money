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
            var balances = Portfolio.Instance.Rebalance();
            return string.Join(" ", balances);
        }
    }
}
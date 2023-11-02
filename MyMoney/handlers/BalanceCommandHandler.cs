using System;
using MyMoney.extensions;
using MyMoney.models;

namespace MyMoney.handlers
{
    public class BalanceCommandHandler : ICommandHandler
    {
        public string Execute(params string[] args)
        {
            if (Portfolio.Instance == null)
            {
                throw new Exception("Portfolio is not allocated");
            }
            var currentMonth = args[0].ToMonth();
            var balances = Portfolio.Instance.ShowBalances(currentMonth);
            return string.Join(" ", balances);
        }
    }
}
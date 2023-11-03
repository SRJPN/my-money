using MyMoney.extensions;

namespace MyMoney.handlers
{
    public class BalanceCommandHandler : ICommandHandler
    {
        private readonly IPortfolioService service;

        public BalanceCommandHandler(IPortfolioService service)
        {
            this.service = service;
        }

        public string Execute(params string[] args)
        {
            var currentMonth = args[0].ToMonth();
            var balances = service.GetPortfolio().ShowBalances(currentMonth);
            return string.Join(" ", balances);
        }
    }
}
namespace MyMoney.handlers
{
    public class RebalanceCommandHandler : ICommandHandler
    {
        private readonly IPortfolioService service;
        public RebalanceCommandHandler(IPortfolioService service)
        {
            this.service = service;
        }

        public string Execute(params string[] args)
        {
            var portfolio = service.GetPortfolio();
            if (!portfolio.CanRebalance)
                return "CANNOT_REBALANCE";
            var balances = portfolio.Rebalance();
            return string.Join(" ", balances);
        }
    }
}
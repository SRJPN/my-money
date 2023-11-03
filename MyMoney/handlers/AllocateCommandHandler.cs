using System.Linq;
using MyMoney.models;

namespace MyMoney.handlers
{
    public class AllocateCommandHandler : ICommandHandler
    {
        private readonly IPortfolioService service;
        public AllocateCommandHandler(IPortfolioService service)
        {
            this.service = service;
        }

        public string Execute(params string[] args)
        {
            var portfolio = new Portfolio(args.Select(int.Parse).ToArray());
            service.InitializePortfolio(portfolio);

            return null;
        }
    }
}
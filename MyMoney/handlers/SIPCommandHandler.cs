using System.Linq;

namespace MyMoney.handlers
{
    public class SIPCommandHandler : ICommandHandler
    {
        private readonly IPortfolioService service;

        public SIPCommandHandler(IPortfolioService service)
        {
            this.service = service;
        }

        public string Execute(params string[] args)
        {
            service.GetPortfolio().AddSips(args.Select(int.Parse).ToArray());
            return null;
        }
    }
}
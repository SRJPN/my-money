using System.Collections.Generic;
using System.Linq;
using MyMoney.extensions;
using MyMoney.models;

namespace MyMoney.handlers
{
    public class ChangeCommandHandler : ICommandHandler
    {
        private readonly IPortfolioService service;
        public ChangeCommandHandler(IPortfolioService service)
        {
            this.service = service;
        }

        public string Execute(params string[] args)
        {
            var monthlyChangePercentage = ExtractMonthlyChangePercentages(args).Select(ParsePercentage).ToArray();
            service.GetPortfolio().UpdateMonthlyChange(ExtractMonth(args), monthlyChangePercentage);
            return null;
        }

        private static IEnumerable<string> ExtractMonthlyChangePercentages(string[] args)
        {
            return args.Take(args.Length - 1);
        }

        private static Month ExtractMonth(string[] args)
        {
            return args[^1].ToMonth();
        }

        private static decimal ParsePercentage(string x)
        {
            return decimal.Parse(x.TrimEnd('%'));
        }
    }
}
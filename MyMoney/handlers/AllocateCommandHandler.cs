using System.Linq;
using MyMoney.models;

namespace MyMoney.handlers
{
    public class AllocateCommandHandler : ICommandHandler
    {
        public string Execute(params string[] args)
        {
            var portfolio = new Portfolio(args.Select(int.Parse).ToArray());
            Portfolio.Instance = portfolio;

            return null;
        }
    }
}
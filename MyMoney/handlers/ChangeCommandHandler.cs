using System;
using System.Linq;
using MyMoney.extensions;
using MyMoney.models;

namespace MyMoney.handlers
{
    public class ChangeCommandHandler : ICommandHandler
    {
        private const int ASSETS_COUNT = 3;
        private const int MONTH_PARAM_INDEX = 3;

        public string Execute(params string[] args)
        {
            if (Portfolio.Instance == null)
            {
                throw new Exception("Portfolio is not allocated");
            }
            var monthlyChangePercentage = args.Take(ASSETS_COUNT).Select(ParsePercentage).ToArray();
            Portfolio.Instance.UpdateMonthlyChange(args[MONTH_PARAM_INDEX].ToMonth(), monthlyChangePercentage);
            return null;
        }

        private static double ParsePercentage(string x)
        {
            return double.Parse(x.TrimEnd('%'));
        }
    }
}
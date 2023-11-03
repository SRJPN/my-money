using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using MyMoney;
using MyMoney.handlers;
using MyMoney.models;

namespace GeekTrust;

[ExcludeFromCodeCoverage]
class Program
{
    static readonly IPortfolioService service = new PortfolioService();
    static readonly IDictionary<string, ICommandHandler> handlers = new Dictionary<string, ICommandHandler> {
        {"ALLOCATE", new AllocateCommandHandler(service)},
        {"SIP", new SIPCommandHandler(service)},
        {"CHANGE", new ChangeCommandHandler(service)},
        {"BALANCE", new BalanceCommandHandler(service)},
        {"REBALANCE", new RebalanceCommandHandler(service)}
    };

    static void Main(string[] args)
    {
        var filePath = args[0];
        var response = new List<string>();
        var portfolio = new Portfolio();
        foreach (string line in File.ReadLines(filePath))
        {
            var arguments = new List<string>(line.Split(" "));
            string command = arguments[0];
            var handler = GetHandler(command);
            var result = handler.Execute(arguments.Skip(1).ToArray());
            if (result != null)
            {
                response.Add(result.ToString());
            }
        }

        response.ForEach(Console.WriteLine);
    }

    private static ICommandHandler GetHandler(string command)
    {
        if (!handlers.ContainsKey(command))
            throw new Exception("INVALID COMMAND PROVIDED");
        return handlers[command];
    }
}

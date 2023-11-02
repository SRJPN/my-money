using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MyMoney.handlers;
using MyMoney.models;

namespace GeekTrust
{
    class Program
    {
        static IDictionary<string, ICommandHandler> handlers = new Dictionary<string, ICommandHandler> {
            {"ALLOCATE", new AllocateCommandHandler()},
            {"SIP", new SIPCommandHandler()},
            {"CHANGE", new ChangeCommandHandler()},
            {"BALANCE", new BalanceCommandHandler()},
            {"REBALANCE", new RebalanceCommandHandler()}
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
            if(!handlers.ContainsKey(command))
                throw new Exception("INVALID COMMAND PROVIDED");
            return handlers[command];
        }
    }
}

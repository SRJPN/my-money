using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MyMoney.extensions;
using MyMoney.models;

namespace GeekTrust
{
    class Program
    {
        static void Main(string[] args)
        {
            // var serviceProvider = ConfigureServices();
            var filePath = args[0];
            var response = new List<string>();
            var portfolio =  new Portfolio();
            foreach (string line in File.ReadLines(filePath))
            {
                var arguments = new List<string>(line.Split(" "));
                string command = arguments[0];
                switch (command)
                {
                    case "ALLOCATE":
                        portfolio = new Portfolio(arguments.Skip(1).Select(int.Parse).ToArray());
                        break;
                    case "SIP":
                        portfolio.AddSips(arguments.Skip(1).Select(int.Parse).ToArray());
                        break;
                    case "CHANGE":
                        portfolio.UpdateMonthlyChange(arguments[4].ToMonth(), arguments.Skip(1).Take(3).Select(x => double.Parse(x.TrimEnd('%'))).ToArray());
                        break;
                    case "BALANCE":
                        response.Add(string.Join(" ", portfolio.ShowBalances(arguments[1].ToMonth())));
                        break;
                    default:
                        throw new Exception("INVALID COMMAND PROVIDED");
                }
            }

            response.ForEach(Console.WriteLine);
        }
    }
}

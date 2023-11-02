using System;
using System.Linq;
using MyMoney.models;

namespace MyMoney.handlers
{
    public class SIPCommandHandler : ICommandHandler
    {
        public string Execute(params string[] args)
        {
            if(Portfolio.Instance == null) {
                throw new Exception("Portfolio is not allocated");
            }
            Portfolio.Instance.AddSips(args.Select(int.Parse).ToArray());
            return null;
        }
    }
}
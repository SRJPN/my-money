using System;
using System.Collections.Generic;

namespace MyMoney.models;

public class Asset
{

    private readonly int initialAllocation = 0;

    private readonly IDictionary<Month, decimal> marketChange = new Dictionary<Month, decimal>();
    private readonly IDictionary<Month, int> monthlyBalance = new Dictionary<Month, int>();
    private int monthlySipAmount = 0;

    public Asset(int initialAllocation)
    {
        this.initialAllocation = initialAllocation;
        monthlyBalance.Add(Month.JANUARY, initialAllocation);
    }

    public int AllocatedAmount
    {
        get
        {
            return initialAllocation;
        }
    }

    public void AddMarketChange(Month month, decimal changeInPercentage)
    {
        marketChange.Add(month, changeInPercentage);
        if (monthlyBalance.ContainsKey(month))
            monthlyBalance[month] += (int)(monthlyBalance[month] * changeInPercentage / 100);
        else
        {
            var previousMonth = month - 1;
            var currentBalance = monthlyBalance[previousMonth] + monthlySipAmount;
            currentBalance += (int)(currentBalance * changeInPercentage / 100);
            monthlyBalance.Add(month, currentBalance);
        }
    }

    public void AddSip(int monthlySipAmount)
    {
        this.monthlySipAmount = monthlySipAmount;
    }

    public int ShowBalance(Month month)
    {
        if (!monthlyBalance.ContainsKey(month))
            return 0;
        return monthlyBalance[month];
    }

    public int Rebalance(decimal rebalancedAmount)
    {
        var currentMonth = (Month)(marketChange.Count - 1);
        var previousMonth = (Month)(marketChange.Count - 2);
        // Console.WriteLine($"{marketChange.Count} {(Month)(marketChange.Count - 1)}");
        var rebalancePercentage = (rebalancedAmount - ShowBalance(previousMonth)) / ShowBalance(previousMonth);

        marketChange[currentMonth] = rebalancePercentage * 100;

        Console.WriteLine($"{marketChange[currentMonth]} {ShowBalance(currentMonth)}");
        return ShowBalance(currentMonth);
    }

    public int CurrentBalance
    {
        get
        {
            var currentMonth = marketChange.Count - 1;
            return ShowBalance((Month)currentMonth);
        }
    }
}

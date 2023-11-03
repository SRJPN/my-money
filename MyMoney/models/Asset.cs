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
            monthlyBalance[month] += CalculateChangedAmount(changeInPercentage, monthlyBalance[month]);
        else
        {
            var previousMonth = month - 1;
            var currentBalance = monthlyBalance[previousMonth] + monthlySipAmount;
            currentBalance += CalculateChangedAmount(changeInPercentage, currentBalance);
            monthlyBalance.Add(month, currentBalance);
        }
    }

    private static int CalculateChangedAmount(decimal changeInPercentage, int currentBalance)
    {
        return (int)Math.Floor(currentBalance * changeInPercentage / 100);
    }

    public void AddSip(int monthlySipAmount)
    {
        this.monthlySipAmount = monthlySipAmount;
    }

    public int ShowBalance(Month month)
    {
        if (!monthlyBalance.ContainsKey(month))
            return CurrentBalance;
        return monthlyBalance[month];
    }

    public int Rebalance(int rebalancedAmount)
    {
        var currentMonth = (Month)(marketChange.Count - 1);
        if (!CanRebalance)
            return ShowBalance(currentMonth);
        monthlyBalance[currentMonth] = rebalancedAmount;
        return ShowBalance(currentMonth);
    }

    public int CurrentBalance
    {
        get
        {
            var currentMonth = monthlyBalance.Count - 1;
            return ShowBalance((Month)currentMonth);
        }
    }

    public bool CanRebalance => monthlyBalance.Count >= 6;
}

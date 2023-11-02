using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMoney.models;

public class Asset
{

    private readonly int initialAllocation = 0;

    private readonly IDictionary<Month, double> marketChange = new Dictionary<Month, double>();
    private int monthlySipAmount = 0;

    public Asset(int initialAllocation)
    {
        this.initialAllocation = initialAllocation;
    }

    public void AddMarketChange(Month month, double changeInPercentage)
    {
        marketChange.Add(month, changeInPercentage);
    }

    public void AddSip(int monthlySipAmount)
    {
        this.monthlySipAmount = monthlySipAmount;
    }

    public int ShowBalance(Month month)
    {
        var balance = 0;
        balance += initialAllocation;
        balance += (int)(balance * marketChange[Month.JANUARY] / 100);

        for (int i = 1; i <= (int)month; i++)
        {  
            var currentMonth = (Month)i;
            balance += monthlySipAmount;
            if(marketChange.ContainsKey(currentMonth))
            {
               balance += (int)(balance * marketChange[currentMonth] / 100);
            }
        }
        return balance;
    }
}

public enum Month
{
    JANUARY,
    FEBRUARY,
    MARCH,
    APRIL,
    MAY,
    JUNE,
    JULY,
    AUGUST,
    SEPTEMBER,
    OCTOBER,
    NOVEMBER,
    DECEMBER

}
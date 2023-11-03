using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMoney.models
{
    public class Portfolio
    {
        public static Portfolio Instance { get; set; } // Singleton instance 

        private readonly IList<Asset> assets;

        public Portfolio(params int[] assetAllocations)
        {
            assets = assetAllocations.Select(assetAllocation => new Asset(assetAllocation)).ToList();
        }

        public int[] ShowBalances(Month month) => assets.Select(asset => asset.ShowBalance(month)).ToArray();

        public void AddSips(params int[] sipAmounts)
        {
            for (int i = 0; i < sipAmounts.Length; i++)
            {
                assets[i].AddSip(sipAmounts[i]);
            }
        }

        public void UpdateMonthlyChange(Month month, params decimal[] marketChanges)
        {
            for (int i = 0; i < marketChanges.Length; i++)
            {
                assets[i].AddMarketChange(month, marketChanges[i]);
            }
        }

        public int[] Rebalance()
        {
            var totalAllocatedAmount = assets.Sum(x => x.AllocatedAmount);
            var totalBalance = assets.Sum(x => x.CurrentBalance);

            var balances = assets.Select(asset =>
            {
                decimal allocatedPercentage = (decimal)asset.AllocatedAmount / totalAllocatedAmount;

                var rebalancedAmount = Math.Floor(decimal.Round(allocatedPercentage * totalBalance, 2));
                // Console.WriteLine($"{allocatedPercentage * totalBalance} {allocatedPercentage} {totalBalance}");
                return asset.Rebalance((int)rebalancedAmount);
            });
            return balances.ToArray();
        }

        public bool CanRebalance => assets[0].CanRebalance;
    }
}
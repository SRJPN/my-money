using System.Collections.Generic;
using System.Linq;

namespace MyMoney.models
{
    public class Portfolio
    {
        private readonly List<Asset> assets;

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

        public void UpdateMonthlyChange(Month month, params double[] marketChanges)
        {
            for (int i = 0; i < marketChanges.Length; i++)
            {
                assets[i].AddMarketChange(month, marketChanges[i]);
            }
        }
    }
}
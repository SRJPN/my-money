using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using MyMoney.models;

namespace MyMoney.Test.models;

public class AssetTest
{
    [Fact]
    public void Ctor_should_create_asset_with_initial_allocation()
    {
        var asset = new Asset(1000);
        Assert.IsType<Asset>(asset);
    }

    [Fact]
    public void AddMarketChange_should_add_marketChange_in_percentage_for_given_month()
    {
        var asset = new Asset(1000);
        asset.AddMarketChange(Month.JANUARY, 4);
    }

    [Fact]
    public void AddSip_should_add_monthly_sip_amount_to_asset()
    {
        var asset = new Asset(1000);
        asset.AddSip(500);
    }

    [Fact]
    public void ShowBalance_should_return_balance_for_the_given_month()
    {
        var asset = new Asset(1000);

        Assert.Equal(1000, asset.ShowBalance(Month.JANUARY));
    }

    [Fact]
    public void ShowBalance_should_return_balance_for_the_given_month_by_calculating_market_change()
    {
        var asset = new Asset(1000);
        asset.AddMarketChange(Month.JANUARY, 4);

        Assert.Equal(1040, asset.ShowBalance(Month.JANUARY));
    }

    [Fact]
    public void ShowBalance_should_return_balance_for_the_given_month_adding_sip_from_second_month()
    {
        var asset = new Asset(1000);
        asset.AddSip(500);
        asset.AddMarketChange(Month.JANUARY, 4);
        asset.AddMarketChange(Month.FEBRUARY, 0);


        Assert.Equal(1540, asset.ShowBalance(Month.FEBRUARY));
    }

    [Fact]
    public void ShowBalance_should_return_balance_for_the_given_month_calculating_market_change_after_adding_sip()
    {
        var asset = new Asset(1000);
        asset.AddSip(500);
        asset.AddMarketChange(Month.JANUARY, 4);
        asset.AddMarketChange(Month.FEBRUARY, 5);


        Assert.Equal(1617, asset.ShowBalance(Month.FEBRUARY));
    }
}
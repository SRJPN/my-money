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
    public void AddMarketChange_should_floor_the_amount()
    {
        var asset = new Asset(4329);
        asset.AddMarketChange(Month.JANUARY, (decimal)-5.00);

        Assert.Equal(4112, asset.ShowBalance(Month.JANUARY));
    }

    [Fact]
    public void AddSip_should_add_monthly_sip_amount_to_asset()
    {
        var asset = new Asset(1000);
        asset.AddSip(500);
    }

    [Fact]
    public void ShowBalance_should_return_currentBalance_for_the_given_month_not_available()
    {
        var asset = new Asset(1000);

        Assert.Equal(1000, asset.ShowBalance(Month.FEBRUARY));
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

    [Fact]
    public void CurrentBalance_should_return_latest_balance_of_asset()
    {
        var asset = new Asset(1000);
        asset.AddSip(500);
        asset.AddMarketChange(Month.JANUARY, 4);
        asset.AddMarketChange(Month.FEBRUARY, 0);


        Assert.Equal(1540, asset.CurrentBalance);
    }

    [Fact]
    public void CanRebalance_return_false_if_six_months_of_monthlyChange_data_not_available()
    {
        var asset = new Asset(1000);
        asset.AddSip(500);
        asset.AddMarketChange(Month.JANUARY, 4);
        asset.AddMarketChange(Month.FEBRUARY, 0);

        Assert.False(asset.CanRebalance);
    }

    [Fact]
    public void CanRebalance_return_true_if_six_months_of_monthlyChange_data_available()
    {
        var asset = new Asset(1000);
        asset.AddSip(500);
        asset.AddMarketChange(Month.JANUARY, 4);
        asset.AddMarketChange(Month.FEBRUARY, 0);
        asset.AddMarketChange(Month.MARCH, 2);
        asset.AddMarketChange(Month.APRIL, 3);
        asset.AddMarketChange(Month.MAY, 7);
        asset.AddMarketChange(Month.JUNE, -2);


        Assert.True(asset.CanRebalance);
    }

    [Fact]
    public void Rebalance_ignores_rebalance_if_cannot_be_rebalances()
    {
        var asset = new Asset(1000);
        asset.AddSip(500);
        asset.AddMarketChange(Month.JANUARY, 4);
        asset.AddMarketChange(Month.FEBRUARY, 0);

        Assert.Equal(1540, asset.CurrentBalance);
        Assert.Equal(1540, asset.Rebalance(1000));
    }

    [Fact]
    public void Rebalance_updates_asset_to_given_balance()
    {
        var asset = new Asset(1000);
        asset.AddSip(500);
        asset.AddMarketChange(Month.JANUARY, 4);
        asset.AddMarketChange(Month.FEBRUARY, 0);
        asset.AddMarketChange(Month.MARCH, 2);
        asset.AddMarketChange(Month.APRIL, 3);
        asset.AddMarketChange(Month.MAY, 7);
        asset.AddMarketChange(Month.JUNE, -2);

        Assert.Equal(3799, asset.CurrentBalance);
        Assert.Equal(1500, asset.Rebalance(1500));
        Assert.Equal(1500, asset.CurrentBalance);
    }
}
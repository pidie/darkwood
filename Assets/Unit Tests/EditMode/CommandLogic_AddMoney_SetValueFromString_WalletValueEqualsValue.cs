using Inventory;
using NUnit.Framework;

public class CommandLogic_AddMoney_SetValueFromString_WalletValueEqualsValue
{
    [Test]
    public void AddSimplePasses()
    {
        var wallet = new PlayerWallet();
        wallet.ModifyValue(100);
        const string amount = "10";

        wallet.SetValue(int.Parse(amount));
        var result = wallet.value;

        Assert.AreEqual(10, result);
    }
}
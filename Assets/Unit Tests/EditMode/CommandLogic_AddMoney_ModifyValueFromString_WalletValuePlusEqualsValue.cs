using Inventory;
using NUnit.Framework;

public class CommandLogic_AddMoney_ModifyValueFromString_WalletValuePlusEqualsValue
{
    [Test]
    public void AddSimplePasses()
    {
        var wallet = new PlayerWallet();
        const string amount = "10";

        wallet.ModifyValue(int.Parse(amount));
        var result = wallet.value;

        Assert.AreEqual(10, result);
    }
}
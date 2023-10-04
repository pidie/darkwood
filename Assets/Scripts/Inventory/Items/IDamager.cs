namespace Inventory.Items
{
    public interface IDamager
    {
        float MinDamage { get; protected set; }
        float MaxDamage { get; protected set; }
    }
}
namespace Inventory.Items
{
    public interface IEquippable
    {
        string[] ValidSlots { get; protected set; }
    }
}
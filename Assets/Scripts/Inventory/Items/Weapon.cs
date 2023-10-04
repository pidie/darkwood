namespace Inventory.Items
{
    public class Weapon : InventoryObject, IEquippable, IDamager
    {
        string[] IEquippable.ValidSlots { get; set; }
        float IDamager.MinDamage { get; set; }
        float IDamager.MaxDamage { get; set; }
    }
}
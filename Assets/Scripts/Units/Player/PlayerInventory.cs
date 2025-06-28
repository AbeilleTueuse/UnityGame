using UnityEngine;

public class PlayerInventory : OwnerBehaviour
{
    [SerializeField]
    private InventoryConfig _config;

    [SerializeField]
    private Item _testItem;

    private InventorySystem _inventory;

    public override void OnStartClient()
    {
        base.OnStartClient();
        _inventory = new(_config.MaxSlots);
        _inventory.TryAddItem(_testItem, 1);
    }

    public InventorySystem Inventory => _inventory;
}

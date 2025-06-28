using FishNet.Connection;
using FishNet.Object;
using UnityEngine;

public class PlayerInventory : OwnerBehaviour
{
    [SerializeField]
    private InventoryConfig _config;

    private InventorySystem _inventory;

    public override void OnStartClient()
    {
        base.OnStartClient();
        _inventory = new(_config.MaxSlots);
    }

    [TargetRpc]
    public void TryPickupLoot(NetworkConnection conn, int itemID, int quantity, int objectId)
    {
        if (_inventory == null)
        {
            ConfirmLootServerRpc(objectId, quantity);
            return;
        }

        Item item = ItemManager.GetItem(itemID);
        int remainingQuantity = _inventory.AddItem(item, quantity);

        ConfirmLootServerRpc(objectId, remainingQuantity);
    }

    [ServerRpc]
    private void ConfirmLootServerRpc(int lootObjectId, int remainingQuantity)
    {
        if (ServerManager.Objects.Spawned.TryGetValue(lootObjectId, out var netObj))
        {
            if (netObj.TryGetComponent<LootableItem>(out var loot))
            {
                if (remainingQuantity == 0)
                    loot.OnLootConfirmed();
                else
                    loot.OnLootCancelled(remainingQuantity);
            }
        }
    }
}

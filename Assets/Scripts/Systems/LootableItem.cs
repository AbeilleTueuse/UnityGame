using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class LootableItem : NetworkBehaviour
{
    private readonly HashSet<GameObject> alreadyTriggered = new();

    [SerializeField]
    private Item _lootItem;

    [SerializeField]
    private int _lootQuantity = 1;

    private LootableItemState _state = LootableItemState.Available;

    private void OnTriggerEnter(Collider other)
    {
        if (!IsServerInitialized)
            return;

        if (!other.CompareTag(GameTags.Player))
            return;

        if (!alreadyTriggered.Add(other.gameObject))
            return;

        if (other.TryGetComponent<PlayerInventory>(out var playerInventory))
        {
            if (_state == LootableItemState.Available)
            {
                _state = LootableItemState.Pending;

                playerInventory.TryPickupLoot(
                    playerInventory.Owner,
                    _lootItem.Id,
                    _lootQuantity,
                    ObjectId
                );
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsServerInitialized)
            return;

        alreadyTriggered.Remove(other.gameObject);
    }

    [ServerRpc(RequireOwnership = false)]
    public void OnLootConfirmed()
    {
        _state = LootableItemState.Looted;
        _lootQuantity = 0;
        Despawn(DespawnType.Destroy);
    }

    [ServerRpc(RequireOwnership = false)]
    public void OnLootCancelled(int remaining)
    {
        _state = LootableItemState.Available;
        _lootQuantity = remaining;
    }
}

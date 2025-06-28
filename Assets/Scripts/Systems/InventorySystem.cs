using System;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem
{
    public event Action OnInventoryChanged;

    private readonly int _maxSlots;
    private readonly InventoryItemSlot[] _slots;

    public InventorySystem(int maxSlots)
    {
        _maxSlots = maxSlots;
        _slots = new InventoryItemSlot[_maxSlots];

        for (int i = 0; i < _maxSlots; i++)
        {
            _slots[i] = new InventoryItemSlot();
        }
    }

    public IReadOnlyList<InventoryItemSlot> Slots => _slots;

    public bool TryAddItem(Item item, int quantity = 1)
    {
        foreach (InventoryItemSlot slot in _slots)
        {
            if (slot.Item == item && slot.Quantity < item.MaxStack)
            {
                int spaceLeft = item.MaxStack - slot.Quantity;
                int toAdd = Mathf.Min(spaceLeft, quantity);
                slot.Quantity += toAdd;
                quantity -= toAdd;
                if (quantity == 0)
                {
                    OnInventoryChanged?.Invoke();
                    return true;
                }
            }
        }

        foreach (var slot in _slots)
        {
            if (slot.IsEmpty)
            {
                int toAdd = Mathf.Min(item.MaxStack, quantity);
                slot.Item = item;
                slot.Quantity = toAdd;
                quantity -= toAdd;
                if (quantity == 0)
                {
                    OnInventoryChanged?.Invoke();
                    return true;
                }
            }
        }

        OnInventoryChanged?.Invoke();
        return false;
    }

    // public void RemoveItem(int slotIndex, int quantity)
    // {
    //     if (slotIndex < 0 || slotIndex >= _slots.Count)
    //         return;
    //     var slot = _slots[slotIndex];

    //     slot.Quantity -= quantity;
    //     if (slot.Quantity <= 0)
    //     {
    //         slot.Clear();
    //     }

    //     OnInventoryChanged?.Invoke();
    // }
}

[Serializable]
public class InventoryItemSlot
{
    public Item Item;
    public int Quantity;

    public bool IsEmpty => Item == null || Quantity <= 0;

    public void Clear()
    {
        Item = null;
        Quantity = 0;
    }
}

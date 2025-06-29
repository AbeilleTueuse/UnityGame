using FishNet.Object;
using UnityEngine;

public class PlayerEquipmentManager : OwnerBehaviour
{
    [Header("Sockets")]
    public Transform RightHandSocket;
    public Transform LeftHandSocket;
    public Transform HelmetSocket;

    [Header("Current Equipment")]
    private GameObject _currentWeapon;
    private GameObject _currentHelmet;

    public void OnEnable()
    {
        ItemEvents.OnItemEquipped += Equip;
    }

    public void OnDisable()
    {
        ItemEvents.OnItemEquipped -= Equip;
    }

    public void Equip(Item item)
    {
        switch (item.SubCategory)
        {
            case ItemSubCategory.Weapon:
                EquipWeaponServerRpc(item.Id);
                break;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void EquipWeaponServerRpc(int itemId)
    {
        if (_currentWeapon != null)
            Despawn(_currentWeapon, DespawnType.Destroy);

        Item item = ItemManager.GetItem(itemId);

        if (item.WorldModelPrefab != null)
        {
            GameObject weapon = Instantiate(item.WorldModelPrefab, RightHandSocket);
            Spawn(weapon);
            SetWeaponVisualObserversRpc(weapon);
        }
    }

    [ObserversRpc]
    private void SetWeaponVisualObserversRpc(GameObject weapon)
    {
        weapon.transform.SetPositionAndRotation(RightHandSocket.position, RightHandSocket.rotation);
        weapon.transform.parent = RightHandSocket.transform;
    }
}

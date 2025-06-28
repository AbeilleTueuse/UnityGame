using UnityEngine.InputSystem;

public class InventoryManager : BaseMenuManager
{
    public void OnOpenInventory(InputValue value) => OpenMenu();

    public void OnCloseInventory(InputValue value) => CloseMenu();

    public void OnCancel(InputValue value) => CloseMenu();
}

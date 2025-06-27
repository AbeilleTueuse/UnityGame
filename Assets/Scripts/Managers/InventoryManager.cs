using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class InventoryManager : BaseMenuManager
{
    public void OnOpenInventory(InputValue value) => OpenMenu();

    public void OnCloseInventory(InputValue value) => CloseMenu();

    public void OnCancel(InputValue value) => CloseMenu();
}

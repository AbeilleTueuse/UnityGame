using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class GameMenuController : MonoBehaviour
{
    private PlayerInput _playerInput;
    public UIDocument GameMenuDocument;
    public UIDocument InventoryDocument;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        HideDocument(GameMenuDocument);
        HideDocument(InventoryDocument);
    }

    public void OnOpenMenu(InputValue value)
    {
        ShowDocument(GameMenuDocument);
        _playerInput.SwitchCurrentActionMap("UI");
    }

    public void OnCancel(InputValue value)
    {
        HideDocument(GameMenuDocument);
        HideDocument(InventoryDocument);
        _playerInput.SwitchCurrentActionMap("Player");
    }

    public void OnOpenInventory(InputValue value)
    {
        ShowDocument(InventoryDocument);
        _playerInput.SwitchCurrentActionMap("UI");
    }

    public void OnCloseInventory(InputValue value)
    {
        HideDocument(InventoryDocument);
        _playerInput.SwitchCurrentActionMap("Player");
    }

    private void ShowDocument(UIDocument document)
    {
        if (document != null)
        {
            document.rootVisualElement.style.display = DisplayStyle.Flex;
        }
    }

    private void HideDocument(UIDocument document)
    {
        if (document != null)
        {
            document.rootVisualElement.style.display = DisplayStyle.None;
        }
    }
}

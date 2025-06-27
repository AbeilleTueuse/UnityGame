using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public abstract class BaseMenuManager : OwnerBehaviour
{
    [SerializeField]
    private MenuName _menuName;
    protected PlayerInput _playerInput;
    public UIDocument MenuDocument;

    public override void OnStartClient()
    {
        base.OnStartClient();
        _playerInput = GetComponentInParent<PlayerInput>();
    }

    public void OpenMenu()
    {
        GameUIEvents.RequestOpen(_menuName);
        _playerInput.SwitchCurrentActionMap("UI");
    }

    public void CloseMenu()
    {
        GameUIEvents.RequestClose(_menuName);
        _playerInput.SwitchCurrentActionMap("Player");
    }
}

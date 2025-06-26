using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class GameMenuController : MonoBehaviour
{
    private PlayerInput _playerInput;
    public UIDocument GameMenuDocument;
    private VisualElement _root;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _root = GameMenuDocument.rootVisualElement;
        _root.style.display = DisplayStyle.None;
    }

    public void OnOpenMenu(InputValue value)
    {
        _root.style.display = DisplayStyle.Flex;
        _playerInput.SwitchCurrentActionMap("UI");
    }

    public void OnCancel(InputValue value)
    {
        _root.style.display = DisplayStyle.None;
        _playerInput.SwitchCurrentActionMap("Player");
    }
}

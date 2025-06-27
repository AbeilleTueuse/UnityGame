using UnityEngine;
using UnityEngine.UIElements;

public abstract class BaseMenuController : MonoBehaviour
{
    [SerializeField]
    private MenuName menuName;
    private UIDocument _uiDocument;

    protected virtual void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        Hide();
    }

    protected virtual void OnEnable()
    {
        GameUIEvents.OnOpenMenuRequested += HandleOpenMenu;
        GameUIEvents.OnCloseMenuRequested += HandleCloseMenu;
    }

    protected virtual void OnDisable()
    {
        GameUIEvents.OnOpenMenuRequested -= HandleOpenMenu;
        GameUIEvents.OnCloseMenuRequested -= HandleCloseMenu;
    }

    private void HandleOpenMenu(MenuName requestedMenu)
    {
        if (requestedMenu == menuName)
            Show();
    }

    private void HandleCloseMenu(MenuName requestedMenu)
    {
        if (requestedMenu == menuName)
            Hide();
    }

    protected void Show()
    {
        if (_uiDocument != null)
            _uiDocument.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    protected void Hide()
    {
        if (_uiDocument != null)
            _uiDocument.rootVisualElement.style.display = DisplayStyle.None;
    }
}

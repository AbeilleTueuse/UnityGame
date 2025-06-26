using UnityEngine;
using UnityEngine.UIElements;

public class HUDController : MonoBehaviour
{
    public UIDocument HUDDocument;
    private VisualElement _staminaBar;
    private PlayerStamina _playerStamina;

    private void Awake()
    {
        _staminaBar = HUDDocument.rootVisualElement.Q<VisualElement>(UIElementNames.StaminaBar);
        _playerStamina = GetComponent<PlayerStamina>();

        if (_playerStamina != null)
        {
            _playerStamina.OnStaminaUpdated += UpdateStaminaBar;
        }
    }

    private void UpdateStaminaBar(float stamina)
    {
        if (_staminaBar != null)
        {
            float staminaPercentage = stamina / _playerStamina.maxStamina;
            _staminaBar.style.width = new StyleLength(
                new Length(staminaPercentage * 100, LengthUnit.Percent)
            );
        }
    }
}

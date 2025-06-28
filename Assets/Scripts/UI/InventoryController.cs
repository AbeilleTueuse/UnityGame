using System.Data;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryController : BaseMenuController
{
    [SerializeField]
    private InventoryConfig _config;

    [SerializeField]
    private PlayerInventory _playerInventory;

    private InventorySystem _inventory;
    private VisualElement _inventoryGrid;

    public void Start()
    {
        _inventory = _playerInventory.Inventory;
        InitializeInventory();
        RefreshUI();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (_inventory != null)
            _inventory.OnInventoryChanged += RefreshUI;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if (_inventory != null)
            _inventory.OnInventoryChanged -= RefreshUI;
    }

    private void InitializeInventory()
    {
        _inventoryGrid = _uiDocument.rootVisualElement.Q<VisualElement>(
            UIElementNames.InventoryGrid
        );

        if (_inventoryGrid == null)
        {
            Debug.LogError("Inventory grid not found in the UI document.");
            return;
        }

        _inventoryGrid.Clear();
        _inventoryGrid.style.width = _config.columns * (_config.cellSize + _config.cellMargin * 2);

        for (int row = 0; row < _config.rows; row++)
        {
            for (int column = 0; column < _config.columns; column++)
            {
                Button inventoryCell = CreateInventoryCell(row, column);
                _inventoryGrid.Add(inventoryCell);
            }
        }
    }

    private Button CreateInventoryCell(int row, int column)
    {
        Button inventoryCell = new() { name = $"InventoryCell_{row}_{column}" };
        inventoryCell.AddToClassList(UIStyleClasses.InventoryCell);
        inventoryCell.style.width = _config.cellSize;
        inventoryCell.style.height = _config.cellSize;
        inventoryCell.style.marginLeft = _config.cellMargin;
        inventoryCell.style.marginTop = _config.cellMargin;
        inventoryCell.style.marginRight = _config.cellMargin;
        inventoryCell.style.marginBottom = _config.cellMargin;

        VisualElement icon = new() { name = "Icon" };
        icon.AddToClassList(UIStyleClasses.InventoryIcon);
        inventoryCell.Add(icon);

        Label quantityLabel = new() { name = "Quantity" };
        quantityLabel.AddToClassList(UIStyleClasses.InventoryQuantity);
        inventoryCell.Add(quantityLabel);

        return inventoryCell;
    }

    private void RefreshUI()
    {
        var slots = _inventory.Slots;

        for (int i = 0; i < _config.MaxSlots; i++)
        {
            if (_inventoryGrid[i] is not Button cell)
                continue;

            VisualElement icon = cell.Q<VisualElement>("Icon");
            Label quantityLabel = cell.Q<Label>("Quantity");

            if (i < slots.Count && !slots[i].IsEmpty)
            {
                Item item = slots[i].Item;

                if (item != null && item.Icon != null)
                {
                    icon.style.backgroundImage = new StyleBackground(item.Icon.texture);
                    icon.style.display = DisplayStyle.Flex;
                }

                quantityLabel.text = slots[i].Quantity > 1 ? slots[i].Quantity.ToString() : "";
                quantityLabel.style.display = DisplayStyle.Flex;
            }
            else
            {
                icon.style.backgroundImage = null;
                icon.style.display = DisplayStyle.None;
                quantityLabel.text = "";
                quantityLabel.style.display = DisplayStyle.None;
            }
        }
    }
}

using System.Data;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryController : BaseMenuController
{
    [SerializeField]
    private int _rows = 5;

    [SerializeField]
    private int _columns = 10;

    [SerializeField]
    private int _cellSize = 100;

    [SerializeField]
    private int _cellMargin = 10;

    public void Start()
    {
        InitializeInventory();
    }

    private void InitializeInventory()
    {
        VisualElement inventoryGrid = _uiDocument.rootVisualElement.Q<VisualElement>(
            UIElementNames.InventoryGrid
        );

        inventoryGrid.Clear();
        inventoryGrid.style.width = _columns * (_cellSize + _cellMargin * 2);
        for (int row = 0; row < _rows; row++)
        {
            for (int column = 0; column < _columns; column++)
            {
                Button inventoryCell = CreateInventoryCell(row, column);
                inventoryGrid.Add(inventoryCell);
            }
        }
    }

    private Button CreateInventoryCell(int row, int column)
    {
        Button inventoryCell = new() { name = $"InventoryCell_{row}_{column}" };
        inventoryCell.AddToClassList(UIStyleClasses.InventoryCell);
        inventoryCell.style.width = _cellSize;
        inventoryCell.style.height = _cellSize;
        inventoryCell.style.marginLeft = _cellMargin;
        inventoryCell.style.marginTop = _cellMargin;
        inventoryCell.style.marginRight = _cellMargin;
        inventoryCell.style.marginBottom = _cellMargin;

        return inventoryCell;
    }
}

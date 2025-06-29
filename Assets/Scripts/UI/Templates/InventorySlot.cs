using UnityEngine;
using UnityEngine.UIElements;

public class VisualSlot : VisualElement
{
    public Image Icon;
    public Label Quantity;
    public string ItemGuid = "";

    public VisualSlot(int size, int margin)
    {
        Icon = new Image();
        Icon.AddToClassList(UIStyleClasses.SlotIcon);

        Add(Icon);

        Quantity = new Label();
        Quantity.AddToClassList(UIStyleClasses.SlotQuantity);

        Icon.Add(Quantity);

        AddToClassList(UIStyleClasses.InventorySlot);
        AddInlineStyle(size, margin);
    }

    private void AddInlineStyle(int size, int margin)
    {
        style.width = size;
        style.height = size;
        style.marginLeft = margin;
        style.marginTop = margin;
        style.marginRight = margin;
        style.marginBottom = margin;
    }

    public void ToEmpty()
    {
        ItemGuid = "";
        Icon.image = null;
        Quantity.text = "";
        Quantity.style.display = DisplayStyle.None;
    }

    public void SetItem(Texture2D icon, int quantity, int itemMaxStack)
    {
        Icon.image = icon;

        if (itemMaxStack >= 2)
        {
            Quantity.text = quantity.ToString();
            Quantity.style.display = DisplayStyle.Flex;
        }
        else
        {
            Quantity.text = "";
            Quantity.style.display = DisplayStyle.None;
        }
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item", order = 1)]
public class Item : ScriptableObject
{
    [Header("Identification")]
    public int ID;
    public string Name;

    [TextArea]
    public string Description;

    [Header("UI")]
    public Sprite Icon;

    [Header("Gameplay")]
    public ItemType ItemType;
    public int MaxStack = 1;
}

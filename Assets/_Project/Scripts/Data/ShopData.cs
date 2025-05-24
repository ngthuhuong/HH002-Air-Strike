using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopData", menuName = "Shop/ShopData")]
public class ShopData : ScriptableObject
{
    public List<ShopItemData> items; // List of shop item data
}

[System.Serializable]
public class ShopItemData
{
    public string itemName;
    public Sprite itemIcon;
    public int price;
    public bool isPurchased;
}
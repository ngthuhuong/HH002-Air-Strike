using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private Image itemIconImage;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button purchaseButton;

    private ShopItemData itemData;

    public void Initialize(ShopItemData data)
    {
        itemData = data;
        itemNameText.text = data.itemName;
        itemIconImage.sprite = data.itemIcon;
        priceText.text = data.isPurchased ? "Purchased" : $"{data.price} Coins";
        purchaseButton.interactable = !data.isPurchased;

        purchaseButton.onClick.AddListener(OnPurchase);
    }

    private void OnPurchase()
    {
        if (itemData.isPurchased)
            return;

        int currentCoins = DataManager.Instance.CurrentCoin;
        if (currentCoins >= itemData.price)
        {
            // Deduct coins and mark as purchased
            DataManager.Instance.CurrentCoin -= itemData.price;
            itemData.isPurchased = true;
            priceText.text = "Purchased";
            purchaseButton.interactable = false;

            Debug.Log($"Purchased {itemData.itemName}");
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }
}
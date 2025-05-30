using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIShop : GUIBase
{
    [Header("Shop Settings")]
    [SerializeField] private ShopData shopData; // ScriptableObject containing shop items
    [SerializeField] private RectTransform scrollViewContent; 
    [SerializeField] private GameObject shopItemPrefab; // Prefab for each shop item


    [Header("Stats")] 
    private float spacing = 30;
    private List<ShopItem> shopItems = new List<ShopItem>();
    
    [Header("Buttons")]
    [SerializeField] private Button closeButton;

    #region MonoBehaviour

    private void Start()
    {
        //scrollViewContent.sizeDelta = new Vector2(1000f, 300f);
        if (shopData == null)
            return;
        PopulateShop();
    }

    private void OnEnable()
    {
        closeButton.onClick.AddListener(OnClickClose);
        Time.timeScale = 0f;
        
    }

    

    private void OnDisable()
    {
        closeButton.onClick.RemoveAllListeners();
        
    }

    #endregion

    #region Button Events

    private void OnClickClose()
    {
        Time.timeScale = 1f;
        Hide();
    }

    #endregion

    private void PopulateShop()
    {
        // Clear existing items
        foreach (Transform child in scrollViewContent)
        {
            Destroy(child.gameObject);
        }
        shopItems.Clear();

        // Instantiate shop items
        foreach (var itemData in shopData.items)
        {
            GameObject itemObject = Instantiate(shopItemPrefab, scrollViewContent);
            ShopItem shopItem = itemObject.GetComponent<ShopItem>();
            shopItem.Initialize(itemData);
            shopItems.Add(shopItem);
        }

       Debug.Log($"content size: {new Vector2(shopItems.Count * (spacing + shopItemPrefab.GetComponent<RectTransform>().sizeDelta.x * 1.5f), shopItemPrefab.GetComponent<RectTransform>().sizeDelta.y * 1.5f)}");
        scrollViewContent.sizeDelta = new Vector2(shopItems.Count * (spacing + shopItemPrefab.GetComponent<RectTransform>().sizeDelta.x * 1.5f), shopItemPrefab.GetComponent<RectTransform>().sizeDelta.y * 1.5f);

    }
}
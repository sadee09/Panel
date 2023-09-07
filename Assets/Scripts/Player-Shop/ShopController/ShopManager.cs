using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class ShopManager : MonoBehaviour
{
    public CoinManager coinManager;
    public List<ItemData> itemDatas; // List of item data

    public GameObject panel;
    public RectTransform panelRect;
    public Vector2 targetPosition;
    private Vector2 _originalPosition;
    public UnityEvent onClose;
    public UnityEvent onOpen;

    public event Action<ItemData> ItemPurchased; // Define a purchase event

    public void Awake()
    {
        _originalPosition = panelRect.anchoredPosition;
    }

    private void Start()
    {
        SaveLoadManager.LoadPurchasedItems(itemDatas); // Load purchased items when the scene starts
    }

    public void PurchaseItem()
    {
        //Item is found through the buy button that is clicked during the purchase of the product
        Button clickedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        ItemData itemData = itemDatas.Find(i => i.buy == clickedButton);
        
        //Item can also be find as name through
        //ItemData itemData = itemDatas.Find(i => i.name == name); 

        if (itemData != null)
        {
            // Price is given as Rs.1000 which includes both numeric and string
            // Extract the numeric part from the item.price.text
            string priceText = itemData.price.text;
            string numericPart = new string(priceText.Where(char.IsDigit).ToArray());

            int itemPrice;
            //parsing the numericPart to integer to compare with coins
            if (int.TryParse(numericPart, out itemPrice))
            {
                Debug.Log("Item Price: " + itemPrice);
                if (coinManager.coins >= itemPrice)
                {
                    coinManager.SubtractCoins(itemPrice);
                    itemData.buy.gameObject.SetActive(false);
                    itemData.bought.SetActive(true);
                    Debug.Log("Purchased item with price: " + itemPrice);

                    ItemPurchased?.Invoke(itemData);

                    // Save the purchased item
                    SaveLoadManager.SavePurchasedItem(itemData);
                }
                else
                {
                    if (panel != null)
                    {
                        panelRect.DOAnchorPos(targetPosition, 0.5f).OnComplete(() =>
                        {
                            // Invoke the onOpen event, triggering any attached actions
                            onOpen?.Invoke();
                        });
                    }
                }
            }
        }
    }

    public void ClosePanel()
    {
        if (panel != null)
        {
            panelRect.DOAnchorPos(_originalPosition, 0.5f).OnComplete(() =>
            {
                // Invoke the onOpen event, triggering any attached actions
                onClose?.Invoke();
            });
        }
    }

    [Serializable]
    public class ItemData
    {
        public TMP_Text price;
        public Button buy;
        public GameObject bought;
        public string name; // Unique identifier for each item
    }

    //Only using PlayerPref
    // private void SavePurchasedItem(ItemData itemData)
    // {
    //     // Save the purchased item's information using PlayerPrefs
    //     PlayerPrefs.SetInt("Purchased_" + itemData.name, 1);
    //     PlayerPrefs.Save();
    // }
    //
    // private void LoadPurchasedItems()
    // {
    //     foreach (var itemData in itemDatas)
    //     {
    //         // Load the purchased status of the item
    //         int purchased = PlayerPrefs.GetInt("Purchased_" + itemData.name, 0); // 0 indicates not purchased by default
    //
    //         // Set the button state based on the loaded status
    //         if (purchased == 1)
    //         {
    //             itemData.buy.gameObject.SetActive(false);
    //             itemData.bought.SetActive(true);
    //         }
    //         else
    //         {
    //             itemData.buy.gameObject.SetActive(true);
    //             itemData.bought.SetActive(false);
    //         }
    //     }
    // }
}



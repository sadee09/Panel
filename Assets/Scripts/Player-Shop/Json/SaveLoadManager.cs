using UnityEngine;
using System;
using System.Collections.Generic;

public class SaveLoadManager : MonoBehaviour
{
    [Serializable]
    private class SaveItemData
    {
        public string name;
        public bool purchased;
    }

    [Serializable]
    private class SavedItemDataList
    {
        public List<SaveItemData> items;
    }

    public static void SavePurchasedItem(ShopManager.ItemData itemData)
    {
        //A serializable item data object
        SaveItemData saveItemData = new SaveItemData
        {
            name = itemData.name,
            purchased = true
        };

        // Loading existing data if it exists so that it wont overwrite previously saved data
        string jsonData = PlayerPrefs.GetString("PurchasedItems", "");

        // Deserialize existing data
        SavedItemDataList itemList = new SavedItemDataList();
        if (!string.IsNullOrEmpty(jsonData))
        {
            itemList = JsonUtility.FromJson<SavedItemDataList>(jsonData);
        }

        // Adding the new item data to the list
        if (itemList.items == null)
        {
            itemList.items = new List<SaveItemData>();
        }

        itemList.items.Add(saveItemData);

        // Serialize the updated data back to JSON
        string updatedJsonData = JsonUtility.ToJson(itemList);

        // Save the updated data
        PlayerPrefs.SetString("PurchasedItems", updatedJsonData);
        PlayerPrefs.Save();
    }

    public static void LoadPurchasedItems(List<ShopManager.ItemData> itemDatas)
    {
        // Loading saved item data from PlayerPrefs
        string jsonData = PlayerPrefs.GetString("PurchasedItems", "");

        // Deserialize the JSON data into a list of serializable item data
        SavedItemDataList itemList = JsonUtility.FromJson<SavedItemDataList>(jsonData);

        if (itemList != null && itemList.items != null)
        {
            // Iterating through the deserialized data and update the UI
            foreach (var itemData in itemDatas)
            {
                SaveItemData saveItemData = itemList.items.Find(i => i.name == itemData.name);

                if (saveItemData != null && saveItemData.purchased)
                {
                    itemData.buy.gameObject.SetActive(false);
                    itemData.bought.SetActive(true);
                }
                else
                {
                    itemData.buy.gameObject.SetActive(true);
                    itemData.bought.SetActive(false);
                }
            }
        }
    }
}
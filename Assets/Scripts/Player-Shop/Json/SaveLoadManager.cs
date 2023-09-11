using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class SaveLoadManager 
{
    [Serializable]
    public class SaveItemData
    {
        public string name;
        public bool purchased;
    }

    [Serializable]
    public class SavedItemDataList
    {
        public List<SaveItemData> items;
        
    }

    private static class ItemListManager
    {
        public static void AddItem(SavedItemDataList itemList, SaveItemData item)
        {
            itemList.items.Add(item);
        }
    }

   
    public static void SavePurchasedItem(Item itemData)
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
        
        ItemListManager.AddItem(itemList, saveItemData);
 
        // Serialize the updated data back to JSON
        string updatedJsonData = JsonUtility.ToJson(itemList);

        // Save the updated data
        PlayerPrefs.SetString("PurchasedItems", updatedJsonData);
        PlayerPrefs.Save();
    }
    

    public static void LoadPurchasedItems(ItemView itemView)
    {
        // Loading saved item data from PlayerPrefs
        string jsonData = PlayerPrefs.GetString("PurchasedItems", "");
        
        // Deserialize the JSON data into a list of serializable item data
        SavedItemDataList itemList = JsonUtility.FromJson<SavedItemDataList>(jsonData);

        if (itemList != null && itemList.items != null)
        {
            // Iterating through the deserialized data and update the UI
            // As itemView is taken we don't need to provide the foreach loop as it checks the data while being instantiated.
            SaveItemData saveItemData = itemList.items.Find(i => i.name == itemView.ItemName);

            if (saveItemData != null && saveItemData.purchased)
            {
                itemView.ChangeBuyButton();
            }
            else
            {
                itemView.ShowBuyButton();
            }
        }
    }
}
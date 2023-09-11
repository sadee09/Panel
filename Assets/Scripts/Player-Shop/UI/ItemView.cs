using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text price;
    [SerializeField] public  Button buy;
    
    public Item itemData;
 
    public void SetData(Item itemData)
    {
        this.itemData = itemData;
        itemImage.sprite = itemData.itemImage;
        price.text = itemData.price;
    }
    public void ChangeBuyButton()
    {
        buy.GetComponentInChildren<TMP_Text>().text = "Bought";
        buy.interactable = false;
        RectTransform buttonRect = buy.GetComponent<RectTransform>();
        buttonRect.sizeDelta = new Vector2(70f, buttonRect.sizeDelta.y);
    }

    public void ShowBuyButton()
    {
        buy.GetComponentInChildren<TMP_Text>().text = "Buy";
        buy.interactable = true;
    }
    
}
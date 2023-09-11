using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
public class ShopOpener : MonoBehaviour
{
    public GameObject panel;
    public UnityEvent onOpen;
    public UnityEvent onClose;
    
    public Vector2 targetPosition;
    private Vector2 originalPosition;
    public RectTransform panelRect;
    
    public static List<ItemView> items = new List<ItemView>();
    public float fadeTime;

    public void Awake()
    {
        originalPosition = panelRect.anchoredPosition;
    }

    public static void AddItemsToAnimation(ItemView itemToAdd)
    {
        items.Add(itemToAdd);
    }
    public void OpenPanel()
    {
        if (panel != null)
        {
            ShopPanel.Instance.CloseCurrentPanel();

            panelRect.DOAnchorPos(targetPosition, 0.5f).OnComplete(() =>
            {
                // Invoke the onOpen event, triggering any attached actions
                onOpen?.Invoke();
            });
            
            // Set the CurrentPanel property of the PanelManager to this PanelOpener script
            ShopPanel.Instance.CurrentPanel = this;
            StartCoroutine("ItemsAnimation");
        }
    }



    public void ClosePanel()
    {
        if (panel != null)
        {
            panelRect.DOAnchorPos(originalPosition, 0.5f).OnComplete(() =>
            {
                // Invoke the onOpen event, triggering any attached actions
                onClose?.Invoke();
            });

            if (ShopPanel.Instance.CurrentPanel == this)
            {
                ShopPanel.Instance.CurrentPanel = null;
            }
        }
    }
    IEnumerator ItemsAnimation()
    {
        foreach (var item in items)
        {
            item.transform.localScale = Vector3.zero;
        }

        
        foreach (var item in items)
        {
            item.transform.DOScale(1f, fadeTime).SetEase(Ease.OutBounce);
            yield return new WaitForSeconds(0.25f);
        }
        
    }
}

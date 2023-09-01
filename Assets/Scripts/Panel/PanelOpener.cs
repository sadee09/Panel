using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class PanelOpener : MonoBehaviour
{
    public GameObject panel;
    public UnityEvent onOpen;
    public UnityEvent onClose;
    
    public Vector2 targetPosition;
    private Vector2 originalPosition;
    public RectTransform panelRect;

    public void Awake()
    {
        originalPosition = panelRect.anchoredPosition;
    }

    public void OpenPanel()
    {
        if (panel != null)
        {
            PanelManager.Instance.CloseCurrentPanel();

            panelRect.DOAnchorPos(targetPosition, 0.5f).OnComplete(() =>
            {
                // Invoke the onOpen event, triggering any attached actions
                onOpen?.Invoke();
            });
            
            // Set the CurrentPanel property of the PanelManager to this PanelOpener script
            PanelManager.Instance.CurrentPanel = this;
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

            if (PanelManager.Instance.CurrentPanel == this)
            {
                PanelManager.Instance.CurrentPanel = null;
            }
        }
    }
}
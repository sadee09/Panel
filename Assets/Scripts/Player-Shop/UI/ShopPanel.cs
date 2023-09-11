using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    private ShopOpener currentPanel;

    private static ShopPanel instance;
    public static ShopPanel Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ShopPanel>();

            }

            return (instance);
        }   
    }

    public ShopOpener CurrentPanel
    {
        get { return currentPanel; }
        set { currentPanel = value; }
    }

    public void CloseCurrentPanel()
    {
        if (currentPanel != null)
        {
            currentPanel.ClosePanel();
        }
    }
}

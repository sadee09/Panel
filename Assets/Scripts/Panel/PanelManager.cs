using UnityEngine;

public class PanelManager : MonoBehaviour
{
    private PanelOpener currentPanel;

    private static PanelManager instance;
    public static PanelManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PanelManager>();
            }

            return (instance);
        }
    }

    public PanelOpener CurrentPanel
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
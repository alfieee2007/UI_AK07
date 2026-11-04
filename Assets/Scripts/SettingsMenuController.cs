
using UnityEngine;

public class SettingsMenuController : MonoBehaviour
{
    public GameObject graphicsPanel;
    public GameObject audioPanel;
    public GameObject controlsPanel;
    public GameObject accessibilityPanel;

    public void ShowGraphics()
    {
        ShowPanel(graphicsPanel);
    }

    public void ShowAudio()
    {
        ShowPanel(audioPanel);
    }

    public void ShowControls()
    {
        ShowPanel(controlsPanel);
    }

    public void ShowAccessibility()
    {
        ShowPanel(accessibilityPanel);
    }

    private void ShowPanel(GameObject selectedPanel)
    {
        graphicsPanel.SetActive(selectedPanel == graphicsPanel);
        audioPanel.SetActive(selectedPanel == audioPanel);
        controlsPanel.SetActive(selectedPanel == controlsPanel);
        accessibilityPanel.SetActive(selectedPanel == accessibilityPanel);
    }
}

using UnityEngine;

public class ChatPanelManager : MonoBehaviour
{

    public GameObject chatPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        chatPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleChatPanel();
        }
    }

    public void ToggleChatPanel()
    {
        chatPanel.SetActive(!chatPanel.activeSelf);
    }
    public void CloseChatPanel()
    {
        chatPanel.SetActive(false);
    }
}

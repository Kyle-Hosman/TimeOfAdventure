using UnityEngine;
//using EasySave3;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject chatPanel;
    public SaveLoadManager saveLoadManager; // Add reference to SaveLoadManager
    public GameObject menuCanvas; // Add reference to MenuCanvas

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (saveLoadManager == null)
        {
            saveLoadManager = FindFirstObjectByType<SaveLoadManager>();
            if (saveLoadManager == null)
            {
                Debug.LogError("SaveLoadManager not found in the scene.");
            }
        }
    }

    private void Start()
    {
        LoadGame(); // Load game on start
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    private void OnApplicationQuit()
    {
        SaveGame(); // Save game on application quit
    }

    public void ToggleMenu()
    {
        if (menuCanvas != null)
        {
            menuCanvas.SetActive(!menuCanvas.activeSelf);
        }
        else
        {
            Debug.LogError("MenuCanvas is not assigned.");
        }
    }

    public void SaveGame()
    {
        if (saveLoadManager != null)
        {
            saveLoadManager.SaveGame();
        }
        else
        {
            Debug.LogError("SaveLoadManager is not assigned.");
        }
    }

    public void LoadGame()
    {
        if (saveLoadManager != null)
        {
            saveLoadManager.LoadGame();
        }
        else
        {
            Debug.LogError("SaveLoadManager is not assigned.");
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string managersSceneName = "Managers";
    [SerializeField] private string uiSceneName = "UI";
    [SerializeField] private string playerSceneName = "Player";
    [SerializeField] private string environmentSceneName = "Environment1"; // Set this dynamically if needed

    private void Start()
    {
        StartCoroutine(LoadScenesSequentially());
    }

    private IEnumerator LoadScenesSequentially()
    {
        // Load Managers scene
        yield return LoadSceneAsync(managersSceneName);

        // Load UI scene
        yield return LoadSceneAsync(uiSceneName);

        // Load Player scene
        yield return LoadSceneAsync(playerSceneName);

        // Finally, load the environment scene
        yield return LoadSceneAsync(environmentSceneName);
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            asyncLoad.allowSceneActivation = true;

            while (!asyncLoad.isDone)
            {
                yield return null; // Wait until the scene is fully loaded
            }
        }
    }

    public void TogglePlayerCamera(bool isActive)
    {
        var playerCamera = GameObject.FindWithTag("MainCamera");
        if (playerCamera != null)
        {
            playerCamera.SetActive(isActive);
        }
    }

    // Example usage: Call this method when switching between environment-specific and player-centric cameras.
}

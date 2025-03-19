using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ES3Internal; // Ensure Easy Save 3 is imported

public class SaveLoadManager : MonoBehaviour
{
    public Transform playerTransform; // Add reference to player's Transform

    public void SaveGame()
    {
        // Implement your save logic using Easy Save 3
        if (playerTransform != null)
        {
            ES3.Save("playerPosition", playerTransform.position);
            Debug.Log("Game Saved");
        }
        else
        {
            Debug.LogError("Player Transform is not assigned.");
        }
    }

    public void LoadGame()
    {
        // Implement your load logic using Easy Save 3
        if (playerTransform != null)
        {
            if (ES3.KeyExists("playerPosition"))
            {
                playerTransform.position = ES3.Load<Vector3>("playerPosition");
                Debug.Log("Game Loaded");
            }
            else
            {
                Debug.LogError("No save data found");
            }
        }
        else
        {
            Debug.LogError("Player Transform is not assigned.");
        }
    }
}

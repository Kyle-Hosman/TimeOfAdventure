using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public InputEvents inputEvents;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Spacebar pressed"); // Log spacebar press
            inputEvents.SubmitPressed();
        }
    }
}

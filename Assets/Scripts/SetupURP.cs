using UnityEngine;

public class SetupURP : MonoBehaviour
{
    void Start()
    {
        // Step 1: Install the Universal Render Pipeline (URP) via the Package Manager.

        // Step 2: Create and configure the URP Asset.
        // Right-click in the Project window and select:
        // Create > Rendering > Universal Render Pipeline > Pipeline Asset (Forward Renderer).
        // Assign this asset in the Graphics settings:
        // Edit > Project Settings > Graphics.

        // Step 3: Set up the 2D Renderer.
        // Right-click in the Project window and select:
        // Create > Rendering > Universal Render Pipeline > 2D Renderer.
        // Assign the 2D Renderer to the URP asset created in Step 2.

        // Step 4: Convert your scene to use URP.
        // Select your materials and convert them to use the URP shader:
        // Right-click on the material and select:
        // Convert Selected Materials to URP.

        // Step 5: Add 2D lights to your scene.
        // In the Hierarchy window, right-click and select:
        // Light > 2D > [Type of Light] (e.g., Point Light, Global Light).
        // Configure the light properties in the Inspector.

        // Step 6: Configure the Pixel Perfect Camera.
        // Add the Pixel Perfect Camera component to your main camera.
        // Configure the Pixel Perfect Camera settings to match your pixel art style.
    }
}

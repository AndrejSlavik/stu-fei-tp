using UnityEngine;
using TMPro;

public class UpdateTextOnMoveY : MonoBehaviour
{
    public Transform movingSpriteTransform;
    public TextMeshProUGUI textMeshPro;

    private void Update()
    {
        // Get the Y position of the moving sprite
        float newY = movingSpriteTransform.position.y;

        // Update the TextMeshPro text with the new X position
        textMeshPro.text = "Y:" + newY.ToString("F2"); // Format to two decimal places, adjust as needed
    }
}
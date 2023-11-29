using UnityEngine;
using TMPro;

public class UpdateTextOnMoveX : MonoBehaviour
{
    public Transform movingSpriteTransform;
    public TextMeshProUGUI textMeshPro;

    private void Update()
    {
        // Get the X position of the moving sprite
        float newX = movingSpriteTransform.position.x;

        // Update the TextMeshPro text with the new Y position
        textMeshPro.text = "X:" + newX.ToString("F2"); // Format to two decimal places, adjust as needed
    }
}
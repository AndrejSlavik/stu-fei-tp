using UnityEngine;

public class JoystickPosition : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the speed as needed

    private void Update()
    {
        // Get input from arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement direction
        Vector2 movement = new Vector2(horizontalInput, verticalInput);

        // Normalize the movement vector to ensure consistent speed in all directions
        movement.Normalize();

        // Update the position of the sprite based on input and speed
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}


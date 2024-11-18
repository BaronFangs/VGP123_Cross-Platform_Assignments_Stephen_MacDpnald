using UnityEngine;

public class Pickups : MonoBehaviour
{
    // Define types of pickups
    public enum PickupType
    {
        Life,
        JumpBoost,
        Shrink,
        Score
    }

    public PickupType type; // Set this in the Inspector

    // Trigger detection when the player collides with this pickup
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Check if the colliding object is the player
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                ApplyPickupEffect(playerController); // Apply effect based on type
            }

            Destroy(gameObject); // Destroy the pickup after collection
        }
    }

    // Apply the pickup effect based on the type
    private void ApplyPickupEffect(PlayerController player)
    {
        switch (type)
        {
            case PickupType.Life:
                player.lives++; // Increase player's lives
                Debug.Log("Picked up a Life! Lives: " + player.lives);
                break;

            case PickupType.JumpBoost:
                player.JumpPowerUp(); // Trigger the jump boost power-up
                Debug.Log("Picked up a Jump Boost!");
                break;

            case PickupType.Shrink:
                // Implement shrink effect here if applicable
                Debug.Log("Picked up Shrink (implement effect here)");
                break;

            case PickupType.Score:
                player.score++; // Increase player's score
                Debug.Log("Picked up Score! Score: " + player.score);
                break;
        }
    }
}

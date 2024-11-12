using UnityEngine;

public class Powerup : MonoBehaviour, IPickup
{
    public void Pickup(GameObject player)
    {
        PlayerController pc = player.GetComponent<PlayerController>();
        pc.JumpPowerUp(); // Trigger the jump power-up effect
        Destroy(gameObject); // Destroys the power-up on collision with the player
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Pickup(collision.gameObject); // Call Pickup when the player collides
        }
    }
}

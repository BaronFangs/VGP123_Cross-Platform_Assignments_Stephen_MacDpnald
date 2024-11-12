using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField, Range(1, 50)] private float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy after a set time if it doesn't collide with anything
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ignore collisions with Player, Collectible, or PLatofrm edge
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Collectible") || collision.gameObject.CompareTag("Edge"))
        {
            return;
        }
        Destroy(gameObject); // Destroy on collision with any other object
    }

    public void SetVelocity(Vector2 velocity)
    {
        GetComponent<Rigidbody2D>().velocity = velocity;
    }
}


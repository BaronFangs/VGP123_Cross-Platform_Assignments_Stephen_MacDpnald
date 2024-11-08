using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    SpriteRenderer sr;
    
    public Vector2 initialShotVelocity;
    public Transform spawnPointLeft;
    public Transform spawnPointRight;
    public Projectile projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (initialShotVelocity == Vector2.zero)
        {
            Debug.Log("Initial shot velocity is zero, changing it to a default value");
            initialShotVelocity.x = 7.0f;
        }

        if (!spawnPointLeft || !spawnPointRight || !projectilePrefab)
        {
            Debug.Log($"Please set default values on the shoot script for {gameObject.name}");
        }
    }

    public void Fire()
    {
        // Determine direction by checking the flipX property
        Vector2 shotVelocity = initialShotVelocity;

        if (sr.flipX)
        {
            // Facing left
            shotVelocity.x = -Mathf.Abs(initialShotVelocity.x); // Ensure velocity is negative for left
            Projectile curProjectile = Instantiate(projectilePrefab, spawnPointLeft.position, spawnPointLeft.rotation);
            curProjectile.SetVelocity(shotVelocity);
        }
        else
        {
            // Facing right
            shotVelocity.x = Mathf.Abs(initialShotVelocity.x); // Ensure velocity is positive for right
            Projectile curProjectile = Instantiate(projectilePrefab, spawnPointRight.position, spawnPointRight.rotation);
            curProjectile.SetVelocity(shotVelocity);
        }
    }
}

using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator), typeof(AudioSource))]
public abstract class Enemy : MonoBehaviour
{
    protected SpriteRenderer sr;
    protected Animator anim;
    protected int health;
    [SerializeField] protected int maxHealth;
    [SerializeField] private AudioClip deathSound;
    private AudioSource audioSource;

    public virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (maxHealth <= 0) maxHealth = 5;

        health = maxHealth;
    }

    public virtual void TakeDamage(int damageValue)
    {
        health -= damageValue;

        if (health <= 0)
        {

            if (deathSound != null)
            {
                audioSource.PlayOneShot(deathSound);
            }

            anim.SetTrigger("Death");

            if (transform.parent != null) Destroy(transform.parent.gameObject, 0.5f);
            else Destroy(gameObject, 0.5f);
        }
    }
}
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
[RequireComponent(typeof(GroundCheck), typeof(Jump), typeof(Shoot))]
public class PlayerController : MonoBehaviour
{
    public int lives = 3; // Player's current lives
    public int score = 0; // Player's score

    [Range(3f, 10f)] public float speed = 5.5f;
    [Range(3f, 10f)] public float jumpForce = 3f;
    public bool isGrounded = false;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private GroundCheck gc;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        gc = GetComponent<GroundCheck>();
    }

    void Update()
    {
        CheckIsGrounded();
        float hInput = Input.GetAxis("Horizontal");

        // Update velocity based on horizontal input if not in a "Fire" animation
        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);
        if (curPlayingClips.Length == 0 || curPlayingClips[0].clip.name != "Fire")
        {
            rb.velocity = new Vector2(hInput * speed, rb.velocity.y);
        }

        // Flip the sprite based on direction of movement
        if (hInput != 0)
        {
            sr.flipX = hInput < 0;
        }

        // Check for attack input
        if (Input.GetButtonDown("Fire1"))
        {
            if (isGrounded)
            {
                anim.SetTrigger("fire"); // Ground-based attack
            }
            else
            {
                anim.SetTrigger("jumpAttack"); // Air-based attack
            }
        }

        anim.SetFloat("speed", Mathf.Abs(hInput));
        anim.SetBool("isGrounded", isGrounded);
    }

    void CheckIsGrounded()
    {
        if (isGrounded && rb.velocity.y <= 0)
        {
            isGrounded = gc.IsGrounded();
        }
        else
        {
            isGrounded = gc.IsGrounded();
        }
    }

    // Jump power-up 
    public void JumpPowerUp()
    {
        //StartCoroutine(GetComponent<Jump>().JumpHeightChange());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPickUp curPickup = collision.GetComponent<IPickUp>();
        if (curPickup != null)
        {
            curPickup.Pickup(gameObject);
        }
    }
}

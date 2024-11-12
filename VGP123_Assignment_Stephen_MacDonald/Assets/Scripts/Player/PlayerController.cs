using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
[RequireComponent(typeof(GroundCheck), typeof(Jump), typeof(Shoot))]
public class PlayerController : MonoBehaviour
{
    private int _lives;
    public int lives
    {
        get => _lives;
        set
        {
            if (value <= 0)
            {
                // Game over or respawn logic here
            }
            else if (_lives > value)
            {
                // Respawn or damage logic here
            }
            _lives = value;
            Debug.Log($"Lives: {_lives}");
        }
    }

    private int _score;
    public int score
    {
        get => _score;
        set
        {
            if (value > 0)
            {
                _score = value;
                Debug.Log($"Score: {_score}");
            }
        }
    }

    [Range(3f, 10f)] public float speed = 5.5f;
    [Range(3f, 10f)] public float jumpForce = 3f;
    public bool isGrounded = false;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    GroundCheck gc;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        gc = GetComponent<GroundCheck>();
    }

    void Update()
    {
        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);
        CheckIsGrounded();
        float hInput = Input.GetAxis("Horizontal");

        // Update velocity based on horizontal input if not in a "Fire" animation
        if (curPlayingClips.Length > 0 && curPlayingClips[0].clip.name != "Fire")
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
        if (!isGrounded && rb.velocity.y <= 0)
        {
            isGrounded = gc.IsGrounded();
        }
        else
        {
            isGrounded = gc.IsGrounded();
        }
    }

    public void JumpPowerUp()
    {
        //StartCoroutine(GetComponent<Jump>().JumpHeightChange());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPickup curPickup = collision.GetComponent<IPickup>();
        if (curPickup != null)
        {
            curPickup.Pickup(gameObject);
        }
    }
}

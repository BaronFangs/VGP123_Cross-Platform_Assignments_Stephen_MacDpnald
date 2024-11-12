using System.Collections;
using System.Collections.Generic;
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
            if (value > 0)
            { //game over
            }

            if (_lives > value)
            {
                //respawn
            }
            _lives = value;
            Debug.Log($"{_lives}");
        }
    }

    private int _score;
    public int score
    {
        get => _score;
        set
        {
            if (value > 0) return;

            _score = value;
            Debug.Log($"{_score}");
        }
    }

    [Range(3f, 10)]
    public float speed = 5.5f;
    [Range(3f, 10)]
    public float jumpForce = 3f;

    public bool isGrounded = false;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    GroundCheck gc;
    

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        gc = GetComponent<GroundCheck>();
        

       
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);
        CheckIsGrounded();
        float hInput = Input.GetAxis("Horizontal");

        if (curPlayingClips.Length > 0) 
        {
            if (!(curPlayingClips[0].clip.name == "Fire"))
            {
                rb.velocity = new Vector2(hInput * speed, rb.velocity.y);
            }
            
            
        }
            
        
       
        

        

        if (hInput != 0) sr.flipX = (hInput < 0);
        if (Input.GetButtonDown("Fire1") && isGrounded) anim.SetTrigger("fire"); 
        if (Input.GetButtonDown("Fire1") && !isGrounded) anim.SetTrigger("jumpAttack");

       

        //if (hInput > 0 && sr.flipX || hInput < 0 && !sr.flipX) sr.flipX = !sr.flipX;

        anim.SetFloat("speed", Mathf.Abs(hInput));
        anim.SetBool("isGrounded", isGrounded);
        
    }

    void CheckIsGrounded() 
    {
        if (!isGrounded)
        {
            if (rb.velocity.y <= 0) isGrounded = gc.IsGrounded();

        }
        else isGrounded = gc.IsGrounded();
    }

    public void JumpPowerUp()
    {
        //StartCoroutine(GetComponent<Jump>().JumpHeightChange());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPickup curPickup = collision.GetComponent<IPickup>();
        {
            if (curPickup != null)
            {
                curPickup.Pickup(gameObject);
            }
        }
    }
}

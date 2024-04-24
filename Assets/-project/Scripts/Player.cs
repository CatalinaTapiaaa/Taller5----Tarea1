using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Player : MonoBehaviour
{
    public GameObject explosion;
    [Header("Movement")]
    public Animator aniMove;
    public float speed;
    public float jumpForce;
    Vector2 move;
    bool flip;
    Rigidbody2D rb;
    [Header("Raycast Box")]
    public LayerMask groundLayer;
    public float groundCheckerCastDistance;
    public Vector2 groundCheckerBoxSize;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(move.x * speed * Time.deltaTime, rb.velocity.y);
    }
    void Update()
    {
        if (!GameManager.stopPlayer)
        {
            rb.constraints &= ~RigidbodyConstraints2D.FreezePosition;

            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            move = new Vector2(h, v);

            aniMove.SetFloat("Move", Mathf.Abs(h));
            speed += GameManager.speedPlayer * Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
            {
                rb.AddForce(Vector2.up * jumpForce);
                aniMove.SetTrigger("Take Of");
            } 

            if (h > 0 && flip)
            {
                Flip();
            }
            else if (h < 0 && !flip)
            {
                Flip();
            }
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
        }

        isGrounded();
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * groundCheckerCastDistance, groundCheckerBoxSize);
    }
   
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            GameManager.victory = true;
            aniMove.SetBool("Stop", true);
        }
        if (collision.gameObject.CompareTag("Spikes"))
        {
            GameManager.death = true;
        }
    }

    public bool isGrounded()
    {
        if (Physics2D.BoxCast(transform.position, groundCheckerBoxSize, 0, -transform.up, groundCheckerCastDistance, groundLayer))
        {
            aniMove.SetBool("Jump", false);
            return true;
        }
        else
        {
            aniMove.SetBool("Jump",true);
            return false;
        }
    }
    void Flip()
    {
        flip = !flip;
        transform.Rotate(0, 180, 0);
    }

    public void isDeath()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
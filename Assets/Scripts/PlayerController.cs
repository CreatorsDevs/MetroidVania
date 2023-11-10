using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundPoint;
    [SerializeField] private BulletController shotToFire;
    [SerializeField] private Transform shotPoint;

    
    public LayerMask whatIsGround;
    public Animator anim;

    private bool isOnGround;
    private bool canDoubleJump;


    void Start()
    {
        
    }

    void Update()
    {
        // Moving sideways
        rb.velocity = new Vector2 (Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y);

        // Handle direction changes
        if(rb.velocity.x < 0 )
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if(rb.velocity.x > 0 )
        {
            transform.localScale = Vector3.one;
        }

        // Checking if player is on ground
        isOnGround = Physics2D.OverlapCircle(groundPoint.position, 0.2f, whatIsGround);

        // Jumping
        if(Input.GetButtonDown("Jump") && (isOnGround || canDoubleJump))
        {
            if(isOnGround)
            {
                canDoubleJump = true;
            }
            else
            {
                canDoubleJump = false;
                anim.SetTrigger("doubleJump");
            }
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Player Shooting mechanism
        if(Input.GetButtonDown("Fire1"))
        {
            Instantiate(shotToFire, shotPoint.position, shotPoint.rotation).moveDir = new Vector2(transform.localScale.x, 0f);
            anim.SetTrigger("shotFired");
        }

        // Animation controllers
        anim.SetBool("isOnGround", isOnGround);
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
    }
}

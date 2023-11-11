using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundPoint;
    [SerializeField] private BulletController shotToFire;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private float dashSpeed, dashTime;
    [SerializeField] private SpriteRenderer sr, afterImage;
    [SerializeField] private float afterImageLifeTime, timeBetweenAfterImages;
    [SerializeField] private float waitAfterDashing;
    [SerializeField] private GameObject standing, ball;
    [SerializeField] private float waitToBall;


    public LayerMask whatIsGround;
    public Animator anim;
    public Color afterImageColor;

    private bool isOnGround;
    private bool canDoubleJump;
    private float dashCounter;
    private float afterImageCounter;
    private float dashRechargeCounter;
    private float ballCounter;

    void Start()
    {
        
    }

    void Update()
    {
        // Dashing Ability and Movement controllers
        if (dashRechargeCounter > 0)
        {
            dashRechargeCounter -= Time.deltaTime;
        }
        else
        {
            if (Input.GetButtonDown("Fire2") && standing.activeSelf)
            {
                dashCounter = dashTime;
                ShowAfterImage();
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            rb.velocity = new Vector2(dashSpeed * transform.localScale.x, rb.velocity.y);

            afterImageCounter -= Time.deltaTime;
            if(afterImageCounter <= 0)
            {
                ShowAfterImage();
            }

            dashRechargeCounter = waitAfterDashing;
        }
        else
        {
            // Moving sideways
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y);

            // Handle direction changes
            if (rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (rb.velocity.x > 0)
            {
                transform.localScale = Vector3.one;
            }
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

        // Ball State - State Change Ability
        if(!ball.activeSelf)
        {
            if(Input.GetAxisRaw("Vertical") < -0.9f)
            {
                ballCounter -= Time.deltaTime;
                if(ballCounter <= 0)
                {
                    ball.SetActive(true);
                    standing.SetActive(false);
                }
            }
            else
            {
                ballCounter = waitToBall;
            }
        }
        else
        {
            if (Input.GetAxisRaw("Vertical") > 0.9f)
            {
                ballCounter -= Time.deltaTime;
                if (ballCounter <= 0)
                {
                    ball.SetActive(false);
                    standing.SetActive(true);
                }
            }
            else
            {
                ballCounter = waitToBall;
            }
        }

        // Animation controllers
        anim.SetBool("isOnGround", isOnGround);
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
    }

    private void ShowAfterImage()
    {
        SpriteRenderer image = Instantiate(afterImage, transform.position, transform.rotation);
        image.sprite = sr.sprite;
        image.transform.localScale = transform.localScale;
        image.color = afterImageColor;

        Destroy(image.gameObject, afterImageLifeTime);

        afterImageCounter = timeBetweenAfterImages;
    }
}

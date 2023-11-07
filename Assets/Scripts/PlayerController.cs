using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundPoint;

    private bool isOnGround;
    public LayerMask whatIsGround;

    public Animator anim;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2 (Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y);

        isOnGround = Physics2D.OverlapCircle(groundPoint.position, 0.2f, whatIsGround);

        if(Input.GetButtonDown("Jump") && isOnGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }




        anim.SetBool("isOnGround", isOnGround);
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
    }
}

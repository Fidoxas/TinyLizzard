using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float speed = 5f;
    private float jumpForce = 7f;
    [SerializeField] private Animator anim;
    private SpriteRenderer _sprite;
    private bool isGrounded;

    void Start()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        _rb.linearVelocity = new Vector2(horizontal * speed, _rb.linearVelocity.y);

        _sprite.transform.rotation = Quaternion.identity;

        anim.SetBool("Walking", horizontal != 0);

        if (horizontal > 0)
        { 
            _sprite.transform.rotation = Quaternion.Euler(0, 0, -5);
        }
        else if (horizontal < 0)
        {
            _sprite.transform.rotation = Quaternion.Euler(0, 0, 5);
        }

        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
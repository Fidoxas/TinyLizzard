using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float speed;
    private bool inAir;
    [FormerlySerializedAs("jumpHeight")] [SerializeField] private float jumpForce;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        inAir = false;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        inAir = true;
    }

    void Update()
    {
        float Horizontal = Input.GetAxis("Horizontal");

        if (Horizontal == 0)
        {
            _rb.linearVelocity = new Vector2(0f,_rb.linearVelocity.y);
        }
        if (Horizontal> 0 )
        {
            _rb.linearVelocity = new Vector2(speed, _rb.linearVelocity.y);
        }

        if (Horizontal < 0)
        {
            _rb.linearVelocity = new Vector2(-speed, _rb.linearVelocity.y);
        }
        
        if(Input.GetKeyDown(KeyCode.W))
        {
            if (!inAir)
            {
                _rb.AddForce(new Vector2(0,jumpForce), ForceMode2D.Impulse);
            }
        }
    }
}
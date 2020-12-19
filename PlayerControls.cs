using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    Rigidbody2D rb;
    Controls controls;
    Animator anim;
    SpriteRenderer rend;

    [SerializeField]
    float speed;
    Vector2 m_speed;
    bool facingRight;



    private void Awake()
    {
         controls = new Controls();
        controls.GamePlay.Move.started += ctx => Move(ctx.ReadValue<Vector2>());
        controls.GamePlay.Move.canceled += ctx => StopMove();
        controls.GamePlay.Jump.started += ctx => Jump();

        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.velocity = new Vector2(m_speed.x, m_speed.y);
      

        if(m_speed.y > 0)
        {
            anim.SetBool("movingUp", true);
        }
        else if (m_speed.y <  0)
        {
            anim.SetBool("movingDown", true);
        }
        else if (m_speed.x > 0)
        {
            anim.SetBool("movingRight", true);
        }
        else if (m_speed.x < 0)
        {
            anim.SetBool("movingLeft", true);
        }

    }

    void Jump()
    {
        rb.AddForce(new Vector2(0,10), ForceMode2D.Impulse);
    }

    private void Move(Vector2 direction)
    {
        m_speed = new Vector2(direction.x * speed, direction.y * speed);
    }

    void StopMove()
    {
        anim.SetBool("movingUp", false);
        anim.SetBool("movingDown", false);
        anim.SetBool("movingLeft", false);
        anim.SetBool("movingRight", false);
        m_speed = Vector2.zero;
    }

    void Flip()
    {
        if (facingRight)
        {
            rend.flipX = true;
            facingRight = false;
        }
        else
        {
            rend.flipX = false;
            facingRight = true;
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }
}

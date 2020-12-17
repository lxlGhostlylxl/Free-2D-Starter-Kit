using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField]
   
    float speed;
    Rigidbody2D rb;
    Controls controls;

    private void Awake()
    {
         controls = new Controls();
        controls.GamePlay.Move.started += ctx => Move(ctx.ReadValue<Vector2>());
        controls.GamePlay.Move.canceled += ctx => StopMove();
        controls.GamePlay.Jump.started += ctx => Jump();

        rb = GetComponent<Rigidbody2D>();
    }



    void Jump()
    {
        rb.AddForce(new Vector2(0,10), ForceMode2D.Impulse);
    }

    private void Move(Vector2 direction)
    {
        rb.velocity = new Vector2((direction.x * speed), rb.velocity.y);
    }

    void StopMove()
    {
        rb.velocity = Vector2.zero;
    }


    private void OnEnable()
    {
        controls.Enable();
    }
}

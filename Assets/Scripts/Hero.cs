using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float speed = 3f; // скорость движения
    [SerializeField] private int lives = 5; // количество жизней
    [SerializeField] private float jumpForce = 15f;
    private bool isGrounded = false;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;


    public static Hero Instance { get; set; }
    
    private States State
    {
        get { return (States)anim.GetInteger("state");}
        set { anim.SetInteger("state", (int)value);}
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        Instance = this;
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        if (isGrounded) State = States.Idle;
        
        if(Input.GetButton("Horizontal"))
            Run();
        if(isGrounded && Input.GetButtonDown("Jump"))
            Jump();
    }

    private void Run()
    {
        if (isGrounded) State = States.Run;
        
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;

    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        
        
    }

    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.8f);
        isGrounded = collider.Length > 1;
        
        if (!isGrounded) State = States.Jump;
    }

    public  void GetDamage()
    {
        lives -= 1;
        Debug.Log(lives);
    }

}

public enum States
{
    Idle,
    Run,
    Jump
    
}
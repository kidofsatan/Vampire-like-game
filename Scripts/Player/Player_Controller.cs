using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{

    public float movmentSpeed = 5f;

    public Animator animator;

    
    private Rigidbody2D rb;
    private Vector3 moveDir;
    private Vector3 lastMoveDir;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {

        var delta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
       
        
                float moveX = 0f;
                float moveY = 0f;

                if (Input.GetKey(KeyCode.W))
                {
                    moveY = +1f;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    moveY = -1f;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    transform.localScale = new Vector3(-1, 1, 1); //right
                    moveX = -1f;
                    
                }
                if (Input.GetKey(KeyCode.D))
                {
                    transform.localScale = new Vector3(1, 1, 1); // left
                    moveX = +1f;
                    
                }

                if(moveX != 0 || moveY != 0)
                {
                    animator.SetBool("isMoving", true);
                    lastMoveDir = moveDir;
                }
                else
                {
                    animator.SetBool("isMoving", false);
                }

                moveDir = new Vector3(moveX, moveY).normalized;
          



      
    }

    private void FixedUpdate()
    {
        
                rb.velocity = moveDir * movmentSpeed;
               
    }
}


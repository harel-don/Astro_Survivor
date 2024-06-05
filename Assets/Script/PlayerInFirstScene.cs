using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInFirstScene : MonoBehaviour
{
    private Vector2 Direction;
    [SerializeField] private float WalkSpeed = 4;
    [SerializeField] private KinematicTopDownController Controller_k;

    [SerializeField] private Animator anim;

    private Rigidbody2D rig;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");
        Vector2 movement = Vector2.zero;
        if (horizontalMovement != 0.0f)
        {
            movement.x = horizontalMovement;
            var side = movement.x > 0 ? 1 : -1;
            anim.SetInteger("direction", side);
        }
        else if (verticalMovement != 0.0f)
        {
            movement.y = verticalMovement;
            var side = movement.y > 0 ? 2 : -2;
            anim.SetInteger("direction", side);
        }

        // Update the Animator only when motion
        if (movement != Vector2.zero)
        {
            Direction = movement;
        }
        
    }

    private void FixedUpdate()
    {
        if (Direction != Vector2.zero)
        {
            
            Controller_k.MovePosition(Direction, WalkSpeed);
            anim.SetTrigger("walk");
            
        }
        else
        {
            rig.velocity = Vector2.zero;
            
        }
    }
}

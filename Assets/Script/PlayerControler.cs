using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public int player_id;
    public KeyCode left_key;
    public KeyCode right_key;
    public KeyCode up_key;
    public KeyCode down_key;
    public KeyCode action_key;
    public KeyCode action_key1;
    public KeyCode action_key2;
    public KeyCode action_key3;
    public KeyCode action_key4;
    private Vector2 move = Vector2.zero;
    private bool action_press = false;
    private bool action_hold = false;
    private bool action_hold1 = false;
    private bool action_hold2 = false;
    private bool action_hold3 = false;
    private bool action_hold4 = false;
    private bool isMove;

    [SerializeField]private Animator anim;
    

    private int directionindec = 0;

    private ScriptForPlayer scriptForPlayer;
    // Start is called before the first frame update
    void Start()
    {
        scriptForPlayer = GetComponent<ScriptForPlayer>();
    }

    private void Awake()
    {
        // anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        move = Vector2.zero;
        action_hold = false;
        action_hold1 = false;
        action_hold2 = false;
        action_hold3 = false;
        action_hold4 = false;
        action_press = false;
        isMove = false;
        if (Input.GetKey(left_key))
        {
            move += -Vector2.right;
            directionindec = -1;
            isMove = true;
        }
        if (Input.GetKey(right_key))
        {
            move += Vector2.right;
            directionindec = 1;
            isMove = true;
        }
        if (Input.GetKey(up_key))
        {
            move += Vector2.up;
            directionindec = 2;
            isMove = true;
        }
        if (Input.GetKey(down_key))
        {
            move += -Vector2.up;
            directionindec = -2;
            isMove = true;
        }
        if (Input.GetKey(action_key))
        {
            action_hold = true;
        }
        if (Input.GetKey(action_key1))
        {
            action_hold1 = true;
        }
        if (Input.GetKey(action_key2))
        {
            action_hold2 = true;
        }
        if (Input.GetKey(action_key3))
        {
            action_hold3 = true;
        }
        if (Input.GetKey(action_key4))
        {
            action_hold4 = true;
        }
        if (Input.GetKeyDown(action_key))
        {
            action_press = true;
        }

        float move_length = Mathf.Min(move.magnitude, 1f);
        move = move.normalized * move_length;
    }

    private void FixedUpdate()
    {
        // anim.SetInteger("direction", directionindec);
        // if(isMove) anim.SetTrigger("walk");
        
    }
    
    public bool GetActionDown()
    {
        return action_press;
    }

    public bool GetActionHold()
    {
        return action_hold;
    } public bool GetActionHold1()
    {
        return action_hold1;
    } public bool GetActionHold2()
    {
        return action_hold2;
    } public bool GetActionHold3()
    {
        return action_hold3;
    } public bool GetActionHold4()
    {
        return action_hold4;
    }
    public Vector2 GetMove()
    {
        return move;
    }

    public int GetDirect()
    {
        return directionindec;
    }

    public bool IsMoving()
    {
        return scriptForPlayer.getMove();
    }
    
}

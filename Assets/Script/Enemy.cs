using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    abstract protected void Death();
    abstract protected void Move();
    [SerializeField] protected int speed;
    [SerializeField] protected GameObject player;
    protected Rigidbody2D rig;

    [SerializeField] protected float intHp = 8;
    [SerializeField] protected Animator m_Animator;
    protected float hp;
    protected Vector3 initTransform;
    [SerializeField] protected float lhit = 1;
    [SerializeField] protected float scoreWorth = 1;
    
    
    
    // Start is called before the first frame update
    virtual protected void  Start()
    {
        rig = GetComponent<Rigidbody2D>();
        hp = intHp;
        initTransform = gameObject.transform.position;
        m_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
   
    
 
    protected virtual void Update()
    {
        if (hp <= 0)
        {
            Death();
           
        }
        // Vector3 localPosition = player.transform.position - transform.position;
        // localPosition = localPosition.normalized; // The normalized direction in LOCAL space
        // transform.Translate(localPosition.x * Time.deltaTime * speed, localPosition.y * Time.deltaTime * speed, localPosition.z * Time.deltaTime * speed);
    }

    public void SetPlayer(GameObject play)
    {
        player = play;
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<ScriptForPlayer>().hit(lhit);
        }
    }

    protected void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<ScriptForPlayer>().hit(lhit);
        }
    }

    public void hit(float ht)
    {
        hp -= ht;
        m_Animator.SetTrigger("hit");
    }
    
    public virtual void vanish()
    {
        gameObject.SetActive(false);
    }
    
    
}

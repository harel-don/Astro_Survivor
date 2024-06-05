using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : Enemy
{
    private float timer;

    [SerializeField] private float timeToMove = 3;
    private bool Tomove;
    [SerializeField] private float timeToStop = 1;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        
        timer += Time.deltaTime;
        
        if (Tomove)
        {
            if (timer < timeToStop)
            {
                Move();  
            }
            else
            {
                timer = 0;
                Tomove = false;
            }
        }
        else
        {
            if (timer < timeToMove)
            {
                timer = 0;
                Tomove = true;
                
            }
        }
        
    }

    protected override void Death()
    {
        ScoreScript.addScore(scoreWorth);
        m_Animator.SetTrigger("dead");
        gameObject.SetActive(false);
    }

   

    protected override void Move()
    {
        rig.transform.position = Vector3.MoveTowards( transform.position, player.transform.position, speed * Time.deltaTime );
        // Vector3 localPosition = player.transform.position - transform.position;
        // localPosition = localPosition.normalized; // The normalized direction in LOCAL space
        // rig.transform.Translate(localPosition.x * Time.deltaTime * speed, localPosition.y * Time.deltaTime * speed, localPosition.z * Time.deltaTime * speed);

    }

    protected override void OnCollisionEnter2D(Collision2D col)
    {
        base.OnCollisionEnter2D(col);
    }

    public override void vanish()
    {
        base.gameObject.SetActive(false);
    }
}

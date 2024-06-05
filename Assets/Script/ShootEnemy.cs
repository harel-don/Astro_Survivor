using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootEnemy :  Enemy
{
    private float timer;

    [SerializeField] private float timeToMove;
    private bool Tomove;
    [SerializeField] private float timeToStop;
    [SerializeField] private float projectileSpeed;

    [SerializeField] private Transform[] spots;

    [SerializeField] private GameObject pops;
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
                Fire();
            }
        }
        else
        {
            if (timer < timeToMove)
            {
                timer = 0;
                Tomove = true;
                Fire();
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

    private void Fire()
    {
        m_Animator.SetTrigger("attack");
        foreach (var spot in spots)
        {
            GameObject star = Instantiate(pops, spot.position ,
                spot.rotation) as GameObject;

            star.GetComponent<Rigidbody2D>().AddForce( spot.up * projectileSpeed);
        }
    }
    
    public override void vanish()
    {
        base.gameObject.SetActive(false);
    }
}

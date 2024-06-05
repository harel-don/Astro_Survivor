using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySkel : Enemy
{
    [SerializeField] private GameObject skull;
    protected override void Death()
    {
        ScoreScript.addScore(scoreWorth);
        skull.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    protected override void Move()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnCollisionEnter2D(Collision2D col)
    {
        base.OnCollisionEnter2D(col);
    }


    private void Awake()
    {
        rig = GetComponentInParent<Rigidbody2D>();
        hp = intHp;
        initTransform = gameObject.transform.position;
        m_Animator = gameObject.GetComponentInParent<Animator>();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        // base.Start();
        rig = GetComponentInParent<Rigidbody2D>();
        hp = intHp;
        initTransform = gameObject.transform.position;
        m_Animator = gameObject.GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    
    public override void vanish()
    {
        skull.gameObject.SetActive(false);
    }
}

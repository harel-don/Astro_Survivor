using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombScript : MonoBehaviour
{

    [SerializeField] private float timeTosetBomb =1.5f;
    private Animator anim;
    private bool Expl;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > timeTosetBomb)
        {
            setBomb();
        }
        // if (timer > TimeToExpl)
        // {
        //     gameObject.SetActive(false);
        // }
        timer += Time.deltaTime;
        
    }

    private void setBomb()
    {
        // Expl = true;
        gameObject.SetActive(false);
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (Expl)
        {
            if (col.gameObject.CompareTag("enemyTag"))
            {
                col.gameObject.GetComponent<Enemy>().hit(2);
            }
            else if (col.gameObject.CompareTag("Player"))
            {
                col.gameObject.GetComponent<ScriptForPlayer>().hit(2);
            }
            
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Expl)
        {
            if (other.gameObject.CompareTag("enemyTag"))
            {
                other.gameObject.GetComponent<Enemy>().hit(2);
            }
            else if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<ScriptForPlayer>().hit(2);
            }
           
            
        }
    }

    public void explotion()
    {
        Expl = true;
        gameObject.GetComponent<AudioSource>().Play();
    }
}

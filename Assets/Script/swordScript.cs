using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordScript : MonoBehaviour
{
    [SerializeField] private float hitDamge = 1;
    [SerializeField] private float timerToswing = 1;
    private float timerSword;
    // Start is called before the first frame update

    private void OnEnable()
    {
        GetComponent<Animator>().SetTrigger("go");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timerSword += Time.deltaTime;
        if (timerSword >= timerToswing)
        {
            timerSword = 0;
            gameObject.SetActive(false);
            // Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("enemyTag"))
        {
            col.gameObject.GetComponent<Enemy>().hit(hitDamge);
            gameObject.SetActive(false);
        }
        // if(col.gameObject.CompareTag("wall"))
        // {
        //     // Destroy(gameObject);
        // }
        
    }
}

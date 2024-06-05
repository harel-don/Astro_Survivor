using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulitScript : MonoBehaviour
{
    [SerializeField] private float hitDamge = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("enemyTag"))
        {
            col.gameObject.GetComponent<Enemy>().hit(hitDamge);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else if(col.gameObject.CompareTag("wall"))
        {
            Destroy(gameObject);
        }
        
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProj : MonoBehaviour
{
    [SerializeField]private float hitDamge = 1;
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
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<ScriptForPlayer>().hit(hitDamge);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        if(col.gameObject.CompareTag("wall"))
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class openSceneByTrig : MonoBehaviour
{
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
        SceneManager.LoadScene("Game");
        // if(col.gameObject.CompareTag("wall"))
        // {
        //     // Destroy(gameObject);
        // }

    }
}

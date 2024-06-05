using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.SceneManagement;
public class colideWithMoonScript : MonoBehaviour
{
    private float timer;

    [SerializeField] private float timeToGo = 2;

    private bool toGo;

    [SerializeField] private AudioSource hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (toGo)
        {
            timer += Time.deltaTime;
            if (timer >= timeToGo)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        toGo = true;
        hit.Play();
        
    }
}

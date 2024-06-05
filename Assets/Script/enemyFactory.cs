using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyFactory : MonoBehaviour
{
    private float timer;

    [SerializeField] private float timeToSpawnEnemy;
    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject [] enemyObj;
    [SerializeField] private Transform [] posList;
    
    [SerializeField] private GameObject player;
    // [SerializeField] private float timeToSpawnEnemy;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= timeToSpawnEnemy)
        {
            var en = enemyObj[Random.Range((int)0, enemyObj.Length)];
            var pos = posList[Random.Range(0, posList.Length)];
            var enem = Instantiate(en, pos.position,transform.rotation) as GameObject;
            if (enem.CompareTag("Enemy") == false)
            {
                enem.GetComponent<Enemy>().SetPlayer(player);
            }

            timer = 0;
        }

        timer += Time.deltaTime;
    }
}

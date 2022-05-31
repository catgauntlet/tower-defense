using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] [Range(0, 50)] private int poolSize = 5;
    [SerializeField] [Range(0.1f, 30f)] float spawnTimer = 1f;

    private GameObject[] enemyPool;

    private void Awake()
    {
        PopulateEnemyPool();
    }


    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(SpawnEnemies());
    }

    private void PopulateEnemyPool()
    {
        enemyPool = new GameObject[poolSize];

        for (int i = 0; i < enemyPool.Length; i++)
        {
            enemyPool[i] = Instantiate(enemyPrefab, transform);
            enemyPool[i].SetActive(false);
        }
    }

    void EnableObjectInPool()
    {
        foreach(GameObject enemy in enemyPool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.SetActive(true);
                return;
            }
        }
    }
    IEnumerator SpawnEnemies()
    {
        while(true)
        {
              EnableObjectInPool();
              yield return new WaitForSeconds(spawnTimer);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolController : MonoBehaviour
{
    [SerializeField] private int poolSize = 5;
    [SerializeField] private GameObject enemyPrefab;

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
              yield return new WaitForSeconds(1f);
        }
    }
}

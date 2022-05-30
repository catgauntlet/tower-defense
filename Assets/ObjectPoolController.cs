using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolController : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float spawnTimer = 1.0f;
    [SerializeField] int maxEnemyCount = 500;

    int enemyCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InstantiateEnemy());
    }

    IEnumerator InstantiateEnemy()
    {
        while(enemyCount < maxEnemyCount)
        {
            enemyCount++;
            Instantiate(enemyPrefab, transform);
            yield return new WaitForSeconds(1f);
        }
    }
}

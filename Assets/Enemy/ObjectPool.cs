using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPool : MonoBehaviour
{

    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private int poolSize = 5;
    [SerializeField] private int spawnTimer = 1;

    private List<GameObject> pool;

    private void Awake() {
        pool = Enumerable
            .Range(0,poolSize)
            .Select( i => Instantiate(EnemyPrefab, transform))
            .Select( go => { go.SetActive(false); return go;})
            .ToList();    
    }

    

    void Start()
    {
       StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while(true)
        {
            var nextInPool = pool.FirstOrDefault(go => go.activeSelf == false);
            if(nextInPool != null)
            {
                nextInPool.SetActive(true);
            }
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}

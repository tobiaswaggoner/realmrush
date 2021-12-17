using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPool : MonoBehaviour
{

    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private int poolSize = 5;
    [SerializeField] private float spawnTimer = 1.0f;

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
        yield return new WaitForSeconds(10);
        while(true)
        {
            var nextInPool = pool.FirstOrDefault(go => go.activeSelf == false);
            if(nextInPool != null)
            {
                nextInPool.SetActive(true);
                if(spawnTimer>0.5f) spawnTimer-=0.01f;
            }
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}

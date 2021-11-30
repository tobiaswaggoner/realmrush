using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{

    private List<Waypoint> Path;

    [SerializeField]
    [Range(0.1f, 5.0f)]    private float Speed = 1.0f;

    public float AbsolutePosition = 0.0f;

    Enemy enemy;


    private void Start() {
        enemy = GetComponent<Enemy>();
        
    }
    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath()); 
    }

    void FindPath()
    {
        Path = GameObject
                    .FindGameObjectWithTag("Path")
                    .GetComponentsInChildren<Waypoint>()
                    .ToList();
    }

    public void ReturnToStart()
    {
        var enemyHealth = GetComponentInParent<EnemyHealth>();
        var scaleFactor = (enemyHealth? enemyHealth.MaxHitPoints - 5 : 5) * 0.01f;
        transform.position = Path[0].transform.position;
        transform.localScale = new Vector3(1 +  scaleFactor , 1 + scaleFactor, 1 + scaleFactor * 10f);
        Speed = Speed + 0.01f;
    }

    private IEnumerator FollowPath()
    {
        foreach (var waypoint in Path.Skip(1))
        {
            var startPosition = transform.position;
            var endPosition = waypoint.transform.position;
            var travelPercent = 0f;

            while(travelPercent<1)
            {
                travelPercent += Time.deltaTime * Speed;
                AbsolutePosition = Path.IndexOf(waypoint) + travelPercent;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                var neededRotation = Quaternion.LookRotation(startPosition - endPosition);    
                transform.rotation = Quaternion.RotateTowards(transform.rotation, neededRotation, Time.deltaTime * 500f);
                yield return new WaitForEndOfFrame();
            }
        }
        gameObject.SetActive(false);
        ReturnToStart();
        enemy.ReduceLives();

    }

}

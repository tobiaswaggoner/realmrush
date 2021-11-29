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
        transform.position = Path[0].transform.position;
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
                transform.LookAt(endPosition);
                yield return new WaitForEndOfFrame();
            }
        }
        gameObject.SetActive(false);
        ReturnToStart();
        enemy.ReduceLives();

    }

}

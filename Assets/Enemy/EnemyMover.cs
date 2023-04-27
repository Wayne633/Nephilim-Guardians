using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{

    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f,5f)] float speed = 1.0f; //ͨ������range���ԣ����ٶ�������һ���ķ�Χ

    Enemy enemy;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath()); // ����Я��Ҫ�õĹ̶���ʽ
    }

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void FindPath()
    {
        path.Clear();
        
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Path");

        foreach (GameObject waypoint in waypoints)
        {
            path.Add(waypoint.GetComponent<Waypoint>());
        }
    }

    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }


    IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPositon = waypoint.transform.position;
            float travelPercent = 0f;
          
            transform.LookAt(endPositon);
            
            while(travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed; // ��֡Ϊ��λǰ��
                transform.position = Vector3.Lerp(startPosition, endPositon, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }

        enemy.StealGold();
        gameObject.SetActive(false);



    }

}

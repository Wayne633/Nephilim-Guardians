using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Enemy))]  
public class EnemyMover : MonoBehaviour
{

    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f,5f)] float speed = 1.0f; //通过增加range属性，把速度限制在一定的范围

    Enemy enemy;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath()); // 调用携程要用的固定格式
    }

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void FindPath()
    {
        path.Clear();
        
        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        foreach (Transform child in parent.transform)
        {
            Waypoint waypoint = child.GetComponent<Waypoint>();

            if(waypoint != null )
            {
                path.Add(waypoint);
            }
            
        }
    }

    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }


    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);

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
                travelPercent += Time.deltaTime * speed; // 按帧为单位前进
                transform.position = Vector3.Lerp(startPosition, endPositon, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }

        FinishPath();


    }

}

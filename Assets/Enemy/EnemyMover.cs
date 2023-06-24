using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Enemy))]  
public class EnemyMover : MonoBehaviour
{

    [SerializeField] [Range(0f,5f)] float speed = 1.0f; //通过增加range属性，把速度限制在一定的范围

    List<Node> path = new List<Node>();

    Enemy enemy;
    GridManager gridManager;
    Pathfinder pathfinder;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
        
    }

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();

    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();

        if(resetPath)
        {
            coordinates = pathfinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }
              
        StopAllCoroutines();
        path.Clear();
        path = pathfinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath()); // 调用携程要用的固定格式
    }

    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }


    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);

    }
     




    IEnumerator FollowPath()
    {
        for(int i = 1; i < path.Count; i++) 
        {
            Vector3 startPosition = transform.position;
            Vector3 endPositon = gridManager.GetPositionFromCoordinates(path[i].coordinates);
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

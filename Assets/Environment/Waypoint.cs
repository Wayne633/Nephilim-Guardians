 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    [SerializeField] Tower towerPrefab;

    [SerializeField] bool isPlaceable;
    public bool IsPlaceable     // 该函数是上面的变量的一个属性
    {
        get { return isPlaceable; }
    }
    
    



    void OnMouseDown()
    {
        if (isPlaceable)
        {
            bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
            //Instantiate(towerPrefab, transform.position, Quaternion.identity); // 复制对象，用默认的位置和默认的旋转角度。
            isPlaceable = !isPlaced;
        }

    }

    //等价于下面的写法：
    /*
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(transform.name);
        }
    }
    */


}

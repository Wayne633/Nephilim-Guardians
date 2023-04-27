 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    [SerializeField] Tower towerPrefab;

    [SerializeField] bool isPlaceable;
    public bool IsPlaceable     // �ú���������ı�����һ������
    {
        get { return isPlaceable; }
    }
    
    



    void OnMouseDown()
    {
        if (isPlaceable)
        {
            bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
            //Instantiate(towerPrefab, transform.position, Quaternion.identity); // ���ƶ�����Ĭ�ϵ�λ�ú�Ĭ�ϵ���ת�Ƕȡ�
            isPlaceable = !isPlaced;
        }

    }

    //�ȼ��������д����
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

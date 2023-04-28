using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

// 该脚本可以让对象的坐标显示在任何位置实时改变
[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]

public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;



    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    Waypoint waypoint;


    void Awake()
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;


        waypoint = GetComponentInParent<Waypoint>();
        DisplayCoordinates();
    }


    // Update is called once per frame
    void Update()
    {
        //  在编辑模式下，显示游戏播放后（脚本运行后）的效果。在游戏播放模式反而不会运行  
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName(); 
        }

        SetLabelColor();
        ToggleLabels();
    }

    void ToggleLabels()
    {
        if(Input.GetKeyDown(KeyCode.C)) 
        { 
            label.enabled = !label.IsActive();
        }
    }



    void SetLabelColor()
    {
        if(waypoint.IsPlaceable)
        {
            label.color = defaultColor;
        }
        else
        { 
            label.color = blockedColor;
        }
    }

    void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

        label.text = coordinates.x + "," + coordinates.y;
    }


    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }


}

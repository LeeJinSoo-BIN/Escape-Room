using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closet_old : MonoBehaviour
{

    private GameObject left_axis;
    private GameObject right_axis;
    public bool leftIsOpen = false;
    public bool rightIsOpen = false;

    private float goal_left_y_rotation;
    private float goal_right_y_rotation;
    public float open_left_y_rotation = 90f;
    public float open_right_y_rotation = -90f;
    public float closed_y_rotation = 0f;

    public float openPower = 0.05f;
    public int endMove = 1000;
    // Start is called before the first frame update
    void Start()
    {
        left_axis = transform.Find("door_left_axis").gameObject;
        right_axis = transform.Find("door_right_axis").gameObject;
    }

    // Update is called once per frame

    public void Open_Close()
    {
        if (leftIsOpen)
            goal_left_y_rotation = open_left_y_rotation;
        else
            goal_left_y_rotation = closed_y_rotation;

        if (rightIsOpen)
            goal_right_y_rotation = open_right_y_rotation;
        else
            goal_right_y_rotation = closed_y_rotation;

        StartCoroutine(RotateAxis(left_axis, left_axis.transform.localRotation.y, goal_left_y_rotation, leftIsOpen));
        StartCoroutine(RotateAxis(right_axis, right_axis.transform.localRotation.y, goal_right_y_rotation, rightIsOpen));
    }

    IEnumerator RotateAxis(GameObject axis, float current_y_rotation, float goal_y_rotation, bool open)
    {
        int count = 0;
        
        while (current_y_rotation != goal_y_rotation)
        {   
            
                current_y_rotation = Mathf.LerpAngle(current_y_rotation, goal_y_rotation, openPower);
            //else
              //  current_y_rotation = Mathf.Lerp(current_y_rotation, goal_y_rotation, openPower);
            if (count > endMove)

                current_y_rotation = goal_y_rotation;
            axis.transform.localRotation = Quaternion.Euler(axis.transform.localRotation.x, current_y_rotation, axis.transform.localRotation.z);
            //
            yield return null;
            count++;
        }
    }
}
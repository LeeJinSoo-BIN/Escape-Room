using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawer : MonoBehaviour
{

    
    public int keyCode;
    /*
    private GameObject m_Male;
    public bool isOpen = false;
    private float goal_z_position;
    public float open_z_position = 0.8f;
    public float closed_z_position = 0.05f;
    public bool isLocked = true;
    public float openPower = 0.05f;
    public int endMove = 1000;
    public bool is_clicked = false;
    // Start is called before the first frame update
    void Start()
    {
        m_Male = transform.Find("drawer_m").gameObject;
    }

    // Update is called once per frame


    public IEnumerator Open_close()
    {
        if (isOpen)
            goal_z_position = open_z_position;
        else
            goal_z_position = closed_z_position;

        int count = 0;
        float current_z_position = m_Male.transform.localPosition.z;
        while(current_z_position != goal_z_position)
        {
            current_z_position = Mathf.Lerp(current_z_position, goal_z_position, openPower);
            if (count > endMove)

                current_z_position = goal_z_position;
            m_Male.transform.localPosition = new Vector3(m_Male.transform.localPosition.x, m_Male.transform.localPosition.y, current_z_position);
            //
            yield return null;
            count++;
        }
    }
    */
}

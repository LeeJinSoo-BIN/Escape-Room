using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Rigidbody를 변경하여 이동하는방법
public class PlayerMovement : MonoBehaviour
{

    public float turnSpeed = 20f;
    public float moveSpeed = 0.1f;
    Vector3 m_Movement;
    Rigidbody m_Rigidbody;
    Quaternion m_Rotation = Quaternion.identity;

    void Start(){
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate() {
       
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //Debug.Log(horizontal);
        //Debug.Log(vertical);
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward,
           m_Movement,
           turnSpeed * Time.deltaTime,
           0f);        
        m_Rotation = Quaternion.LookRotation(desiredForward);

        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * moveSpeed);
        //m_Rigidbody.MoveRotation(m_Rotation);
    }

    /*void OnAnimatorMove()
    {
        
        Debug.Log("moved");
    }*/
}

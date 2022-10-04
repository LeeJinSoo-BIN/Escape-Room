using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//transform을 변경하여 이동하는 방법
public class PlayerMovement_2 : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float turnSpeed = 6f;    
    public float runWeight = 2f;
    public float jumpPower = 500f;    
    public bool m_Jumping = false;
    private Vector3 m_Movement;

    public Camera m_Camera;

    private GameObject m_ClickedTarget;
    private GameObject m_WatchingTarget;
    private Vector3 ScreenCenter;

    private GameObject m_LeftSharingan;
    private GameObject m_RightSharingan;
    public bool m_ActivingLeftSharingan = false;
    public bool m_ActivingRightSharingan = false;
    public float currentFoV;
    private float currentCamerXRotation = 0f;
    private bool haveItem = false;
    private bool watchingItem = false;
    private GameObject m_HavingItem;
    void Start()
    {
        m_LeftSharingan = transform.Find("eyes").Find("left").gameObject;
        m_RightSharingan = transform.Find("eyes").Find("right").gameObject;
        ScreenCenter = new Vector3(m_Camera.pixelWidth / 2, m_Camera.pixelHeight / 2);
        currentFoV = m_Camera.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {

        Move();
        SitJump();

        View();
        

        Skill();
        GetClickedTarget();

    }
    void GetClickedTarget()
    {

        RaycastHit hit;
        Ray ray = m_Camera.ScreenPointToRay(ScreenCenter);

        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            m_WatchingTarget = hit.collider.gameObject;
        }
        if (Input.GetMouseButtonUp(0))
        {
            m_ClickedTarget = m_WatchingTarget;
            Debug.Log(m_ClickedTarget);



            if (m_ClickedTarget.transform.tag == "DigitalLocker")
            {

                string clicked_num = m_ClickedTarget.transform.name;
                m_ClickedTarget.transform.parent.gameObject.GetComponent<digitallocker>().ClickButton(clicked_num);

            }
            else if (m_ClickedTarget.transform.tag == "KeyLock")
            {
                if (haveItem)
                {
                    Debug.Log("matching key");
                    if (m_ClickedTarget.transform.root.tag == "Drawer")
                    {
                        if (m_HavingItem.transform.GetComponent<item>().keyCode ==
                         m_ClickedTarget.transform.root.gameObject.GetComponent<drawer>().keyCode)
                        {
                            m_ClickedTarget.transform.root.gameObject.GetComponent<Animator>().SetBool("isLocked", false);
                        }
                    }
                    else if (m_ClickedTarget.transform.root.tag == "Closet")
                    {
                        if (m_HavingItem.transform.GetComponent<item>().keyCode ==
                         m_ClickedTarget.transform.root.gameObject.GetComponent<closet>().keyCode)
                        {
                            m_ClickedTarget.transform.root.gameObject.GetComponent<Animator>().SetBool("isLocked", false);
                        }
                    }

                    else if (m_ClickedTarget.transform.root.tag == "Safe")
                    {
                        if (m_HavingItem.transform.GetComponent<item>().keyCode ==
                         m_ClickedTarget.transform.root.gameObject.GetComponent<safe>().keyCode)
                        {
                            m_ClickedTarget.transform.root.gameObject.GetComponent<Animator>().SetBool("isLocked", false);
                        }
                    }

                    else if (m_ClickedTarget.transform.root.tag == "Door")
                    {
                        if (m_HavingItem.transform.GetComponent<item>().keyCode ==
                         m_ClickedTarget.transform.root.gameObject.GetComponent<door>().keyCode)
                        {
                            m_ClickedTarget.transform.root.gameObject.GetComponent<Animator>().SetBool("isLocked", false);
                        }
                    }
                }
            }
            else if (m_ClickedTarget.transform.tag == "ClosetHandle" || m_ClickedTarget.transform.tag == "SafeHandle" ||
                m_ClickedTarget.transform.tag == "DrawerHandle" || m_ClickedTarget.transform.tag == "DoorHandle")
            {
                m_ClickedTarget = m_ClickedTarget.transform.root.gameObject;
                m_ClickedTarget.GetComponent<Animator>().SetBool("isOpen", !m_ClickedTarget.GetComponent<Animator>().GetBool("isOpen"));
            }
            else if (m_ClickedTarget.transform.tag == "Item")
            {
                if (!haveItem)
                {   
                    if (m_ClickedTarget.transform.root.tag != "Item")
                    {
                        Debug.Log(m_ClickedTarget.transform.root.tag);
                        while (true)
                        {
                            if (m_ClickedTarget.transform.parent.tag == "Item")
                                m_ClickedTarget = m_ClickedTarget.transform.parent.gameObject;
                            else
                            {
                                m_ClickedTarget.transform.parent = null;
                                break;
                            }
                                
                            
                        }
                    }
                    m_ClickedTarget = m_ClickedTarget.transform.root.gameObject;
                    m_ClickedTarget.transform.SetParent(transform);
                    m_ClickedTarget.transform.localPosition = new Vector3(0f, -0.5f, 1.2f);
                    m_ClickedTarget.transform.localEulerAngles = new Vector3(-40f, 0, 0);
                    m_ClickedTarget.GetComponent<Rigidbody>().isKinematic = true;
                    haveItem = !haveItem;
                    m_HavingItem = m_ClickedTarget;
                }
            }
            else if (m_ClickedTarget.transform.tag == "NumberLockerRing")
            {
                string clicked_num = m_ClickedTarget.transform.name;
                m_ClickedTarget.transform.parent.parent.gameObject.GetComponent<numberlocker>().ClickRing(clicked_num);
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            if (haveItem && !watchingItem) 
            {
                transform.GetChild(transform.childCount - 1).GetComponent<Rigidbody>().isKinematic = false;
                transform.GetChild(transform.childCount - 1).parent = null;
                haveItem = !haveItem;
                m_HavingItem = null;
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (haveItem)
            {
                watchingItem = !watchingItem;
                if (watchingItem) {
                    
                    //transform.localEulerAngles = new Vector3(0, 0, 0);
                    m_Camera.transform.localEulerAngles = new Vector3(0, 0f, 0f);
                    transform.GetChild(transform.childCount - 1).localPosition = new Vector3(0f, 0, 1.5f);
                    m_Camera.transform.GetChild(0).transform.localScale = new Vector3(0, 0, 0);
                    StartCoroutine(RotateItem(transform.GetChild(transform.childCount - 1).gameObject));
                    
                    
                }
                else
                {
                    transform.GetChild(transform.childCount - 1).localPosition = new Vector3(0f, -0.5f, 1.2f);
                    transform.GetChild(transform.childCount - 1).localEulerAngles = new Vector3(-40f, 0, 0);
                    m_Camera.fieldOfView = currentFoV;
                    m_Camera.transform.GetChild(0).transform.localScale = new Vector3(1, 1, 1);
                }
            }
        }
    }

    IEnumerator RotateItem(GameObject item)
    {

        while (watchingItem)
        {
            float mouse_horizontal = Input.GetAxisRaw("Mouse Y");
            float mouse_vertical = Input.GetAxisRaw("Mouse X");
            float scroll = Input.GetAxis("Mouse ScrollWheel") * turnSpeed;

            

            transform.GetChild(transform.childCount - 1).transform.localRotation *= Quaternion.Euler(-mouse_horizontal * turnSpeed, 0,mouse_vertical * turnSpeed);

            m_Camera.fieldOfView = Mathf.Clamp(m_Camera.fieldOfView + scroll, 30, currentFoV);

            yield return null;
        }
        
    }


    void Skill()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            m_ActivingLeftSharingan = !m_ActivingLeftSharingan;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            m_ActivingRightSharingan = !m_ActivingRightSharingan;
        }
        if (m_ActivingLeftSharingan)
        {
            m_LeftSharingan.transform.localRotation *= Quaternion.Euler(0, 0, 0.8f);
        }
        if (m_ActivingRightSharingan)
        {
            m_RightSharingan.transform.localRotation *= Quaternion.Euler(0, 0, 0.8f);
        }
    }

    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            m_Movement *= runWeight;
        }
        transform.Translate(m_Movement * moveSpeed * Time.deltaTime);
        
    }

    void SitJump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!m_Jumping)
            {
                //transform.Translate((transform.position + new Vector3(0, 1 * jumpPower, 0)) * Time.deltaTime);
                GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                m_Jumping = true;
                
            }
            
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Vector3 m_sit = new Vector3(0, -1f, 0);
            m_Camera.transform.position += m_sit;
            moveSpeed /= 2;
            
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Vector3 m_stand = new Vector3(0, 1f, 0);
            m_Camera.transform.position += m_stand;
            moveSpeed *= 2;
        }
    }
    void View()
    {
        if (!watchingItem)
        {
            float mouse_horizontal = Input.GetAxisRaw("Mouse Y");
            float mouse_vertical = Input.GetAxisRaw("Mouse X");

            transform.localRotation *= Quaternion.Euler(0, mouse_vertical * turnSpeed, 0);

            currentCamerXRotation += -mouse_horizontal * turnSpeed;
            currentCamerXRotation = Mathf.Clamp(currentCamerXRotation, -90, 90);

            //m_Camera.transform.localRotation *= Quaternion.Euler(-mouse_horizontal * turnSpeed * Time.deltaTime, 0, 0);
            m_Camera.transform.localEulerAngles = new Vector3(currentCamerXRotation, 0f, 0f);
        }

        
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {            
            m_Jumping = false;
        }
    }
}

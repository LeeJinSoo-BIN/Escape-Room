using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class numberlocker : MonoBehaviour
{
    public int currentNum = 0;
    public int password = 12345;
    public void ClickRing(string clickedRing)
    {
        int n;
        if( int.TryParse(clickedRing, out n))
        {
            if (currentNum % (n * 10) / n == 9)
            {
                currentNum -= (currentNum % (n * 10) / n) * n;
            }
            else
            {
                currentNum += n;
            }
        }
        Debug.Log(n);
        
        if (clickedRing == "10000")
        {
            transform.Find("number_ring1").Rotate(-36f, 0, 0);
        }
        else if (clickedRing == "1000")
        {
            transform.Find("number_ring2").Rotate(-36f, 0, 0);
        }
        else if (clickedRing == "100")
        {
            transform.Find("number_ring3").Rotate(-36f, 0, 0);
        }
        else if (clickedRing == "10")
        {
            transform.Find("number_ring4").Rotate(-36f, 0, 0);
        }
        else if (clickedRing == "1")
        {
            transform.Find("number_ring5").Rotate(-36f, 0, 0);
        }
        else if (clickedRing == "enter")
        {
            Debug.Log("??");
            if (currentNum == password)
            {
                transform.Find("Torus").localPosition = new Vector3(14, 22, 0);
                gameObject.AddComponent<Rigidbody>();
                transform.root.GetComponent<Animator>().SetBool("isLocked", false);
                Destroy(this);
            }
        }
    }



}

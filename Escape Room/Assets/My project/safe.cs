using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class safe : MonoBehaviour
{
    public int keyCode;
    public Animator anim;
    public void Unlock()
    {
        anim.SetBool("isClosed", false);
    }
}

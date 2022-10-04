using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class digitallocker : MonoBehaviour
{

    public string password = "1234";
    private string currentString = "";
    public TextMeshPro textmesh;
    
   
  
    void InsertSpace()
    {
        int start = 0;
        
        while (true)
        {
            
            int index = currentString.IndexOf(" ", start);
            if (index == -1 || start > 8) break;
            currentString = currentString.Insert(index, "0");
            start = index+2;
        }
    }

    public void ClickButton(string clickedButton)
    {
        int n;
        if (clickedButton == "clear")
        {
            
            if (currentString.Length >0)
                currentString = currentString.Remove(currentString.Length - 1);
        }
        else if (clickedButton == "enter")
        {
            if (currentString == password)
            {
                
                currentString = "OPEN";

                
                transform.root.GetComponent<Animator>().SetBool("isLocked", false);
                Destroy(this);
            }
            else
            {
                Debug.Log(currentString);
                Debug.Log(password);
                Debug.Log("fail");
            }
        }
        else if (int.TryParse(clickedButton, out n))
        {
            
            if (currentString.Length < 4)
            {
                currentString = currentString.Insert(currentString.Length, clickedButton);
                
            }
        }
        textmesh.text = currentString;

    }

    void OnDestroy()
    {
        textmesh.text = currentString;

    }
}

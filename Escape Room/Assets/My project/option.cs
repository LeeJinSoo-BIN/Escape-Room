using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class option : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider cameraSpeedSlider;
    public GameObject Character;
    public GameObject OptionPanel;
    public float cameraSpeed  = 0.5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OptionPanel.SetActive(!OptionPanel.activeSelf);
        }
        if (OptionPanel.activeSelf)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void ClickResumeButton()
    {
        OptionPanel.SetActive(false);
    }

    public void ClickExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }

    public void SetCameraSpeed()
    {
        Character.GetComponent<PlayerMovement_2>().turnSpeed = cameraSpeedSlider.value * cameraSpeed;
    }


}

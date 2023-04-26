using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : Screen
{
    [SerializeField] private Camera _camera;

    private bool _isPause = false;

    private void Start()
    {
        CanvasGroup.alpha= 0f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _isPause == false)
        {
            Open();
        }
        else if ((Input.GetKeyDown(KeyCode.Escape) && _isPause == true))
        {
            Close();
        }
    }

    protected void OnButtonClick()
    {
        if(_isPause == false)
        {
            Open();
        }
        else if(_isPause)
        {
            Close();
        }
    }

    public override void Open()
    {
        _camera.GetComponent<FirstPersonLook>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        CanvasGroup.alpha = 1f;
        _isPause = true;
        Time.timeScale = 0f;
        AudioListener.volume = 0;
    }

    public override void Close()
    {
        _camera.GetComponent<FirstPersonLook>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        CanvasGroup.alpha = 0f;
        _isPause = false;
        Time.timeScale = 1f;
        AudioListener.volume = 1;
    }
}
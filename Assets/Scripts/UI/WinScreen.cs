using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : Screen
{
    [SerializeField] private Camera _camera;

    private void Start()
    {
        CanvasGroup.alpha = 0f;
    }

    public override void Close()
    {
        _camera.GetComponent<FirstPersonLook>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        CanvasGroup.alpha = 1f;
        Time.timeScale = 0f;
    }

    public override void Open()
    {
        _camera.GetComponent<FirstPersonLook>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        CanvasGroup.alpha = 0f;
        Time.timeScale = 1f;
    }
}

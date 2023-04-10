using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : Screen
{
    [SerializeField] private Camera _camera;

    private const string _mainScene = "SampleScene";
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

    protected override void OnButtonClick()
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
    }

    public override void Close()
    {
        _camera.GetComponent<FirstPersonLook>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        CanvasGroup.alpha = 0f;
        _isPause = false;
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(_mainScene);
        Close();
    }
}
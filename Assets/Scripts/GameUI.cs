using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [SerializeField] private string _scene;

    private const string _mainMenuScene = "MenuScene"; 

    public void Restart()
    {
        SceneManager.LoadScene(_scene);
        Time.timeScale = 1f;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(_mainMenuScene);
        Time.timeScale = 1f;
    }
}

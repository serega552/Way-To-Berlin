using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using IJunior.TypedScenes;

public class Game : MonoBehaviour
{
    [SerializeField] private bool _isMobile;

    public bool IsMobile => _isMobile;

    private Scene _scene;
    private GameUI _gameUI;

    private void Start()
    {
        _gameUI = GetComponentInChildren<GameUI>();
    }

    private void Awake()
    {
        _scene = SceneManager.GetActiveScene();
    }

    public void Restart()
    {
        SceneManager.LoadScene(_scene.name);
        Time.timeScale = 1f;
    }

    public void LoadMenu()
    {
        MenuScene.Load();
        Time.timeScale = 1f;
    }

    public void WinLevel()
    {
        _gameUI.Win();
    }
}

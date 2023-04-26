using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    
    public void LoadLevel()
    {
        SceneManager.LoadScene(_sceneName);
    }
}

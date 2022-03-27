using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance { get; private set; }

    [SerializeField] private int _levelsCount = 2;

    private int _sceneIndex
    {
        get => PlayerPrefs.GetInt(nameof(_sceneIndex)) % _levelsCount;
        set => PlayerPrefs.SetInt(nameof(_sceneIndex), value);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(1 + _sceneIndex);
    }

    public void LoadNextLevel()
    {
        _sceneIndex++;
        SceneManager.LoadScene(1 + _sceneIndex);
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        RestartLevel();
    }
}
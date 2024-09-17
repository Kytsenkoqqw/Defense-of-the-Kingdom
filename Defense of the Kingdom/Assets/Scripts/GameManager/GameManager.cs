using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.WSA;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _restart;
    
    
    [SerializeField] private GameObject _pauseMenu;
    private bool _IsPaused = false;

    private void Start()
    {
        _pauseButton.onClick.AddListener(Pause);
        _resumeButton.onClick.AddListener(Resume);
        _restart.onClick.AddListener(RestartLevel);
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void TogglePauseMenu()
    {
        if (_IsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        _IsPaused = true;
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        _IsPaused = false;
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ResumeButton()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
        _pauseMenu.SetActive(false);
    }
}

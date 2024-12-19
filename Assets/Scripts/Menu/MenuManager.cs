using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MenuManager : MonoBehaviour
{
    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject creditsMenu;
    public GameObject audioMenu;
    public GameObject pauseMenu;
    public GameObject hud;
    public GameObject winMenu;
    
    private GameObject _player;
    private OpeningAnimation _openingAnimation;

    public bool winMenuAvailable = true;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _openingAnimation = _player.GetComponent<OpeningAnimation>();
    }
    
    public void PlayGame()
    {
        _openingAnimation.StartAnimation();
        
        Invoke("LoadGameScene", 2);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
        
        Time.timeScale = 1f;
    }

    public void LoadTitleScene()
    {
        SceneManager.LoadScene(0);
        
        Time.timeScale = 1f;
    }

    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
    }

    public void ShowCreditsMenu()
    {
        Time.timeScale = 0f;
        
        mainMenu.SetActive(false);
        audioMenu.SetActive(false);
        
        creditsMenu.SetActive(true);
    }
    
    public void ShowAudioMenu()
    {
        Time.timeScale = 0f;
        
        mainMenu.SetActive(false);
        creditsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        hud.SetActive(false);
        
        audioMenu.SetActive(true);
    }
    
    public void ShowPauseMenu()
    {
        Time.timeScale = 0f;
        
        hud.SetActive(false);
        
        pauseMenu.SetActive(true);
    }

    public void ShowHud()
    {
        hud.SetActive(true);
        
        Time.timeScale = 1f;
    }
    
    public void HideCreditsMenu()
    {
        creditsMenu.SetActive(false);
        
        Time.timeScale = 1f;
    }
    
    public void HideAudioMenu()
    {
        audioMenu.SetActive(false);
        
        Time.timeScale = 1f;
    }
    
    public void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
        
        Time.timeScale = 1f;
        
        hud.SetActive(true);
    }

    public void ShowWinMenu()
    {
        Time.timeScale = 0f;
        
        hud.SetActive(false);
        
        winMenu.SetActive(true);
    }

    public void HideWinMenu()
    {
        winMenuAvailable = false;
        
        winMenu.SetActive(false);
        
        Time.timeScale = 1f;
        
        hud.SetActive(true);
    }
}

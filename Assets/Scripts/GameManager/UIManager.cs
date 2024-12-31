using System;
using UnityEngine;
using TMPro;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    private MenuManager _menuManager;
    
    private TimeController _timeController;
    
    private AudioManager _audioManager;
    
    [SerializeField] private TextMeshProUGUI _textTravelDistance;
    
    [SerializeField] private TextMeshProUGUI _textCollectableCount;
    
    [SerializeField] private TextMeshProUGUI _textDestructibleCount;

    [SerializeField] private Transform _player;
    
    private Vector3 _startPosition;
    
    private Vector3 _endPosition;
    
    public float travelDistance;
    
    public bool winMenuAvailable = true;

    [SerializeField] private float winningDistance = 10000f;
    
    public Slider musicSlider, sfxSlider;
    
    public Button masterAudioBtn;
    
    public Image buttonImage;
    
    public Sprite[] buttonSprites;
    
    private void Awake()
    {   
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _endPosition = _player.position;
        
        _menuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();
        
        _timeController = GameObject.Find("TimeController").GetComponent<TimeController>();
        
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        
        _timeController.StartTimer();
    }

    private void FixedUpdate()
    {
        travelDistance += Vector3.Distance(_player.position, _endPosition);
        _endPosition = _player.position;
        
        UpdateTextDistance();
        
        WinCondition();
    }

    private void WinCondition()
    {
        if (travelDistance >= winningDistance && _menuManager.winMenuAvailable)
        {
            _timeController.StopTimer();

            _timeController.UpdateTextPlayTime();
            
            _menuManager.ShowWinMenu();
            
            _audioManager.Play("Win");
        }
    }
    
    private void UpdateTextDistance()
    {
        // float travelDistance = 0f;
        
        // travelDistance += Vector3.Distance(_player.position, _endPosition);
        
        _textTravelDistance.text = travelDistance.ToString("0") + " m";

        // _textTravelDistance.text = (_player.position.magnitude).ToString("0") + " m";
        // if (_player.position.magnitude < 0f) _textTravelDistance.text = "0 m";
    }

    public void UpdateTextCollectable(int currentAmount)
    {
        _textCollectableCount.text = currentAmount.ToString();
    }
    
    public void UpdateTextDestructible(int currentAmount)
    {
        _textDestructibleCount.text = currentAmount.ToString();
    }
    
    public void ToggleMasterAudio()
    {
        UpdateBtnMasterAudio();
        
        AudioManager.instance.ToggleMasterAudio();
    }
    
    public void ToggleMusic()
    {
        AudioManager.instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        AudioManager.instance.ToggleSFX();
    }

    public void ToggleMusicVolume()
    {
        AudioManager.instance.ToggleMusicVolume(musicSlider.value);
    }

    public void ToggleSFXVolume()
    {
        AudioManager.instance.ToggleSFXVolume(sfxSlider.value);
    }

    public void UpdateBtnMasterAudio()
    {
        if (buttonImage.sprite == buttonSprites[0])
        {
            buttonImage.sprite = buttonSprites[1];
            buttonImage.color = Color.grey;
            return;
        }

        buttonImage.sprite = buttonSprites[0];
        buttonImage.color = Color.white;
    }
}

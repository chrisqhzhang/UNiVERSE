using System;
using UnityEngine;
using TMPro;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;
    
    private MenuManager _menuManager;
    
    private TimeController _timeController;
    
    [SerializeField] private TextMeshProUGUI _textTravelDistance;
    
    [SerializeField] private TextMeshProUGUI _textCollectableCount;
    
    [SerializeField] private TextMeshProUGUI _textDestructibleCount;

    [SerializeField] private Transform _player;
    
    private Vector3 _startPosition;
    
    private Vector3 _endPosition;
    
    public float travelDistance;
    
    public bool winMenuAvailable = true;

    [SerializeField] private float winningDistance = 10000f;
    
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
}

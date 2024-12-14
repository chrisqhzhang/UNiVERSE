using System;
using UnityEngine;
using TMPro;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;
    
    [SerializeField] private TextMeshProUGUI _textTravelDistance;
    
    [SerializeField] private TextMeshProUGUI _textCollectableCount;
    
    [SerializeField] private TextMeshProUGUI _textDestructibleCount;

    [SerializeField] private Transform _player;
    
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

    private void Update()
    {
        UpdateTextDistance();
    }
    
    public void UpdateTextDistance()
    { 
        _textTravelDistance.text = (_player.position.magnitude).ToString("0") + " m";
        if (_player.position.magnitude < 0f) _textTravelDistance.text = "0 m";
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

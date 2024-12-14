using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TimeManager : MonoBehaviour
{
    [Header("Time")]
    [Tooltip("Day length in minutes")]
    // Length of day in minutes
    [SerializeField] private float _targetDayLength = 0.5f;
    public float targetDayLength { get { return _targetDayLength; } }
    
    [SerializeField] [Range(0.0f, 1.0f)] private float _timeOfDay;
    public float timeOfDay { get { return _timeOfDay; } }
    

    [SerializeField] private int _dayNumber = 0;
    public int dayNumber { get { return _dayNumber; } }
    
    [SerializeField] private int _yearNumber = 0;
    public int yearNumber { get {return _yearNumber;} } 
    
    [SerializeField] private int _yearLength = 100;
    public int yearLength { get {return _yearLength;} }
    
    public bool pause = false;
    
    private float _timeScale = 100.0f; 
    
    [Header("Sun Light")]
    [SerializeField] private Transform _dailyRotation;
    
    [SerializeField] private Light _sun;
    
    private float _intensity;
    
    [SerializeField] private float _sunBaseIntensity = 100.0f;

    [SerializeField] private float _sunVariation = 1.5f;

    [SerializeField] private Gradient _sunColor;


    [Header("Seasonal Variables")] 
    [SerializeField] private Transform _sunSeasonalRotation;
    [SerializeField] [Range(-45.0f, 45.0f)] private float _maxSeasonalTilt;
    
    [Header("Modules")]
    private List<TimeModuleBase> moduleList = new List<TimeModuleBase>();
    
    
    
    // Update is called once per frame
    private void Update()
    {
        if (!pause)
        {
            UpdateTimeScale();
            UpdateTime();
        }
        
        AdjustSunRotation();
        SunIntentsity();
        AdjustSunColor();
        UpdateModules();
    }
    
    private void UpdateTimeScale()
    {
        _timeScale = 24 / (_targetDayLength / 60);
    }

    private void UpdateTime()
    {
        _timeOfDay += Time.deltaTime * _timeScale / 86400;
        if (_timeOfDay > 1)
        {
            _dayNumber++;
            _timeOfDay -= 1;

            if (_dayNumber > _yearLength)
            {
                _yearNumber++;
                _dayNumber = 0;
            }
        }
    }

    private void AdjustSunRotation()
    {
        float sunAngle = -timeOfDay * 360.0f;
        _dailyRotation.transform.localRotation = Quaternion.Euler(new Vector3(sunAngle, 0.0f, 0.0f));
        
        float seasonalAngle = -_maxSeasonalTilt * Mathf.Cos(dayNumber / yearLength * 2.0f * Mathf.PI);
        _sunSeasonalRotation.localRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, seasonalAngle));
        
    }

    private void SunIntentsity()
    {
        _intensity = Vector3.Dot(_sun.transform.forward, Vector3.down);
        _intensity = Mathf.Clamp01(_intensity);

        _sun.intensity = _intensity * _sunBaseIntensity * _sunVariation;
    }

    private void AdjustSunColor()
    {
        _sun.color = _sunColor.Evaluate(_intensity);
    }

    public void AddModule(TimeModuleBase module)
    {
        moduleList.Add(module);
    }

    public void RemoveModule(TimeModuleBase module)
    {
        moduleList.Remove(module);
    }

    private void UpdateModules()
    {
        foreach (TimeModuleBase module in moduleList)
        {
            module.UpdateModule(_intensity);
            module.UpdateSkybox(_timeOfDay);
        }
    }
    
}

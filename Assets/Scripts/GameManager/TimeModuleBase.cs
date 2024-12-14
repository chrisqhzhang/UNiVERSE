using System;
using UnityEngine;

public abstract class TimeModuleBase : MonoBehaviour
{
    protected TimeManager TimeController;

    private void OnEnable()
    {
        TimeController = this.GetComponent<TimeManager>();
        if(TimeController != null) TimeController.AddModule(this);
    }

    private void OnDisable()
    {
        if(TimeController != null) TimeController.RemoveModule(this);
    }
    
    public abstract void UpdateModule(float intensity);

    public abstract void UpdateSkybox(float timeOfDay);
}

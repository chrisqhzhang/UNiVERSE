using UnityEngine;

public class SkyboxModule : TimeModuleBase
{
    [SerializeField] private Gradient _skyColor;
    [SerializeField] private Gradient _horizonColor;
    
    public override void UpdateModule(float intensity)
    {
        RenderSettings.skybox.SetColor("_skyTint", _skyColor.Evaluate(intensity));
        RenderSettings.skybox.SetColor("_GroundColor", _horizonColor.Evaluate(intensity));
    }

    public override void UpdateSkybox(float timeOfDay)
    {
        RenderSettings.skybox.SetFloat("_DayTime", timeOfDay);
        // RenderSettings.skybox.SetFloat("_StarSize", 6.0f - (timeOfDay * 3.0f));
        RenderSettings.skybox.SetFloat("_StarIntencity", 10.5f - (timeOfDay * 10.0f));
    }
}

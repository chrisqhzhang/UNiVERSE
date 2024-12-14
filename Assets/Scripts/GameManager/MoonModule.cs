using UnityEngine;

public class MoonModule : TimeModuleBase
{
    [SerializeField] private Light _moon;
    [SerializeField] private Gradient _moonColor;
    [SerializeField] private float _baseIntensity;
    public override void UpdateModule(float intensity)
    {
        _moon.color = _moonColor.Evaluate(1 - intensity);
        _moon.intensity = (1 - intensity) * _baseIntensity + 0.05f;
    }

    public override void UpdateSkybox(float timeOfDay)
    {
        return;
    }
}

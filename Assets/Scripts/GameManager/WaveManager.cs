using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    public float amplitude = 1.0f;

    public float length = 2.0f;

    public float speed = 1.0f;

    public float offset = 0.0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance !=this) Destroy(this);
    }

    // Update is called once per frame
    private void Update()
    {
        offset += Time.deltaTime * speed;
    }

    public float GetWaveHeight(float x)
    {
        return amplitude * Mathf.Sin(x / length + offset);
    }
}

using UnityEngine;

public class Floatable : MonoBehaviour
{
    public Rigidbody rb;
    
    [SerializeField] private float _depthBeforeSubmerged = 0.5f;
    
    [SerializeField] private float _displacementAmount = 1.0f;

    [SerializeField] private int _floaters = 300;

    [SerializeField] private float _waveDrag = 0.99f;

    [SerializeField] private float _waveAngularDrag = 0.5f;
    
    private Vector3 _gravity = new Vector3(0, -9.81f, 0);
    
    [SerializeField] private Transform _rotation;

    private TimeManager _timeManager;
    
    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody>();
    }

    private void FixedUpdate()
    {
        FloatObject();
    }
    
    private void FloatObject()
    {
        rb.AddForceAtPosition(_gravity / _floaters, transform.position, ForceMode.Acceleration);
        
        // float waveHeight = WaveManager.instance.GetWaveHeight(transform.position.x);
        
        // if (transform.position.y < waveHeight) {}
        float displacementMultiplier =
            Mathf.Clamp01(-transform.position.y / _depthBeforeSubmerged) * _displacementAmount;
        rb.AddForceAtPosition(new Vector3(0.0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0.0f),
            transform.position, ForceMode.Acceleration);
        rb.AddForce(displacementMultiplier * rb.linearVelocity * _waveDrag * Time.fixedDeltaTime,
            ForceMode.VelocityChange);
        rb.AddTorque(displacementMultiplier * rb.linearVelocity * _waveAngularDrag * Time.fixedDeltaTime,
            ForceMode.VelocityChange);
        
        // _rotation.transform.localRotation = Quaternion.Euler(new Vector3(0f, _timeManager.timeOfDay * 360f, 0f));
    }
}

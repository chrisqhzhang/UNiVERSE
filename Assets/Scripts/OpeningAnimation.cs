using UnityEngine;

public class OpeningAnimation : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        _animator = gameObject.GetComponent<Animator>();
    }
    
    
    public void StartAnimation()
    {
        _animator.Play("Float");
        
        _rb.AddForce(transform.up * 2.5f, ForceMode.Impulse);
        _rb.AddTorque(Vector3.up * 100f);
    }
}

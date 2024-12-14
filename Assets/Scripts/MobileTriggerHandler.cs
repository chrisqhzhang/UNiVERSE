using UnityEngine;

public class MobileTriggerHandler : MonoBehaviour
{
    [SerializeField] private LayerMask _whoCanCollect = LayerMaskHelper.CreateLayerMask(6);
    
    private Mobile _mobile;
    
    private void Awake()
    {
        _mobile = GetComponent<Mobile>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (LayerMaskHelper.ObjectIsInLayerMask(other.gameObject, _whoCanCollect))
        {
            _mobile.Move(other.gameObject);
        }
    }
}

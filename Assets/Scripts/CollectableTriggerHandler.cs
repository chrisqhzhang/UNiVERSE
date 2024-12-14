using System;
using UnityEngine;

public class CollectableTriggerHandler : MonoBehaviour
{
    [SerializeField] private LayerMask _whoCanCollect = LayerMaskHelper.CreateLayerMask(6);
    
    private Collectable _collectable;
    
    private void Awake()
    {
        _collectable = GetComponent<Collectable>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (LayerMaskHelper.ObjectIsInLayerMask(other.gameObject, _whoCanCollect))
        {
            _collectable.Collect(other.gameObject);
            
            Destroy(gameObject);
        }
    }
}

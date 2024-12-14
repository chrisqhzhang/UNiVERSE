using UnityEngine;

public class DestructibleTriggerHandler : MonoBehaviour
{
    [SerializeField] private LayerMask _whoCanCollect = LayerMaskHelper.CreateLayerMask(6);
    
    private Destructible _destructible;
    
    private void Awake()
    {
        _destructible = GetComponent<Destructible>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (LayerMaskHelper.ObjectIsInLayerMask(other.gameObject, _whoCanCollect))
        {
            _destructible.Destruct(other.gameObject);
            
            Destroy(gameObject);
        }
    }
}

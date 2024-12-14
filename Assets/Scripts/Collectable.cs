using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CollectableTriggerHandler))]

public class Collectable : MonoBehaviour
{
    [SerializeField] private CollectableSOBase _collectable;
    
    private void Reset()
    {
        this.tag = "Collectable";
        GetComponent<MeshCollider>().convex = true;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().linearDamping = 0.01f;
        
        // GetComponent<Collectable>()._collectable = Resources.Load<CollectableCurrencySO>(Assets/Data/1TrashBag.asset);
    }
    public void Collect(GameObject objectCollected)
    {
        _collectable.Collect(objectCollected);
    }
}

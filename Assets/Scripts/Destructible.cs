using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(DestructibleTriggerHandler))]

public class Destructible : MonoBehaviour
{
    [SerializeField] private DestructibleSOBase _destructible;
    
    private void Reset()
    {
        this.tag = "Destructible";
        GetComponent<MeshCollider>().convex = true;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().linearDamping = 0.01f;
    }
    public void Destruct(GameObject objectDestructed)
    {
        _destructible.Destruct(objectDestructed);
    }
}

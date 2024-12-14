using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MobileTriggerHandler))]

public class Mobile : MonoBehaviour
{
    [SerializeField] private MobileSOBase _mobile;
    
    private void Reset()
    {
        this.tag = "Mobile";
        GetComponent<MeshCollider>().convex = true;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().linearDamping = 0.01f;
    }
    public void Move(GameObject objectMoved)
    {
        _mobile.Move(objectMoved);
    }
}

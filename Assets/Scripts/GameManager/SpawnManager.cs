using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

// [ExecuteInEditMode]
public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    
    private Collectable _collectable;
    
    private Destructible _destructible;
    
    private Mobile _mobile;

    // Planet, Cloud, Sun, Moon, Rock, Flower, Mushroom, Log, Vehicle, People, Trash bag, etc
    [SerializeField] private GameObject[] collectables;
    
    [SerializeField] private GameObject[] destructibles;
    
    [SerializeField] private GameObject[] mobiles;
    
    [SerializeField] private GameObject[] immobiles;

    [SerializeField] private List<GameObject> instances;
    
    private void Awake()
    {
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        foreach (GameObject go in collectables)
        {
            PoissonDiscSampler3D sampler = new PoissonDiscSampler3D(20f, 20f, 80f, 4.75f);
            
            foreach (Vector3 sample in sampler.Samples())
            {
                GameObject ins = Instantiate(go, new Vector3(sample.x, sample.y, sample.z), Quaternion.identity);
                ins.tag = "Collectable";
                instances.Add(ins);
            }
        }
        
        foreach (GameObject go in destructibles)
        {
            PoissonDiscSampler3D sampler = new PoissonDiscSampler3D(20f, 20f, 80f, 4.75f);
            
            foreach (Vector3 sample in sampler.Samples())
            {
                GameObject ins = Instantiate(go, new Vector3(sample.x, sample.y, sample.z), Quaternion.identity);
                ins.tag = "Destructible";
                instances.Add(ins);
            }
        }
        
        foreach (GameObject go in mobiles)
        {
            PoissonDiscSampler3D sampler = new PoissonDiscSampler3D(20f, 20f, 80f, 4.75f);
            
            foreach (Vector3 sample in sampler.Samples())
            {
                GameObject ins = Instantiate(go, new Vector3(sample.x, sample.y, sample.z), Quaternion.identity);
                ins.tag = "Mobile";
                instances.Add(ins);
            }
        }
        
        foreach (GameObject go in immobiles)
        {
            PoissonDiscSampler3D sampler = new PoissonDiscSampler3D(20f, 20f, 80f, 4.75f);
            
            foreach (Vector3 sample in sampler.Samples())
            {
                GameObject ins = Instantiate(go, new Vector3(sample.x, sample.y, sample.z), Quaternion.identity);
                ins.tag = "Immobile";
                instances.Add(ins);
            }
        }
        
        foreach (GameObject go in instances)
        {
            ChildrenFinder(go);
        }
    }

    private void ChildrenFinder(GameObject gameObject)
    {
        List<GameObject> childrenList = new List<GameObject>();
        
        Transform[] children = gameObject.GetComponentsInChildren<Transform>(true);
        
        foreach (Transform child in children)
        {
            bool component = child.gameObject.GetComponent<MeshRenderer>();
            
            if (component)
            {
                childrenList.Add(child.gameObject);
            }
        }

        foreach (GameObject child in childrenList)
        {
            switch (child.tag)
            {
                case "Collectable":
                    child.AddComponent<MeshCollider>();
                    child.GetComponent<MeshCollider>().convex = true;
            
                    child.AddComponent<Rigidbody>();
                    child.GetComponent<Rigidbody>().useGravity = false;
                    child.GetComponent<Rigidbody>().linearDamping = 0.01f;
            
                    child.AddComponent<Floatable>();
                    
                    child.AddComponent<Collectable>();
                    
                    break;
                
                case "Destructible":
                    child.AddComponent<MeshCollider>();
                    child.GetComponent<MeshCollider>().convex = true;
            
                    child.AddComponent<Rigidbody>();
                    child.GetComponent<Rigidbody>().useGravity = false;
                    child.GetComponent<Rigidbody>().linearDamping = 0.01f;
            
                    child.AddComponent<Floatable>();
                    
                    child.AddComponent<Destructible>();
                    
                    break;
                
                case "Mobile":
                    child.AddComponent<MeshCollider>();
                    child.GetComponent<MeshCollider>().convex = true;
            
                    child.AddComponent<Rigidbody>();
                    child.GetComponent<Rigidbody>().useGravity = false;
                    child.GetComponent<Rigidbody>().linearDamping = 0.01f;
            
                    child.AddComponent<Floatable>();
                    
                    child.AddComponent<Mobile>();
                    
                    break;
                
                case "Immobile":
                    child.AddComponent<MeshCollider>();
                    child.GetComponent<MeshCollider>().convex = true;
            
                    child.AddComponent<Rigidbody>();
                    child.GetComponent<Rigidbody>().useGravity = false;
                    child.GetComponent<Rigidbody>().linearDamping = 0.01f;
            
                    child.AddComponent<Floatable>();
                    
                    break;
            }
        }
    }
}

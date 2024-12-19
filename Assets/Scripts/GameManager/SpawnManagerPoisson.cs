using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

// [ExecuteInEditMode]
public class SpawnManagerPoisson : MonoBehaviour
{
    public static SpawnManagerPoisson instance;
    
    private Collectable _collectable;
    
    private Destructible _destructible;
    
    private Mobile _mobile;
    
    // Planet, Cloud, Sun, Moon, Rock, Flower, Mushroom, Log, Vehicle, People, Trash bag, etc
    [SerializeField] private GameObject[] collectables;
    [SerializeField] private GameObject[] destructibles;
    [SerializeField] private GameObject[] immobiles;
    [SerializeField] private GameObject[] mobiles;
    
    [SerializeField] private List<GameObject> instances;
    
    [SerializeField] private GameObject _player;
    
    // [SerializeField] private float _objectSize = 1f;
    
    [SerializeField] private float width = 100f;
    [SerializeField] private float height = 100f;
    [SerializeField] private float depth = 100f;
    [SerializeField] private float radius = 5f;
    
    private Vector3 _startPosition;
    // private Hashtable _objectContainer = new Hashtable();
    // private List<Vector3> _objectPositions = new List<Vector3>();
    // private int distance = 10;
    // public int xPlayerMove => (int)(_player.transform.position.x - _startPosition.x);
    // public int yPlayerMove => (int)(_player.transform.position.y - _startPosition.y);
    // public int zPlayerMove => (int)(_player.transform.position.z - _startPosition.z);
    // private int xPlayerLocation => (int)Mathf.Floor(_player.transform.position.x);
    // private int yPlayerLocation => (int)Mathf.Floor(_player.transform.position.y);
    // private int zPlayerLocation => (int)Mathf.Floor(_player.transform.position.z);
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SpawnObjects();
    }

    // private void Update()
    // {
    //     if (Mathf.Abs(xPlayerMove) >= distance || Mathf.Abs(yPlayerMove) >= distance ||
    //         Mathf.Abs(zPlayerMove) >= distance)
    //     {
    //         SpawnObjects();
    //     }
    // }

    private void SpawnObjects()
    {
        // int numCollectables = 1;
        // int numDestructibles = 1;
        // int numMobiles = 1;
        // int numImmobiles = 1;
        
        foreach (GameObject go in collectables)
        {
            // PoissonDiscSampler3D sampler = new PoissonDiscSampler3D(position.x, position.y, position.z, _radius);
            
            PoissonDiscSampler3D sampler = new PoissonDiscSampler3D(width, height, depth, radius);
            
            foreach (Vector3 sample in sampler.Samples())
            {
                // if (!_objectContainer.ContainsKey(sample))
                // {
                    GameObject ins = Instantiate(go, new Vector3(sample.x, sample.y, sample.z), Quaternion.identity);
                    ins.tag = "Collectable";
                    instances.Add(ins);

                    // _objectContainer.Add(sample, ins);

                    // _objectPositions.Add(ins.transform.position);
                    // }
            }
        }
        
        foreach (GameObject go in destructibles)
        {
            // PoissonDiscSampler3D sampler = new PoissonDiscSampler3D(position.x, position.y, position.z, _radius);
            
            PoissonDiscSampler3D sampler = new PoissonDiscSampler3D(width, height, depth, radius);
            
            foreach (Vector3 sample in sampler.Samples())
            {
                // if (!_objectContainer.ContainsKey(sample))
                // {
                    GameObject ins = Instantiate(go, new Vector3(sample.x, sample.y, sample.z), Quaternion.identity);
                    ins.tag = "Destructible";
                    instances.Add(ins);

                    // _objectContainer.Add(sample, ins);

                    // _objectPositions.Add(ins.transform.position);
                // }
            }
        }
        
        foreach (GameObject go in immobiles)
        {
            // PoissonDiscSampler3D sampler = new PoissonDiscSampler3D(position.x, position.y, position.z, _radius);
            
            PoissonDiscSampler3D sampler = new PoissonDiscSampler3D(width, height, depth, radius);
            
            foreach (Vector3 sample in sampler.Samples())
            {
                // if (!_objectContainer.ContainsKey(sample))
                // {
                GameObject ins = Instantiate(go, new Vector3(sample.x, sample.y, sample.z), Quaternion.identity);
                ins.tag = "Immobile";
                instances.Add(ins);

                // _objectContainer.Add(sample, ins);

                // _objectPositions.Add(ins.transform.position);
                // }
            }
        }
        
        foreach (GameObject go in mobiles)
        {
            // PoissonDiscSampler3D sampler = new PoissonDiscSampler3D(position.x, position.y, position.z, _radius);
            
            PoissonDiscSampler3D sampler = new PoissonDiscSampler3D(width, height, depth, radius);
            
            foreach (Vector3 sample in sampler.Samples())
            {
                // if (!_objectContainer.ContainsKey(sample))
                // {
                    GameObject ins = Instantiate(go, new Vector3(sample.x, sample.y, sample.z), Quaternion.identity);
                    ins.tag = "Mobile";
                    instances.Add(ins);
                    
                    // _objectContainer.Add(sample, ins);

                    // _objectPositions.Add(ins.transform.position);
                // }
            }
        }
        
        // foreach (GameObject go in instances)
        // {
        //      ChildrenFinder(go);
        // }
    }

    private void ChildrenFinder(GameObject go)
    {
        List<GameObject> childrenList = new List<GameObject>();
        
        Transform[] children = go.GetComponentsInChildren<Transform>(true);
        
        foreach (Transform child in children)
        {
            bool haveComponent = child.gameObject.GetComponent<MeshRenderer>();
            
            if (haveComponent)
            {
                childrenList.Add(child.gameObject);
            }
        }

        foreach (GameObject child in childrenList)
        {
            switch (child.tag)
            {
                case "Collectable":
                    
                    child.AddComponent<Floatable>();
                    
                    if (child.gameObject.GetComponent<MeshRenderer>() != null)
                    {
                        child.GetComponent<MeshCollider>().convex = true;
                    }
                    else
                    {
                        child.AddComponent<MeshCollider>();
                        child.GetComponent<MeshCollider>().convex = true;
                    }

                    if (child.gameObject.GetComponent<Rigidbody>() != null)
                    {
                        child.GetComponent<Rigidbody>().useGravity = false;
                        child.GetComponent<Rigidbody>().linearDamping = 0.01f;
                    }
                    else
                    {
                        child.AddComponent<Rigidbody>();
                        child.GetComponent<Rigidbody>().useGravity = false;
                        child.GetComponent<Rigidbody>().linearDamping = 0.01f;
                    }

                    if (child.gameObject.GetComponent<Collectable>() == null)
                    {
                        child.AddComponent<Collectable>();
                    }
                    else return;
                    
                    break;
                
                case "Destructible":
                    
                    child.AddComponent<Floatable>();
                    
                    if (child.gameObject.GetComponent<MeshRenderer>() != null)
                    {
                        child.GetComponent<MeshCollider>().convex = true;
                    }
                    else
                    {
                        child.AddComponent<MeshCollider>();
                        child.GetComponent<MeshCollider>().convex = true;
                    }

                    if (child.gameObject.GetComponent<Rigidbody>() != null)
                    {
                        child.GetComponent<Rigidbody>().useGravity = false;
                        child.GetComponent<Rigidbody>().linearDamping = 0.01f;
                    }
                    else
                    {
                        child.AddComponent<Rigidbody>();
                        child.GetComponent<Rigidbody>().useGravity = false;
                        child.GetComponent<Rigidbody>().linearDamping = 0.01f;
                    }

                    if (child.gameObject.GetComponent<Destructible>() == null)
                    {
                        child.AddComponent<Destructible>();
                    }
                    else return;
                    
                    break;
                
                case "Immobile":
                    
                    if (child.gameObject.GetComponent<MeshRenderer>() != null)
                    {
                        child.GetComponent<MeshCollider>().convex = true;
                    }
                    else
                    {
                        child.AddComponent<MeshCollider>();
                        child.GetComponent<MeshCollider>().convex = true;
                    }

                    if (child.gameObject.GetComponent<Rigidbody>() != null)
                    {
                        child.GetComponent<Rigidbody>().useGravity = false;
                    }
                    else
                    {
                        child.AddComponent<Rigidbody>();
                        child.GetComponent<Rigidbody>().useGravity = false;
                    }
                    
                    break;
                
                case "Mobile":
                    
                    child.AddComponent<Floatable>();
                    
                    if (child.gameObject.GetComponent<MeshRenderer>() != null)
                    {
                        child.GetComponent<MeshCollider>().convex = true;
                    }
                    else
                    {
                        child.AddComponent<MeshCollider>();
                        child.GetComponent<MeshCollider>().convex = true;
                    }

                    if (child.gameObject.GetComponent<Rigidbody>() != null)
                    {
                        child.GetComponent<Rigidbody>().useGravity = false;
                        child.GetComponent<Rigidbody>().linearDamping = 0.01f;
                    }
                    else
                    {
                        child.AddComponent<Rigidbody>();
                        child.GetComponent<Rigidbody>().useGravity = false;
                        child.GetComponent<Rigidbody>().linearDamping = 0.01f;
                    }

                    if (child.gameObject.GetComponent<Mobile>() == null)
                    {
                        child.AddComponent<Mobile>();
                    }
                    else return;
                    
                    break;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


// Using Perlin Noise to make it come true X-)
public class VoxelSpawner : MonoBehaviour
{
    public static VoxelSpawner instance;
    
    private Collectable _collectable;
    
    private Destructible _destructible;
    
    private Mobile _mobile;
    
    [SerializeField] private GameObject[] collectables;
    
    [SerializeField] private GameObject[] destructibles;
    
    [SerializeField] private GameObject[] mobiles;
    
    [SerializeField] private GameObject[] immobiles;
    
    [SerializeField] private int width = 20;
    
    [SerializeField] private int length = 10;
    
    [SerializeField] private int depth = 20;
    
    [SerializeField] private int noiseHeight = 8;

    private float _gridOffset = 2f;
    
    // [SerializeField] private List<GameObject> instances;
    
    [SerializeField] private GameObject player;
    
    private Vector3 _startPos;
    
    [SerializeField] private GameObject objectToSpawn;
    
    private Hashtable _blockContainer = new Hashtable();
    
    private List<Vector3> _blockPositions = new List<Vector3>();
    
    private int distance = 10;

    private float detailScale = 8f;
    
    public int xPlayerMove => (int)(player.transform.position.x - _startPos.x);
    
    public int yPlayerMove => (int)(player.transform.position.y - _startPos.y);
    
    public int zPlayerMove => (int)(player.transform.position.z - _startPos.z);

    private int xPlayerLocation => (int)Mathf.Floor(player.transform.position.x);
    
    private int yPlayerLocation => (int)Mathf.Floor(player.transform.position.y);
    
    private int zPlayerLocation => (int)Mathf.Floor(player.transform.position.z);
    
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
        GenerateBlocks();
    }

    private void Update()
    {
        if (Mathf.Abs(xPlayerMove) >= distance || Mathf.Abs(yPlayerMove) >= distance ||
            Mathf.Abs(zPlayerMove) >= distance)
        {
            GenerateBlocks();
        }
    }

    private void GenerateBlocks()
    {
        _startPos = player.transform.position;
        
        for (int x = -width; x < width; x++)
        {
            for (int y = -length; y < length; y++)
            {
                for (int z = -depth; z < depth; z++)
                {
                    Vector3 pos = new Vector3(x * _gridOffset + xPlayerLocation, 
                        (yNoise(x + xPlayerLocation, z + zPlayerLocation, detailScale) + yPlayerLocation) * noiseHeight, 
                        z * _gridOffset + zPlayerLocation);
                    
                    // Vector3 pos = new Vector3(_startPosition.x + GeneratedNoiseYZ(y, z, detailScale) * noiseHeight,
                    //     _startPosition.y + GeneratedNoiseXZ(x, z, detailScale) * noiseHeight,
                    //     _startPosition.z + GeneratedNoiseXY(x, y, detailScale) * noiseHeight);

                    if (!_blockContainer.Contains(pos))
                    {
                        GameObject go = Instantiate(objectToSpawn, pos, Quaternion.identity);
                    
                        _blockContainer.Add(pos, go);

                        _blockPositions.Add(go.transform.position);
                    }
                }
            }
        }
    }
    
    

    private float xNoise(int y, int z, float detailScale)
    {
        float yNoise = (y + this.transform.position.y) / detailScale;
        float zNoise = (z + this.transform.position.y) / detailScale;
        
        return Mathf.PerlinNoise(yNoise, zNoise);
    }
    private float yNoise(int x, int z, float detailScale)
    {
        float xNoise = (x + this.transform.position.x) / detailScale;
        float zNoise = (z + this.transform.position.y) / detailScale;
        
        return Mathf.PerlinNoise(xNoise, zNoise);
    }
    
    private float zNoise(int x, int y, float detailScale)
    {
        float xNoise = (x + this.transform.position.x) / detailScale;
        float yNoise = (y + this.transform.position.z) / detailScale;
        
        return Mathf.PerlinNoise(xNoise, yNoise);
    }

    private void SpawnObject(int spawnNumber, string spawnType)
    {
        for (int c = 0; c < spawnNumber; c++)
        {
            GameObject toPlacecObject = Instantiate(objectToSpawn, ObjectSpawnLocation(), Quaternion.identity);
        }
    }

    private Vector3 ObjectSpawnLocation()
    {
        int rndIndex = Random.Range(0, _blockPositions.Count);

        Vector3 newPos = new Vector3(
            _blockPositions[rndIndex].x,
            _blockPositions[rndIndex].y + 1.0f,
            _blockPositions[rndIndex].z);
        
        _blockPositions.RemoveAt(rndIndex);
        return newPos;
    }
    
    
}

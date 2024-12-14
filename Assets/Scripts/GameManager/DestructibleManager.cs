using UnityEngine;

public class DestructibleManager : MonoBehaviour
{
    public static DestructibleManager instance;
    
    public int CurrentRecord { get; private set; }

    public void Awake()
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

    public void IncrementRecord(int amount)
    {
        CurrentRecord += amount;

        HUDManager.instance.UpdateTextDestructible(CurrentRecord);
    }
}

using UnityEngine;

public class MobileManager : MonoBehaviour
{
    public static MobileManager instance;
    
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

        // HUDManager.instance.UpdateTextMobile(CurrentRecord);
    }
}

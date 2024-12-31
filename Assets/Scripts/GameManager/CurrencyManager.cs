using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;
    
    public int CurrentCurrency { get; private set; }

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

    public void IncrementCurrency(int amount)
    {
        CurrentCurrency += amount;

        UIManager.instance.UpdateTextCollectable(CurrentCurrency);
    }
}

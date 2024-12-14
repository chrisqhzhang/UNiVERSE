using UnityEngine;

[CreateAssetMenu(fileName = "New Trash Collectable", menuName = "Data/Collectable/Currency/Trash")]
public class CollectableCurrencySO : CollectableSOBase
{
    [Header("Collectable Stats")]
    public int CurrencyAmount = 1;
    
    public override void Collect(GameObject objectCollected)
    {
        CurrencyManager.instance.IncrementCurrency(CurrencyAmount);

        if (_playerEffects == null)
        {
            GetReferences(objectCollected);
        }
        
        _playerEffects.PlayCollectionEffect(CollectFlashTime, CollectColor, CollectClip);
    }
}

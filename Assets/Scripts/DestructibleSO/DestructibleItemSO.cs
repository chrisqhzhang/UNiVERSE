using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Item Destructible", menuName = "Data/Destructible/Item")]
public class DestructibleItemSO : DestructibleSOBase
{
    [Header("Destructible Stats")]
    public int DestructibleAmount = 1;

    public override void Destruct(GameObject objectDestructed)
    {
        DestructibleManager.instance.IncrementRecord(DestructibleAmount);

        if (_playerEffects == null)
        {
            GetReferences(objectDestructed);
        }
        
        _playerEffects.PlayCollectionEffect(DestructFlashTime, DestructColor, DestructClip);
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "New Item Mobile", menuName = "Data/Mobile/Item")]
public class MobileRecordSO : MobileSOBase
{
    [Header("Mobile Stats")]
    public int MobileAmount = 1;

    public override void Move(GameObject objectMoved)
    {
        MobileManager.instance.IncrementRecord(MobileAmount);

        if (_playerEffects == null)
        {
            GetReferences(objectMoved);
        }
        
        _playerEffects.PlayCollectionEffect(MoveFlashTime, MoveColor, MoveClip);
    }
}

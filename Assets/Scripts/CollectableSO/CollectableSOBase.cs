using UnityEngine;

public abstract class CollectableSOBase : ScriptableObject
{
    [Header("Collection Effects")] 
    public Color CollectColor;
    public float CollectFlashTime = 0.5f;
    public AudioClip CollectClip;
    
    protected PlayerEffects _playerEffects;
    public abstract void Collect(GameObject objectCollected);

    public void GetReferences(GameObject objectCollected)
    {
        _playerEffects = FinderHelper.GetComponentOnObject<PlayerEffects>(objectCollected);
    }
}

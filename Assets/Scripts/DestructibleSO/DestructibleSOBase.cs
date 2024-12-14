using UnityEngine;

public abstract class DestructibleSOBase : ScriptableObject
{
    [Header("Destruction Effects")] 
    public Color DestructColor;
    public float DestructFlashTime = 0.5f;
    public AudioClip DestructClip;
    
    protected PlayerEffects _playerEffects;
    public abstract void Destruct(GameObject objectDestructed);

    public void GetReferences(GameObject objectDestructed)
    {
        _playerEffects = FinderHelper.GetComponentOnObject<PlayerEffects>(objectDestructed);
    }
}

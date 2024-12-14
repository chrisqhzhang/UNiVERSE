using UnityEngine;

public abstract class MobileSOBase : ScriptableObject
{
    [Header("Movement Effects")] 
    public Color MoveColor;
    public float MoveFlashTime = 0.5f;
    public AudioClip MoveClip;
    
    protected PlayerEffects _playerEffects;
    public abstract void Move(GameObject objectMoved);

    public void GetReferences(GameObject objectMoved)
    {
        _playerEffects = FinderHelper.GetComponentOnObject<PlayerEffects>(objectMoved);
    }
}

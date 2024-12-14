using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Data/Player/Stats")]
public class PlayerStatsSOBase : ScriptableObject
{
    [SerializeField] private float maxHealth;
    public float MaxHealth => maxHealth;
    
    [SerializeField] private float maxSpeed;
    public float MaxSpeed => maxSpeed;
}

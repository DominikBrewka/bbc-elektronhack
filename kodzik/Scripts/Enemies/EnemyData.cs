using UnityEngine;

[CreateAssetMenu(menuName = "elektronhack/EnemyData")]
public class EnemyData : ScriptableObject
{
    public new string name;

    public float health;
    
    [Range(1f, 250f)] public float defense;

    public float strength;

}
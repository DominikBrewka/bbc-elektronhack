using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Shop/ItemData")]
public class ItemData : ScriptableObject
{
    public new string name;
    public string description;

    public Texture sprite;

    public float pistolDamage;
    public float rocketDamage;
    public float movementSpeed;
    public float blastRadius;
    
    public int recoverHP;
    public int maxHP;
}

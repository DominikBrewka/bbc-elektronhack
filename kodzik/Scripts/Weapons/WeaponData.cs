using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/WeaponData")]
public class WeaponData : ScriptableObject
{
    public new string name;

    public float damage;

    public int magSize;
    public float fireRate;
    public float reloadTime;

    [Header("Cosmetics")]
    public AnimationClip reloadAnimation;
    public AnimationClip shootAnimation;
    public AudioClip shootSound;
    public AudioClip reloadStartSound;
    public AudioClip reloadSound;
    public AudioClip noAmmoShootSound;
}
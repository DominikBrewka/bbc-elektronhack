using UnityEngine;
using System.Collections;
using TMPro;

public class Weapon : MonoBehaviour
{
    [SerializeField] WeaponData data;
    [SerializeField] GameObject mesh;
    AudioSource audioSource;
    bool isReloading;
    bool isThisDrawn;
    float timeSinceShot;
    public int currentAmmo;
    WaitForSeconds reloadWait;

    public TMP_Text ammo;


    void Start() {
        ammo = GameObject.Find("AMMO").GetComponent<TMP_Text>();
        reloadWait = new WaitForSeconds(data.reloadTime);
        audioSource = transform.parent.GetComponent<AudioSource>();
        currentAmmo = data.magSize;
        isThisDrawn = PlayerShooting.drawnWeapon == this;

        // Add listeners to shoot and reload actions
        PlayerShooting.shootAction += Shoot;
        PlayerShooting.reloadAction += Reload;
    }


    bool CanShoot() {
        return !isReloading && timeSinceShot > 1/data.fireRate;
    }

    void Update() {
        timeSinceShot += Time.deltaTime;
        // disable or enable rendering
        isThisDrawn = PlayerShooting.drawnWeapon == this;
        mesh.SetActive(isThisDrawn);
        StopCoroutine(ReloadRoutine());
        // Cancel reloading if weapon was switched
        if (isThisDrawn&& !isReloading)
        {
            ammo.text = "Amunicja: " + currentAmmo.ToString() + "/" + data.magSize;
        }
        if (!isThisDrawn && isReloading) {
            isReloading = false;
        }
    }

    public void Reload() {
        if (isThisDrawn) StartCoroutine(ReloadRoutine());
    }


    public IEnumerator ReloadRoutine() {
        ammo.text = "Prze³adowywanie";
        isReloading = true;

        // wait
        audioSource.PlayOneShot(data.reloadStartSound);
        yield return reloadWait;
        // Cancel reloading if weapon was switched
        if (!isReloading) { yield break; }
        isReloading = false;
        currentAmmo = data.magSize;
        audioSource.PlayOneShot(data.reloadSound);
        ammo.text = "Amunicja: " + currentAmmo.ToString() + "/" + data.magSize;
    }

    public void Shoot() {
        if (!isThisDrawn) return;
        if (!CanShoot()) return;

        if (currentAmmo > 0) {
            timeSinceShot = 0;
            currentAmmo--;
            // Hitscan behavior
            if (data.GetType() == typeof(WeaponHitscan)) {
                WeaponHitscan _data = (WeaponHitscan)data;
                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 100f)){
                    // ...
                }
            }
            // Projectile behavior
            else if (data.GetType() == typeof(WeaponProjectile)) {
                WeaponProjectile _data = (WeaponProjectile)data;
                Instantiate(_data.projectile, transform.position, transform.rotation);
            }
            // Wyœwietla ilosc ammo lmao
            ammo.text = currentAmmo.ToString() + "/" + data.magSize;

            // Particles and animations here
            // ...
            audioSource.PlayOneShot(data.shootSound);
            return;
        }
        // Not enough ammo
        audioSource.PlayOneShot(data.noAmmoShootSound);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
public class m416 : MonoBehaviour ,IInventoryItem
{
    public int id;
    public ItemType itemtype;
    public string weaponName;
    public Sprite weaponImage;
    public float damage;
    public float range;

    [System.Serializable]
    public class Attachment
    {
        public Transform muzzlePos;
        public Transform sightPos;
        public Transform extendedPos;
    };

    [System.Serializable]
    public class Sound
    {
        public AudioClip weaponSound;             //Weapon Sound Effect
        public AudioClip noAmmoSound;             //Empty Gun Sound 
        public AudioClip reloadSound;             //Reload Sound 
    };

    public Attachment attachment;
    public Sound sound;

    public int currentAmmo = 30;                    //The Current Ammo In Weapon
    public int defaultMagSize = 30;               //How Much Ammo Is In Each Mag
    public int magMaxSize = 30;
    public int totalAmmoInInventory;                  //How Much Ammo Is In Your Cache (Storage)
    public int ammoNeeded;                     //Ammo Counter For How Much Is Needed, You Shoot 5 Bullets, You Need 5

    public WeaponShotType.ShotType shotType;
    public float fireRate;

    [SerializeField]
    public GameObject BulletPrefab;

    float nextTimeToFire = 0f;

    public string Name => weaponName;

    public Sprite spriteImage => weaponImage;

    public int ItemId { get => id; set => id = value; }

    public ItemType itemType { get => itemtype; set => itemtype = value; }

    public InputAction mouse,reload;

    public bool isFiring = false;

    public ParticleSystem[] muzzleFlash;


    public event Action<string> uiAmmoUpdater;
    public event Action<WeaponShotType.ShotType> shotTypeUpdater;
    public event Action<float , string> uiReloadUpdater;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void UiUpdate(string text)
    {
        uiAmmoUpdater?.Invoke(text);
    }

    public void ShotTypeUpdater(WeaponShotType.ShotType shotType)
    {
        shotTypeUpdater?.Invoke(shotType);
    }



    bool CheckFireRate()
    {
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + (1f / fireRate);
            return true;
        }
        return false;
    }

    public void shoot()
    {
        if (mouse.IsPressed() && isFiring == true)
        {
            if(currentAmmo > 0)
            {
                if (shotType == WeaponShotType.ShotType.Auto)
                {

                    if (CheckFireRate())
                    {

                        InstanceBullet(attachment.muzzlePos);
                    }
                }
                else if (shotType == WeaponShotType.ShotType.Single)
                {

                    InstanceBullet(attachment.muzzlePos);
                    StopFiring();
                }
            }
            else
            {
                //StartCoroutine(playTheSound(sound.noAmmoSound));
                if(mouse.triggered)
                {
                    audioSource.PlayOneShot(sound.noAmmoSound);
                }

            }

        }
    }

    public void Reloading()
    {
        StartCoroutine(reloadTimer());
        float finalTime = Time.time + 5;
        while (Time.time < finalTime)
        {

            float currenTime = Time.time;
            Debug.Log(currenTime);
            uiReloadUpdater(finalTime / currenTime, (finalTime / currenTime).ToString());

            
        }

    }
    bool isReloading;

    IEnumerator reloadTimer()
    {
        isReloading = true;
        isFiring = false;

        yield return new WaitForSeconds(7);
        currentAmmo += ammoNeeded;
        //reseting Ammo
        ammoNeeded = 0;
        uiAmmoUpdater(currentAmmo + " / inf");
        isReloading = false;

        isFiring = true;
    }

    void ParticlesEmitter()
    {
        foreach (var particle in muzzleFlash)
        {
            particle.Emit(1);
        }
    }

    public void StartFiring()
    {
        if (isReloading) return;
        isFiring = true;

    }

    public void StopFiring()
    {
        isFiring = false;
    }



    public GameObject InstanceBullet(Transform origin)
    {
        ammoNeeded++;
        currentAmmo--;
        uiAmmoUpdater(currentAmmo + " / inf");
        ParticlesEmitter();
        audioSource.PlayOneShot(sound.weaponSound);
        //audioSource.PlayOneShot(sound.weaponSound);
        GameObject projectile = Instantiate(
            BulletPrefab,
            origin.position,
            origin.rotation,
            null
            );
        return projectile;
    }
    

    public void OnPickup()
    {
        uiAmmoUpdater(currentAmmo + " / inf");
        shotTypeUpdater(shotType);
    }



}


public static class WeaponShotType
{
    public enum ShotType
    {
        Auto,
        Single,
        Burst
    }
    public static ShotType Next(this ShotType myEnum)
    {
        switch (myEnum)
        {
            case ShotType.Auto:
                return ShotType.Burst;
            case ShotType.Burst:
                return ShotType.Single;
            case ShotType.Single:
                return ShotType.Auto;
            default:
                return 0;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
        public AudioSource weaponSound;             //Weapon Sound Effect
        public AudioSource noAmmoSound;             //Empty Gun Sound 
        public AudioSource reloadSound;             //Reload Sound 
    };

    public Attachment attachment;
    public Sound sound;

    public int currentAmmo = 30;                    //The Current Ammo In Weapon
    public int defaultMagSize = 30;               //How Much Ammo Is In Each Mag
    public int magMaxSize = 30;
    public int totalAmmoInInventory;                  //How Much Ammo Is In Your Cache (Storage)
    public int ammoNeeded;                     //Ammo Counter For How Much Is Needed, You Shoot 5 Bullets, You Need 5

    public ShotType shotType;
    public float fireRate;

    [SerializeField]
    public GameObject BulletPrefab;

    float nextTimeToFire = 0f;

    public string Name => weaponName;

    public Sprite spriteImage => weaponImage;

    public int ItemId { get => id; set => id = value; }

    public ItemType itemType { get => itemtype; set => itemtype = value; }

    public InputAction mouse;

    public bool isFiring = false;

    public ParticleSystem[] muzzleFlash;

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
                ParticlesEmitter();
                if (shotType == ShotType.Auto)
                {
                    if (CheckFireRate())
                    {
                        Debug.Log(shotType);
                        InstanceBullet(attachment.muzzlePos);
                    }
                }
                else if (shotType == ShotType.Single)
                {
                    Debug.Log(shotType);
                    InstanceBullet(attachment.muzzlePos);
                    StopFiring();
                }
            }

        }

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
        Debug.Log("m416 got picked up");
    }
}
public enum ShotType
{
    Auto,
    Single,
    Burst
}
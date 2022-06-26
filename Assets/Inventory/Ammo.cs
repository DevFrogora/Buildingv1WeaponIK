using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public ParticleSystem hitEffect;
    public enum AmmoType
    {
        FivePointFive,
        SevenPointSeven,
        NinePointNine
    };
    public AmmoType ammoType;

    public float time;
    public Vector3 initalPosition;
    public Vector3 initialVelocity;
    public float bulletSpeed = 1000f;
    public float bulletDrop = 0f;

    float maxLifeTime = 3f;


    Vector3 GetPosition()
    {
        // p + v*t + 0.5* g *t *t
        Vector3 gravity = Vector3.down * bulletDrop;
        return (initalPosition) + (initialVelocity * time) + (0.5f * gravity * time * time); 
    }

    private void Start()
    {
        CreateBullet();
    }

    public void CreateBullet()
    {
        initalPosition = transform.position;
        initialVelocity = transform.forward.normalized * bulletSpeed;
        time = 0f;
    }

    void Updatebullet(float deltaTime)
    {
        SimulateBullets(deltaTime);
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        if(time > maxLifeTime)
        {
            gameObject.SetActive(false);
        }
    }

    void SimulateBullets(float deltatime)
    {
        Vector3 p0 = GetPosition();
        time += deltatime;
        Vector3 p1 = GetPosition();
        RaycastSegment(p0, p1);
    }

    Ray bulletRay;
    RaycastHit bulletHitInfo;
    void RaycastSegment(Vector3 start , Vector3 end)
    {
        Vector3 direction = end - start;
        float distance = direction.magnitude;
        bulletRay.origin = start;
        bulletRay.direction = direction;

        if (Physics.Raycast(bulletRay, out bulletHitInfo, distance))
        {
            hitEffect.transform.position = bulletHitInfo.point;
            hitEffect.transform.forward = bulletHitInfo.normal;
    
            transform.position = bulletHitInfo.point;
            hitEffect.Emit(1);
            time = maxLifeTime;
        }
        else
        {
            //Debug.DrawLine(transform.position, transform.position + (transform.forward * 10f), Color.red, 20);

        }
    }

    private void Update()
    {
        Updatebullet(Time.deltaTime);
    }


}

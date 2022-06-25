using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public enum AmmoType
    {
        FivePointFive,
        SevenPointSeven,
        NinePointNine
    };
    public AmmoType ammoType;


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ColliderEnter");
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("OnCollisionStay");
    }

    private void OnDestroy()
    {
        Debug.Log("I am getting destroyed ?");
    }

}

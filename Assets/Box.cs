using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour , IGrab
{

    public int id;
    public ItemType itemtype;
    public string weaponName;
    public Sprite weaponImage;

    public Transform leftHandGrip;
    public Transform rightHandGrip;


    public string Name => weaponName;

    public Sprite spriteImage => weaponImage;

    public int ItemId { get => id; set => id = value; }

    public ItemType itemType { get => itemtype; set => itemtype = value; }


    public void OnPickup()
    {

    }

}